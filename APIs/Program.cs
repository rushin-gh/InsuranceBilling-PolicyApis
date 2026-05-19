using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Models.Authentication;
using Services.Authentication;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// [JWT.SERVICES.START]
builder.Services.Configure<JwtConfigModel>(builder.Configuration.GetSection("JwtCofig"));
builder.Services.AddScoped<JwtService>();

// Add JWT Authentication
var JwtConfig = builder.Configuration.GetSection("JwtConfig").Get<JwtConfigModel>();
var key = Encoding.UTF8.GetBytes(JwtConfig.SecretKey);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = JwtConfig.Issuer,
        ValidAudience = JwtConfig.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ClockSkew = TimeSpan.Zero // Remove default 5 min tolerance
    };
});
// [JWT.SERVICES.END]

builder.Services.AddAuthorization();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

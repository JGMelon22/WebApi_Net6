using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using WebApi_ManProg.Infra.IoC;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddServices(builder.Configuration);
builder.Services.AddMvc().AddJsonOptions(options => // Remove os retornos nulos no get
{
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

var app = builder.Build();

// Serviço da autenticação
var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Net6DecoupledAPI"));

// Tipo de autenticação que será usada
builder.Services.AddAuthentication(authOptions =>
{
    authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer("Bearer", options =>
{
    // Chama a autenticação em si
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        IssuerSigningKey = key,
        ValidateAudience = false,
        ValidateIssuer = false
    };
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// Chama a autenticação no programa em si após o registro do serviço
app.UseAuthentication();

app.MapControllers();

app.Run();
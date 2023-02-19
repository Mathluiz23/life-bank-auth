using System.Text;
using LifeBankAuth.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using LifeBankAuth.Constants;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(options =>
{
  options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
  options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
  options.SaveToken = true;
  options.RequireHttpsMetadata = false;
  options.TokenValidationParameters = new TokenValidationParameters()
  {
    ValidateIssuer = false,
    ValidateAudience = false,
    ValidateIssuerSigningKey = true,
    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(TokenConstant.Secret))
  };
});

// Adicionar POLICY CLAIMS BASED aqui!
builder.Services.AddAuthorization(options =>
{
  options.AddPolicy("NewPromo", policy =>
  {
    policy.RequireClaim("Person", ClientTypeEnum.PessoaFisica.ToString());
    policy.RequireClaim("Currency", new string[] { CurrencyEnum.Real.ToString(), CurrencyEnum.Peso.ToString() });
  }
  );

});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

// Adicionar uso de autenticação
app.UseAuthentication();

// Adicionar uso de autorização
app.UseAuthentication();

app.UseCors(c => c.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.MapControllers();

app.Run();

public partial class Program { }
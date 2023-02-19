using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LifeBankAuth.Models;
using Microsoft.IdentityModel.Tokens;





namespace LifeBankAuth.Services
{
  public class TokenGenerator
  {
    /// <summary>
    /// This function is to Generate Token 
    /// </summary>
    /// <returns>A string, the token JWT</returns>
    public static string Generate(Client client)
    {
      var tokenHandler = new JwtSecurityTokenHandler();

      var tokenDescriptor = new SecurityTokenDescriptor()
      {
        Subject = AddClaims(client),
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(TokenConstant.Secret)),
          SecurityAlgorithms.HmacSha256Signature
          ),
        Expires = DateTime.Now.AddDays(1)
      };

      var token = tokenHandler.CreateToken(tokenDescriptor);

      return tokenHandler.WriteToken(token);
    }

    /// <summary>
    /// Function that adds the claims to the token
    /// </summary>
    /// <param name="client"> A client object value</param>
    /// <returns>Returns an object of type ClaimsIdentity</returns>
    private static ClaimsIdentity AddClaims(Client client)
    {
      var claims = new ClaimsIdentity();

      var claimsValues = new List<Claim> {
                new Claim(ClaimTypes.Name, client.Name),
                new Claim("Currency", client.Currency.ToString()),
                client.IsCompany ? new Claim("ClientType", ClientTypeEnum.PessoaJuridica.ToString())
                :
                new Claim("ClientType", ClientTypeEnum.PessoaFisica.ToString())
            };

      claims.AddClaims(claimsValues);
      return claims;
    }
  }
}
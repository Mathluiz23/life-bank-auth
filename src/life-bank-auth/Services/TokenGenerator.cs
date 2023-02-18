using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LifeBankAuth.Models;
using Microsoft.IdentityModel.Tokens;
using TokenConstants = LifeBankAuth.Constants.TokenConstant;




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
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(TokenConstants.Secret)),
          SecurityAlgorithms.HmacSha256Signature
          ),
        Expires = DateTime.Now.AddDays(TokenConstants.Expire)
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

      var clientType = client.IsCompany ? ClientTypeEnum.PessoaJuridica : ClientTypeEnum.PessoaFisica;

      claims.AddClaim(new Claim(ClaimTypes.Role, clientType.ToString()));

      return claims;
    }
  }
}
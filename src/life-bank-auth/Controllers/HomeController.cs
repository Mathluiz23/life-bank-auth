using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LifeBankAuth.Controllers;

[ApiController]
[Route("[controller]")]
public class HomeController : ControllerBase
{
  /// <summary>
  /// Message function that does not need authorization
  /// </summary>
  /// <returns>Returns a message</returns>
  [HttpGet("MessageForEveryone")]
  [AllowAnonymous]
  public static ActionResult<string> CreateAccountUntilFriday()
  {
    return "Crie sua conta na Like Bank até sexta-feira!";
  }
}

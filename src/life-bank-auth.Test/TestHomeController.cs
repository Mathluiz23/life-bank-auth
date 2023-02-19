
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;

namespace LifeBankAuth.Test;

public class TestHomeController : IClassFixture<WebApplicationFactory<Program>>
{
  private readonly WebApplicationFactory<Program> _factory;
  public TestHomeController(WebApplicationFactory<Program> factory)
  {
    _factory = factory;
  }

  [Trait("Category", "2 - Criar Endpoint Anônimo")]
  [Fact(DisplayName = "Teste para MessageForEveryone com Status Ok")]
  public async Task TestMessageForEveryoneSuccess()
  {
    var client = _factory.CreateClient();

    var response = await client.GetAsync("/home/MessageForEveryone");

    response.StatusCode.Should().Be(HttpStatusCode.OK);
  }
}

using Microsoft.AspNetCore.Mvc;

namespace VaultGuardApi.Controllers;

[ApiController]
[Route("")]
public sealed class HomeController: ControllerBase
{
    public IActionResult Get()
    {
        List<User> users = [
        new()
        {
            Name = "Kleant",
            Surname = "Bajraktari",
            Age = 18
        },
        
        new()
        {
            Name = "Test",
            Surname = "User",
            Age = 20
        },
        new()
        {
            Name = "Test2",
            Surname = "User2",
            Age = 25
        }
        ];
        return Ok(users);
    }
}
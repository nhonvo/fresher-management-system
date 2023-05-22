using Domain;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class AppController : ControllerBase
{
    private readonly AppConfiguration _appsettings;

    public AppController(AppConfiguration appsettings)
    {
        _appsettings = appsettings;
    }

    [HttpGet("appsettings")]
    public IActionResult GetAppsettings()
    => Ok(_appsettings);
}

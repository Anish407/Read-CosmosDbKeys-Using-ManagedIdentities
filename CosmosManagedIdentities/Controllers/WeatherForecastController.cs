using Microsoft.AspNetCore.Mvc;

namespace CosmosManagedIdentities.Controllers;
[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    public DatabaseAccountListKeysResult DatabaseAccountListKeys { get; }


    //Inject the DatabaseAccountListKeysResult
    public WeatherForecastController(DatabaseAccountListKeysResult databaseAccountListKeys)
        => DatabaseAccountListKeys = databaseAccountListKeys;

    //Return the keys registered in the DI container
    [HttpGet]
    public IActionResult Get() => Ok(DatabaseAccountListKeys);

}

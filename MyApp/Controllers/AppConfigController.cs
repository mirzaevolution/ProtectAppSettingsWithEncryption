using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace MyApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppConfigController : ControllerBase
    {
        private readonly IConfiguration _config;

        public AppConfigController(IConfiguration configuration)
        {
            _config = configuration;
        }


        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new
            {
                ConnectionString = _config["ConnectionStrings:DefaultConnection"],
                RandomKey = _config["SensitiveInfo:RandomKey"]
            });
        }
    }
}

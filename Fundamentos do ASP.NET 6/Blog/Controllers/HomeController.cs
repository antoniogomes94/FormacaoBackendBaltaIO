using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    [ApiController]
    [Route("")]
    public class HomeController : ControllerBase
    {
        [HttpGet("")]
        //Endpoint para helath check para ver se a api está ok
        public IActionResult Get()
        {
            return Ok();
        }
        
        [HttpGet("/ApiKey/")]
        //Endpoint para helath check para ver se a api está ok
        public IActionResult GetComAPiKey()
        {
            return Ok();
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AncientAura.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetData()
        {
            return Ok(new { Message = "Authorized access!" });
        }
    }
}

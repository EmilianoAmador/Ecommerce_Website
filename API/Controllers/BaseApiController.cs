using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]                 //API controller attribute
    [Route("api/[controller]")]     //Routing attribute
    public class BaseApiController : ControllerBase
    {
        
    }
}
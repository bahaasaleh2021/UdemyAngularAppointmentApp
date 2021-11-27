using API.Data;
using API.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API.Controllers
{
    [ServiceFilter(typeof(LogUserActivityActionFilter))]
    [ApiController]
    [Route("api/[controller]")]
    public class BaseController : ControllerBase
    {
       
    }
}
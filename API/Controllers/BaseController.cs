using API.Data;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseController : ControllerBase
    {
        public readonly DataContext _context;
        public BaseController(DataContext context)
        {
            _context = context;
        }
    }
}
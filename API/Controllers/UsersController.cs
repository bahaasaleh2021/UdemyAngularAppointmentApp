using System.Collections.Generic;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
   
    public class UsersController : BaseController
    {
   

    public UsersController(DataContext context):base(context)
    {
    }
     
     [HttpGet]
     public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers(){
         return await _context.AppUsers.ToListAsync();
     }
     
     
     [HttpGet("{id}")]
     [Authorize]
     public async Task<ActionResult<AppUser>> GetUser(int id){
         return await _context.AppUsers.FindAsync(id);
     }

    }
    
}
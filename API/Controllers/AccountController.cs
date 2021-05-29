using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using API.DTO;
using API.Entities;
using API.services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccountController : BaseController
    {
        public ITokenService _tokenSer { get; }
        public AccountController(DataContext context, ITokenService tokenSer) : base(context)
        {
            _tokenSer = tokenSer;

        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO user)
        {

            if (await DoesUserExist(user.UserName))
                return BadRequest("User Name is taken");

            using (var hmac = new HMACSHA512())
            {
                var newUser = new AppUser
                {
                    UserName = user.UserName.ToLower(),
                    PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(user.Password)),
                    PasswordSalt = hmac.Key
                };

                _context.AppUsers.Add(newUser);
                await _context.SaveChangesAsync();
                return new UserDTO
                {
                    UserName = user.UserName,
                    Token =_tokenSer.CreateToken(newUser)
                };
            }



        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO user){
            var existUser=await _context.AppUsers.FirstOrDefaultAsync(s=>s.UserName==user.UserName.ToLower());
            if(existUser==null)
             return Unauthorized("INVlaid username");
            using(var hmac=new HMACSHA512(existUser.PasswordSalt)){
                var computedHash=hmac.ComputeHash(Encoding.UTF8.GetBytes(user.Password));
                for (int i = 0; i < computedHash.Length; i++){
                    if(computedHash[i] !=existUser.PasswordHash[i])
                    return Unauthorized("InValid Password");
                }

                return new UserDTO{
                    UserName=user.UserName,
                    Token=_tokenSer.CreateToken(existUser)
                };
                
            }

        }

        private async Task<bool> DoesUserExist(string userName)
        {
            return await _context.AppUsers.AnyAsync(s => s.UserName == userName.ToLower());
        }
    }
}
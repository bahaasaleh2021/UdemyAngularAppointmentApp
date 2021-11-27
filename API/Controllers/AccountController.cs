using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using API.DTO;
using API.Entities;
using API.Interfaces;
using API.services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccountController : BaseController
    {
        public ITokenService _tokenSer { get; }
         public readonly DataContext _context;
         public readonly IMapper _mapper;
            
        
        public AccountController(DataContext context, ITokenService tokenSer,IMapper mapper) 
        {
            _tokenSer = tokenSer;
           _context = context;
           _mapper=mapper;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO user)
        {

            if (await DoesUserExist(user.UserName))
                return BadRequest("User Name is taken");

var newUser=_mapper.Map<AppUser>(user);
            using (var hmac = new HMACSHA512())
            {
                
                    newUser.UserName = user.UserName.ToLower();
                    newUser.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(user.Password));
                    newUser.PasswordSalt = hmac.Key;
                    
                

                _context.AppUsers.Add(newUser);
                await _context.SaveChangesAsync();
                return new UserDTO
                {
                    UserName = user.UserName,
                    Token =_tokenSer.CreateToken(newUser),
                    KnownAs=user.KnownAs,
                    Gender=user.Gender
                };
            }



        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO user){
            var existUser=await _context.AppUsers.Include(s=>s.Photos).FirstOrDefaultAsync(s=>s.UserName==user.UserName.ToLower());
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
                    Token=_tokenSer.CreateToken(existUser),
                    PhotoUrl=existUser.Photos.FirstOrDefault(s=>s.IsMain)?.Url,
                    KnownAs=existUser.KnownAs,
                    Gender=existUser.Gender
                };
                
            }

        }

        private async Task<bool> DoesUserExist(string userName)
        {
            return await _context.AppUsers.AnyAsync(s => s.UserName == userName.ToLower());
        }
    }
}
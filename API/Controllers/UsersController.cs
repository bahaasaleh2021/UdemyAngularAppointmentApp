using System.Collections.Generic;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using API.DTO;

namespace API.Controllers
{

    public class UsersController:BaseController
    {

        public IUserRepository _userRepository { get; }

        public UsersController(IUserRepository _userRepository)
        {
            this._userRepository = _userRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
        {
            var users=await _userRepository.GetMembersAsync();
            return users.ToList();
        }


        [HttpGet("{userName}")]
        [Authorize]
        public async Task<ActionResult<MemberDto>> GetUser(string userName)
        {
            return await _userRepository.GetMemberAsync(userName);
        }

    }

}
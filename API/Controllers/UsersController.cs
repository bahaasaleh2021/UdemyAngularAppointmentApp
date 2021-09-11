using System.Collections.Generic;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using API.DTO;
using API.Repositories;
using System.Security.Claims;
using AutoMapper;

namespace API.Controllers
{

    public class UsersController:BaseController
    {

        public IUserRepository _userRepository { get; }
        public IMapper _mapper { get; set; }

        public UsersController(IUserRepository _userRepository,IMapper _mapper)
        {
            this._userRepository = _userRepository;
            this._mapper=_mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetMembers()
        {
            return Ok(await _userRepository.GetMembersAsync());
           
        }


        [HttpGet("{userName}")]
        [Authorize]
        public async Task<ActionResult<MemberDto>> GetMember(string userName)
        {
            return Ok( await _userRepository.GetMemberAsync(userName));
        }


        [HttpPut]
        public async Task<ActionResult> UpdateUser(MemberUpdateDTO model){
              var userName=User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
              var user=await _userRepository.GetUserByUserNameAsync(userName);
              _mapper.Map(model,user);
             _userRepository.Update(user);
             
             if(await _userRepository.SaveAllAsync()>0)
               return NoContent();

               return BadRequest("Failed To Update User");

        }

    }

}
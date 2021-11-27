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
using API.Extensions;
using Microsoft.AspNetCore.Http;
using API.Interfaces;
using API.Helpers;

namespace API.Controllers
{

    public class UsersController : BaseController
    {

        public IUserRepository _userRepository { get; }
        private readonly IPhotoService _photoService;
        public IMapper _mapper { get; set; }

        public UsersController(IUserRepository _userRepository, IPhotoService _photoService, IMapper _mapper)
        {
            this._userRepository = _userRepository;
            this._photoService = _photoService;
            this._mapper = _mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetMembers([FromQuery]RequestParams userParams)
        {
            var curUser=User.GetUserName();

            var user= await _userRepository.GetUserByUserNameAsync(curUser);
          
            
            var userPagesList=await _userRepository.GetMembersAsync(userParams);
            Response.AddPaginationHeader(userPagesList.CurrentPage,userPagesList.PageSize,userPagesList.TotalPages,userPagesList.TotalCount);
            
            return Ok(userPagesList);

        }


        [HttpGet("{userName}")]
        [Authorize]
        public async Task<ActionResult<MemberDto>> GetMember(string userName)
        {
            return Ok(await _userRepository.GetMemberAsync(userName));
        }


        [HttpPut]
        public async Task<ActionResult> UpdateUser(MemberUpdateDTO model)
        {
            var userName = User.GetUserName();
            var user = await _userRepository.GetUserByUserNameAsync(userName);
            _mapper.Map(model, user);
            _userRepository.Update(user);

            if (await _userRepository.SaveAllAsync() > 0)
                return NoContent();

            return BadRequest("Failed To Update User");

        }

        [HttpPost("add-photo")]
        public async Task<ActionResult<PhotoDto>> AddPhoto(IFormFile file)
        {
            var result = await _photoService.AddPhotoAsync(file);
            if (result.Error != null)
                return BadRequest(result.Error.Message);

            var userName = User.GetUserName();
            var user = await _userRepository.GetUserByUserNameAsync(userName);

            var photo = new Photo
            {
                Url = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId,
                IsMain = !user.Photos.Any()
            };

            user.Photos.Add(photo);
            if (await _userRepository.SaveAllAsync() > 0)
            {
                var photoModel = _mapper.Map<PhotoDto>(photo);
                return CreatedAtAction("GetMember", new { userName = userName }, photoModel);
            }

            return BadRequest("Error when uploading photo");
        }

        [HttpPut("set-main-photo/{photoId}")]
        public async Task<ActionResult> SetMainPhoto(int photoId)
        {
           
           var user=await _userRepository.GetUserByUserNameAsync(User.GetUserName());
           var photo=user.Photos.FirstOrDefault(s=>s.Id==photoId);
           var main=user.Photos.FirstOrDefault(s=>s.IsMain);
           if(main!=null)
             main.IsMain=false;

           photo.IsMain=true;

           if(await _userRepository.SaveAllAsync()>0)
              return NoContent();

          return BadRequest("Error when updating photo");
        }
   
      
       [HttpDelete("delete-photo/{photoId}")]
        public async Task<ActionResult> DeletePhoto(int photoId)
        {
           
           var user=await _userRepository.GetUserByUserNameAsync(User.GetUserName());
           var photo=user.Photos.FirstOrDefault(s=>s.Id==photoId);
           if(photo==null)
             return NotFound();
               
           if(photo.IsMain)
              return BadRequest("You can not remove main photo");
            
            if(photo.PublicId!=null)
            {
              var result=await _photoService.DeletePhotoAsync(photo.PublicId);
              if(result.Error!=null)
                 return BadRequest(result.Error.Message);
            }

            user.Photos.Remove(photo);

           if(await _userRepository.SaveAllAsync()>0)
              return NoContent();

          return BadRequest("Error when removing photo");
        }
   
   
    }

    

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTO;
using API.Entities;
using API.Extensions;
using API.Helpers;
using API.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    public class MessagesController : BaseController
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public MessagesController(IMessageRepository messageRepository, IUserRepository userRepository, IMapper mapper)
        {
            _messageRepository = messageRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }


        [HttpPost]
        public async Task<ActionResult<MessageDTO>> Create(CreateMessageDTO createMessageDTO)
        {
            var userName = User.GetUserName();
            if (userName == createMessageDTO.RecepientUserName.ToLower())
                return BadRequest("You can not send message to youself");

            var sender = await _userRepository.GetUserByUserNameAsync(userName);
            var recepient = await _userRepository.GetUserByUserNameAsync(createMessageDTO.RecepientUserName);

            var message = new Message
            {
                Sender = sender,
                Recepient = recepient,
                SenderUserName = sender.UserName,
                RecepientUserName = recepient.UserName,
                Content = createMessageDTO.Content
            };

            _messageRepository.Add(message);

            if (await _messageRepository.SaveAll())
                return Ok(_mapper.Map<MessageDTO>(message));

            return BadRequest("Failed to send message");

        }

        [HttpGet]
        public async Task<IEnumerable<MessageDTO>> GetMessageForUser([FromQuery] MessageParams messageParams){

            messageParams.UserName=User.GetUserName();
            var myMessages=await _messageRepository.GetMessagesForUser(messageParams);
            Response.AddPaginationHeader(myMessages.CurrentPage,myMessages.PageSize,myMessages.TotalPages,myMessages.TotalCount);
            return myMessages;
        }

        [HttpGet("thread/{userName}")]
        public ActionResult<IEnumerable<MessageDTO>> GetMessageThread(string userName){
            var curUser=User.GetUserName();
            var thread= _messageRepository.GetMessgeThread(curUser,userName);
            return Ok(thread); 
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMessage(int id){
            var msg=await _messageRepository.Get(id);
            var userName=User.GetUserName();
            if(msg.RecepientUserName==userName)
              msg.DeletedByReciever=true;
            else if(msg.SenderUserName==userName)
              msg.DeletedBySender=true;

            if(msg.DeletedByReciever && msg.DeletedBySender)
               _messageRepository.Delete(msg);

            await _messageRepository.SaveAll();

            return Ok();

        }

    }
}
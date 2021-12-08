using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTO;
using API.Entities;
using API.Helpers;

namespace API.Repositories
{
    public interface IMessageRepository
    {
        void Add(Message message);
        void Delete(Message message);
        Task<Message> Get(int messageId);
        Task<PagedList<MessageDTO>> GetMessagesForUser(MessageParams messageParams);
        IEnumerable<MessageDTO> GetMessgeThread(string curUserName,string recepientUserName);
        Task<bool> SaveAll();
    }
}
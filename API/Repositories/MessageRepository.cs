using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTO;
using API.Entities;
using API.Helpers;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{

    public class MessageRepository : IMessageRepository
    {
        private readonly DataContext _db;
        private readonly IMapper _mapper;

        public MessageRepository(DataContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public void Add(Message message)
        {
            _db.Messages.Add(message);
        }

        public void Delete(Message message)
        {
            _db.Messages.Remove(message);
        }

        public async Task<Message> Get(int messageId)
        {
            return await _db.Messages.FindAsync(messageId);
        }

        public async Task<PagedList<MessageDTO>> GetMessagesForUser(MessageParams messageParams)
        {
            var q = _db.Messages.OrderByDescending(x => x.DateSent).AsQueryable();
            q = messageParams.Container switch
            {
                "Inbox" => q.Where(x => x.Recepient.UserName == messageParams.UserName && !x.DeletedByReciever),
                "Outbox" => q.Where(x => x.Sender.UserName == messageParams.UserName && !x.DeletedBySender),
                _ => q.Where(x => x.Recepient.UserName == messageParams.UserName && x.DateRead == null)
            };

            var list = q.ProjectTo<MessageDTO>(_mapper.ConfigurationProvider);

            return await PagedList<MessageDTO>.CreateAsync(list, messageParams.PageNo, messageParams.PageSize);
        }

        public IEnumerable<MessageDTO> GetMessgeThread(string curUserName, string recepientUserName)
        {
            var messages =  _db.Messages.
            Include(x => x.Sender).ThenInclude(x=>x.Photos).
            Include(x => x.Recepient).ThenInclude(x=>x.Photos).Where(
                x => x.Sender.UserName == curUserName && x.Recepient.UserName == recepientUserName && !x.DeletedBySender ||
                   x.Sender.UserName == recepientUserName && x.Recepient.UserName == curUserName && !x.DeletedByReciever
            ).OrderByDescending(x => x.DateSent).ToList();

            var unReadMsseages =   _db.Messages.Where(x => x.DateRead == null && x.Recepient.UserName == curUserName).ToList();

            if (unReadMsseages.Any())
            {
                foreach (var item in unReadMsseages)
                {
                    item.DateRead = DateTime.Now;
                }

                  _db.SaveChanges();
            }

            var thread=_mapper.Map<IEnumerable<MessageDTO>>(messages);
            return thread;

        }

        public async Task<bool> SaveAll()
        {
            return await _db.SaveChangesAsync() > 0;
        }
    }
}
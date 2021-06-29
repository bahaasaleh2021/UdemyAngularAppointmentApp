using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTO;
using API.Entities;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class UserRepository : IUserRepository
    {
        public DataContext Context { get; }
        public IMapper _mapper { get; }
        public UserRepository(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            Context = context;

        }

         public async Task<IEnumerable<MemberDto>> GetMembersAsync()
        {
        
         return await Context.AppUsers.ProjectTo<MemberDto>(_mapper.ConfigurationProvider).ToListAsync();

        }
        

        public async Task<MemberDto> GetMemberAsync(string userName)
        {
            return await Context.AppUsers.Where(x=>x.UserName==userName.ToLower()).ProjectTo<MemberDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();

        }


        public async Task<bool> SaveAllasync()
        {
            await Context.SaveChangesAsync();
            return true;
        }

        public void Update(AppUser user)
        {
            Context.Entry(user).State = EntityState.Modified;
        }

       

       
    }
}
using API.DTO;
using API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<MemberDto>> GetMembersAsync();
        Task<AppUser> GetUserByUserNameAsync(string userName);
        Task<MemberDto> GetMemberAsync(string userName);
        void Update(AppUser user);
        Task<int> SaveAllAsync();


    }
}

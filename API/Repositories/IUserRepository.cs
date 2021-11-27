using API.DTO;
using API.Entities;
using API.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repositories
{
    public interface IUserRepository
    {
        Task<PagedList<MemberDto>> GetMembersAsync(RequestParams userParams);
        Task<AppUser> GetUserByUserNameAsync(string userName);
        Task<AppUser> GetUserByUserIdAsync(int userId);
        Task<MemberDto> GetMemberAsync(string userName);
        void Update(AppUser user);
        Task<int> SaveAllAsync();


    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTO;
using API.Entities;

namespace API.Data
{
    public interface IUserRepository
    {
        void Update(AppUser user);
        Task<bool> SaveAllasync();
       
     
        Task<MemberDto> GetMemberAsync(string userName);
        
        Task<IEnumerable<MemberDto>> GetMembersAsync();

    }
}
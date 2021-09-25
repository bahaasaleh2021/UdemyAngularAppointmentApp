using API.Data;
using API.DTO;
using API.Entities;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _db;
        private readonly IMapper _mapper;

        public UserRepository(DataContext db,IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public async Task<AppUser> GetUserByUserNameAsync(string userName)
        {
            return await _db.AppUsers.Include(s=>s.Photos).FirstOrDefaultAsync(s=>s.UserName==userName);
        }

        public async Task<MemberDto> GetMemberAsync(string userName)
        {
            return await _db.AppUsers.Where(s => s.UserName == userName).
                            ProjectTo<MemberDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<MemberDto>> GetMembersAsync()
        {
            return await _db.AppUsers.ProjectTo<MemberDto>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<int> SaveAllAsync()
        {
            return await _db.SaveChangesAsync();
        }

        public  void Update(AppUser user)
        {
            _db.Entry(user).State = EntityState.Modified;
        }

        
    }
}

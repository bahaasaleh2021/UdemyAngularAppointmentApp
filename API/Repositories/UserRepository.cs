using API.Data;
using API.DTO;
using API.Entities;
using API.Helpers;
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

        public async Task<PagedList<MemberDto>> GetMembersAsync(RequestParams userParams)
        {
            var query=_db.AppUsers.AsQueryable();

            if(!string.IsNullOrEmpty(userParams.CurrentUser))
               query=query.Where(s=>s.UserName!=userParams.CurrentUser);

            if(!string.IsNullOrEmpty(userParams.Gender))
               query=query.Where(s=>s.Gender==userParams.Gender);

            if(userParams.MaxAge.HasValue){
                var minDob = DateTime.Today.AddYears(-userParams.MinAge.Value);
                query=query.Where(s=>s.DateOfBirth<=minDob);
            }

            if(userParams.MinAge.HasValue){
                var maxDob=DateTime.Today.AddYears(-userParams.MaxAge.Value);
                query=query.Where(s=>s.DateOfBirth>=maxDob);
            }

            query=userParams.OrderBy switch{
                "created"=>query.OrderByDescending(s=>s.Created),
                _ =>query.OrderByDescending(x=>x.LastActive)
            };

            
            var q=query.ProjectTo<MemberDto>(_mapper.ConfigurationProvider).AsNoTracking();

            return await PagedList<MemberDto>.CreateAsync(q,userParams.PageNo,userParams.PageSize);
        }

        public async Task<int> SaveAllAsync()
        {
            return await _db.SaveChangesAsync();
        }

        public  void Update(AppUser user)
        {
            _db.Entry(user).State = EntityState.Modified;
        }

        public async Task<AppUser> GetUserByUserIdAsync(int userId)
        {
             return await _db.AppUsers.FindAsync(userId);

        }
    }
}

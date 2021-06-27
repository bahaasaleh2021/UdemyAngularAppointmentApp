using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public static class Seed
    {
        public static async Task SeedUsers(DataContext cont){
            if(await cont.AppUsers.AnyAsync())
               return;

            var userData=await File.ReadAllTextAsync("Data/UserSeedData.json");
            var users=JsonSerializer.Deserialize<List<AppUser>>(userData);
            using var hmac=new HMACSHA512();

            foreach (var user in users){
               user.UserName=user.UserName.ToLower();
               user.PasswordHash=hmac.ComputeHash(Encoding.UTF8.GetBytes("P@ssw0rd"));
               user.PasswordSalt=hmac.Key;
               cont.AppUsers.Add(user);
            }
            
            await cont.SaveChangesAsync();
                
            
        }
    }
}
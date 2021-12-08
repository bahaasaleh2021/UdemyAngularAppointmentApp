using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions options):base(options)
        {
            
        }

        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder builder){

        builder.Entity<Message>()
        .HasOne(s=>s.Recepient)
        .WithMany(x=>x.MessagesRecieved)
        .OnDelete(DeleteBehavior.Restrict);

         builder.Entity<Message>()
        .HasOne(s=>s.Sender)
        .WithMany(x=>x.MessagesSent)
        .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
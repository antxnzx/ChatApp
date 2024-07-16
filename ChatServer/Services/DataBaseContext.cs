using ChatServer.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatServer.Services
{
    public class DataBaseContext : DbContext
    {
        internal DbSet<UserInfo> UserInfo { get; set; }
        internal DbSet<UserLoginInfo> UserLoginInfo { get; set; }
        internal DbSet<UserSubs> UserSubs { get; set; }

        public DataBaseContext(DbContextOptions<DataBaseContext> options)
         : base(options)
        {
            Database.EnsureCreated();   
        }
    }
}

using ChatServer.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatServer.Services
{
    public static class DbRequests
    {
        public static async Task<UserInfo?> GetUserInfo(DataBaseContext db, HttpContext context)
        {

            string? Login = context.Request.Query["login"];
            UserInfo? user = new();
            if (!string.IsNullOrWhiteSpace(Login))
            {

                user = await db.UserInfo
                  .Where(u => u.UserLogin == Login)
                  .FirstOrDefaultAsync();

            }
            return user;
        }
        public static async Task<List<UserSubs>> GetUserSubs(DataBaseContext db, HttpContext context)
        {

            string? Login = context.Request.Query["login"];
            List<UserSubs> subs = [];
            if (!string.IsNullOrWhiteSpace(Login))
            {

                 subs = await db.UserSubs
                  .Where(u => u.UserLogin == Login)
                  .ToListAsync();

            }
            return subs;
        }
    }
}

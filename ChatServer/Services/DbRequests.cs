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

        public static async Task<List<UserInfo>> GetUsers(DataBaseContext db, HttpContext context)
        {

            List<UserInfo> users = [];

            users = await db.UserInfo.ToListAsync();

            return users;
        }

        public static async Task<bool> AddSub(DataBaseContext db, HttpContext context)
        {
            bool response = false;
            UserSubs? user = await context.Request.ReadFromJsonAsync<UserSubs>();

            if (user != null)
            {
                UserSubs? user1 = await db.UserSubs
                     .Where(u => u == user)
                     .FirstOrDefaultAsync();
                if (user1 == null)
                {
                    await db.UserSubs.AddAsync(user);
                    await db.SaveChangesAsync();
                    response = true;
                }
            }
            return response;
        }
        public static async Task<bool> RemoveSub(DataBaseContext db, HttpContext context)
        {
            bool response = false;
            UserSubs? user = await context.Request.ReadFromJsonAsync<UserSubs>();

            if (user != null)
            {
                UserSubs? user1 = await db.UserSubs
                     .Where(u => u == user)
                     .FirstOrDefaultAsync();
                if (user1 != null)
                {
                     db.UserSubs.Remove(user);
                    await db.SaveChangesAsync();
                    response = true;
                }
            }
            return response;
        }
    }
}

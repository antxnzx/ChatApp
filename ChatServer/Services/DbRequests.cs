using ChatServer.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.X86;

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


        public static async Task<List<UserSubs>> GetUsersSubs(DataBaseContext db, HttpContext context)
        {

            List<UserSubs> users = [];

            users = await db.UserSubs.ToListAsync();

            return users;
        }
        public static async Task<bool> CreateSub(DataBaseContext db, HttpContext context)
        {
            bool response = false;
            UserSubs? user = await context.Request.ReadFromJsonAsync<UserSubs>();

            if (user != null)
            {
                UserSubs? user1 = await db.UserSubs
                     .Where(u => u.UserLogin == user.UserLogin 
                                 && u.Subscription == user.Subscription )
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
        public static async Task<bool> DeleteSub(DataBaseContext db, HttpContext context, int id)
        {
            bool response = false;
            UserSubs? user = null;
            try
            {
                user = await context.Request.ReadFromJsonAsync<UserSubs>();

            }
            catch (Exception)
            {

                
            }
            UserSubs? user1;
            if (user != null)
            {
                 user1 = await db.UserSubs
                     .Where(u => u.UserLogin == user.UserLogin 
                     && u.Subscription == user.Subscription)
                     .FirstOrDefaultAsync();
                
            }
            else
            {
                user1 = await db.UserSubs
                     .Where(u => u.Id == id)
                     .FirstOrDefaultAsync();
            }
            if (user1 != null)
            {
                db.UserSubs.Remove(user1);
                await db.SaveChangesAsync();
                response = true;
            }
            return response;
        }
        
        public static async Task<bool> UpdateSub(DataBaseContext db, HttpContext context)
        {
            bool response = false;
            UserSubs? user = await context.Request.ReadFromJsonAsync<UserSubs>();

            if (user != null)
            {
                UserSubs? user1 = await db.UserSubs
                     .Where(u => u.Id == user.Id)
                     .FirstOrDefaultAsync();
                if (user1 != null)
                { 
                    user1.UserLogin = user.UserLogin;
                    user1.Subscription = user.Subscription;
                    await db.SaveChangesAsync();
                    response = true;
                }
            }
            return response;
        }


        public static async Task<List<UserInfo>> GetUsersInfos(DataBaseContext db, HttpContext context)
        {

            List<UserInfo> users = [];

            users = await db.UserInfo.ToListAsync();

            return users;
        }

        public static async Task<bool> CreateUserInfo(DataBaseContext db, HttpContext context)
        {
            bool response = false;
            UserInfo? user = await context.Request.ReadFromJsonAsync<UserInfo>();

            if (user != null)
            {
                UserInfo? user1 = await db.UserInfo
                     .Where(u => u.UserLogin == user.UserLogin)
                     .FirstOrDefaultAsync();
                if (user1 == null)
                {
                    await db.UserInfo.AddAsync(user);
                    await db.SaveChangesAsync();
                    response = true;
                }
            }
            return response;
        }
        public static async Task<bool> DeleteUserInfo(DataBaseContext db, HttpContext context, int id)
        {
            bool response = false;
            //UserInfo? user = await context.Request.ReadFromJsonAsync<UserInfo>();

            
                UserInfo? user1 = await db.UserInfo
                     .Where(u => u.Id == id)
                     .FirstOrDefaultAsync();
                if (user1 != null)
                {

                    db.UserInfo.Remove(user1);
                    await db.SaveChangesAsync();
                    response = true;
                }
            
            return response;
        }

        public static async Task<bool> UpdateUserInfo(DataBaseContext db, HttpContext context)
        {
            bool response = false;
            UserInfo? user = await context.Request.ReadFromJsonAsync<UserInfo>();

            if (user != null)
            {
                UserInfo? user1 = await db.UserInfo
                     .Where(u => u.Id == user.Id)
                     .FirstOrDefaultAsync();
                if (user1 != null)
                {
                    user1.UserLogin = user.UserLogin;
                    user1.Surname = user.Surname;
                    user1.Name = user.Name;
                    user1.IsAdmin = user.IsAdmin;
                    await db.SaveChangesAsync();
                    response = true;
                }
            }
            return response;
        }

        public static async Task<List<UserLoginInfo>> GetUsersLoginInfos(DataBaseContext db, HttpContext context)
        {

            List<UserLoginInfo> users = [];

            users = await db.UserLoginInfo.ToListAsync();

            return users;
        }

        public static async Task<bool> CreateUserLoginInfo(DataBaseContext db, HttpContext context)
        {
            bool response = false;
            UserLoginInfo? user = await context.Request.ReadFromJsonAsync<UserLoginInfo>();

            if (user != null)
            {
                UserLoginInfo? user1 = await db.UserLoginInfo
                     .Where(u => u.Login == user.Login)
                     .FirstOrDefaultAsync();
                if (user1 == null)
                {
                    await db.UserLoginInfo.AddAsync(user);
                    await db.SaveChangesAsync();
                    response = true;
                }
            }
            return response;
        }
        public static async Task<bool> DeleteUserLoginInfo(DataBaseContext db, HttpContext context, int id)
        {
            bool response = false;
            //UserLoginInfo? user = await context.Request.ReadFromJsonAsync<UserLoginInfo>();

            
                UserLoginInfo? user1 = await db.UserLoginInfo
                     .Where(u => u.Id == id)
                     .FirstOrDefaultAsync();
                if (user1 != null)
                {

                    db.UserLoginInfo.Remove(user1);
                    await db.SaveChangesAsync();
                    response = true;
                }
            
            return response;
        }

        public static async Task<bool> UpdateUserLoginInfo(DataBaseContext db, HttpContext context)
        {
            bool response = false;
            UserLoginInfo? user = await context.Request.ReadFromJsonAsync<UserLoginInfo>();

            if (user != null)
            {
                UserLoginInfo? user1 = await db.UserLoginInfo
                     .Where(u => u.Id == user.Id)
                     .FirstOrDefaultAsync();
                if (user1 != null)
                {
                    user1.Login = user.Login;
                    user1.Password = user.Password;
                    await db.SaveChangesAsync();
                    response = true;
                }
            }
            return response;
        }
    }
}

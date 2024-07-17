using ChatServer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Owin.Security.Provider;

namespace ChatServer.Services
{
    public static class LoginService
    {
        public static async Task<bool> CheckLogin(DataBaseContext db, Logininfo info)
        {
            bool response = false;
            UserLoginInfo? user = await db.UserLoginInfo
                 .Where(u => u.Login == info.Login).FirstOrDefaultAsync();
            if (user != null)
            {
                if (user.Password == info.Password)
                {
                    response = true;
                }
            }
            return response;
        }

        public static async Task<bool> RegisterUser(DataBaseContext db, UserLoginInfo info)
        {
            bool response = false;
            UserLoginInfo? user = await db.UserLoginInfo
                .Where(u => u.Login == info.Login).FirstOrDefaultAsync();
            if (user == null)
            {
                await db.UserLoginInfo.AddAsync(info);
                await db.SaveChangesAsync();
                response = true;
            }

            return response;
        }

        public static async Task<bool> RegisterUser(DataBaseContext db, UserInfo info)
        {
            bool response = true;

            await db.UserInfo.AddAsync(info);
            await db.SaveChangesAsync();


            return response;
        }
    }
}

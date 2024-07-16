using ChatServer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Owin.Security.Provider;

namespace ChatServer.Services
{
    public static class LoginService
    {
        public static async Task<bool> CheckLogin(DataBaseContext db, string login, string password)
        {
            bool response = false;
           UserLoginInfo ?user = await db.UserLoginInfo
                .Where(u => u.Login == login).FirstOrDefaultAsync();
            if (user != null)
            {
                if (user.Password == password)
                {
                    response = true;
                }
            }
            return response;
        }
    }
}

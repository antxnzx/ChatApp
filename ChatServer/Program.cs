using ChatServer.Hubs;
using ChatServer.Models;
using ChatServer.Services;
using Microsoft.EntityFrameworkCore;

namespace ChatServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddCors();
            builder.Services.AddSignalR();
            builder.Services.AddDbContext<DataBaseContext>(opt => opt.UseSqlite("Data Source=db.db;"));

            var app = builder.Build();
            app.UseCors(builder => builder.AllowAnyOrigin());
            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.Map("/login", async (DataBaseContext db, HttpContext context) => {
                Logininfo? info = await context.Request.ReadFromJsonAsync<Logininfo>();
                bool response = false;
                if (info != null)
                {
                    response = await LoginService.CheckLogin(db,info);
                }
                return response;
            });
            
            
            app.Map("/register/{dbid:int}", async (DataBaseContext db, HttpContext context, int dbid) =>
            {
                bool response = false;
                switch (dbid)
                {
                    case 1:
                        UserLoginInfo? info2 = await context.Request.ReadFromJsonAsync<UserLoginInfo>();
                        if (info2 != null)
                        {
                            response = await LoginService.RegisterUser(db, info2);
                        }
                        break;
                    case 2:
                        UserInfo? info = await context.Request.ReadFromJsonAsync<UserInfo>();
                        if (info != null)
                        {
                            response = await LoginService.RegisterUser(db, info);
                        }
                        break;
                }


                return response;

            });


            app.Map("/profile", async (DataBaseContext db, HttpContext context) =>
            {
                UserInfo? user = await DbRequests.GetUserInfo(db, context);
                return user;
                
            });

            app.Map("/subs", async (DataBaseContext db, HttpContext context) =>
            {
                return await DbRequests.GetUserSubs(db, context);
            });

            app.Map("/users", async (DataBaseContext db, HttpContext context) =>
            {
                return await DbRequests.GetUsers(db, context);
            });


            app.Map("/addsub", async (DataBaseContext db, HttpContext context) =>
            {
                return await DbRequests.AddSub(db, context);
            });
            app.Map("/removesub", async (DataBaseContext db, HttpContext context) =>
            {
                return await DbRequests.RemoveSub(db, context);
            });

            app.MapHub<ChatHub>("/chat");
            
            app.Run();
        }
    }
}

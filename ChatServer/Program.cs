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

            app.Map("/login", async (DataBaseContext db, HttpContext context) =>
            {
                Logininfo? info = await context.Request.ReadFromJsonAsync<Logininfo>();
                bool response = false;
                if (info != null)
                {
                    response = await LoginService.CheckLogin(db, info);
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
                return await DbRequests.GetUsersInfos(db, context);
            });


            app.Map("/addsub", async (DataBaseContext db, HttpContext context) =>
            {
                return await DbRequests.CreateSub(db, context);
            });
            app.Map("/removesub", async (DataBaseContext db, HttpContext context) =>
            {
                return await DbRequests.DeleteSub(db, context, -1);
            });


            app.MapGet("/UserLoginInfo", async (DataBaseContext db, HttpContext context) =>
            {
                return await DbRequests.GetUsersLoginInfos(db, context);
            });
            app.MapPost("/UserLoginInfo", async (DataBaseContext db, HttpContext context) =>
            {
                return await DbRequests.CreateUserLoginInfo(db, context);
            });
            app.MapDelete("/UserLoginInfo/{id:int}", async (DataBaseContext db, HttpContext context, int id) =>
            {
                return await DbRequests.DeleteUserLoginInfo(db, context, id);
            });
            app.MapPut("/UserLoginInfo", async (DataBaseContext db, HttpContext context) =>
            {
                return await DbRequests.UpdateUserLoginInfo(db, context);
            });

            //userInfo

            app.MapGet("/UserInfo", async (DataBaseContext db, HttpContext context) =>
            {
                return await DbRequests.GetUsersInfos(db, context);
            });
            app.MapPost("/UserInfo", async (DataBaseContext db, HttpContext context) =>
            {
                return await DbRequests.CreateUserInfo(db, context);
            });
            app.MapDelete("/UserInfo/{id:int}", async (DataBaseContext db, HttpContext context, int id) =>
            {
                return await DbRequests.DeleteUserInfo(db, context, id);
            });
            app.MapPut("/UserInfo", async (DataBaseContext db, HttpContext context) =>
            {
                return await DbRequests.UpdateUserInfo(db, context);
            });

            //usersubs
            app.MapGet("/UserSubs", async (DataBaseContext db, HttpContext context) =>
            {
                return await DbRequests.GetUsersSubs(db, context);
            });
            app.MapPost("/UserSubs", async (DataBaseContext db, HttpContext context) =>
            {
                return await DbRequests.CreateSub(db, context);
            });
            app.MapDelete("/UserSubs/{id:int}", async (DataBaseContext db, HttpContext context, int id) =>
            {
                return await DbRequests.DeleteSub(db, context, id);
            });
            app.MapPut("/UserSubs", async (DataBaseContext db, HttpContext context) =>
            {
                return await DbRequests.UpdateSub(db, context);
            });


            app.MapHub<ChatHub>("/chat");

            app.Run();
        }
    }
}

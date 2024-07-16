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

            builder.Services.AddSignalR();
            builder.Services.AddDbContext<DataBaseContext>(opt => opt.UseSqlite("Data Source=db.db;"));

            var app = builder.Build();

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.Map("/login", async (DataBaseContext db, HttpContext context) => {
                string? login = context.Request.Query["login"];
                string? password = context.Request.Query["password"];
                bool response = false;
                if (!String.IsNullOrWhiteSpace(login)&& !String.IsNullOrWhiteSpace(password))
                {
                    response = await LoginService.CheckLogin(db, login, password);
                }
                return response;
            }); 
            app.MapHub<ChatHub>("/chat");
            
            app.Run();
        }
    }
}

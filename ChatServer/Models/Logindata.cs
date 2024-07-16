using ChatServer.Services;

namespace ChatServer.Models
{
    public record Logindata(DataBaseContext Db, string Login, string Password);
}

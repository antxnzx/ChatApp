namespace ChatClient.Models
{
    public class UserLoginInfo(string UserLogin, string UserPassword)
    {
        public int Id { get; set; }
        public string Login { get; set; } = UserLogin;
        public string Password { get; set; } = UserPassword;
    }
}

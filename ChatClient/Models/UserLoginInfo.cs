namespace ChatClient.Models
{
    public class UserLoginInfo
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        public UserLoginInfo(string UserLogin, string UserPassword)
        {
            Login = UserLogin;
            Password = UserPassword;
        }
        public UserLoginInfo()
        {
        }
    }
}

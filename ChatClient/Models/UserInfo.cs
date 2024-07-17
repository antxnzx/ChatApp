namespace ChatClient.Models
{
    public class UserInfo
    {
        public int Id { get; set; }
        public string UserLogin { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int IsAdmin { get; set; } = 0;
        public UserInfo(string Login, string userName, string userSurname)
        {
            UserLogin = Login;
            Name = userName;
            Surname = userSurname;
        }
        public UserInfo()
        { 

        }
    }
}

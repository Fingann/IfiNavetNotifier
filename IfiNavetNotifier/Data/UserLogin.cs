namespace IfiNavetNotifier
{
    public class UserLogin
    {
        public UserLogin(string username, string password)
        {
            Username = username;
            Password = password;           
        }
        public string Username { get;  }
        public string Password { get;  }
    }
}
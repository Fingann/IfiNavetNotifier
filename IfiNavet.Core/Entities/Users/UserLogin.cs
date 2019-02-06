namespace IfiNavet.Core.Entities.Users
{
    public class UserLogin
    {
        public UserLogin(string username, string password)
        {
            Username = username;
            Password = password;
        }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
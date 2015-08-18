namespace IssueTrackerWebApp.Models
{
    using Helpers;

    public class User
    {
        public User(string username, string password)
        {
            this.UserName = username;
            this.HashPassword = HashUtilities.HashPassword(password);
        }

        public string UserName { get; set; }

        public string HashPassword { get; set; }
    }
}
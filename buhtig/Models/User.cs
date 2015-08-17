namespace IssueTrackerWebApp.Models
{
    using System.Linq;
    using System.Text;
    using System.Security.Cryptography;

    public class User
    {
        public string User_name { get; set; }
        public string Passwort_hash { get; set; }
        public static string HashPassword(string password)
        {
            return string.Join(string.Empty, SHA1.Create().ComputeHash(Encoding.Default.GetBytes(password)).Select(x => x.ToString()));
        }
        public User(string username, string password)
        {
            User_name = username;
            Passwort_hash = HashPassword(password);
        }
    }
}

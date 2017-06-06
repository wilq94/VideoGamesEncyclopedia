namespace VideoGamesEncyclopedia.Models
{
    public class UsersWithRoles
    {
        public UsersWithRoles(string email, string userName, string role)
        {
            EmailAddress = email;
            UserName = userName;
            Role = role;
        }

        public string EmailAddress { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
    }
}
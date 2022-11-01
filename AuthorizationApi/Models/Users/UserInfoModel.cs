namespace AuthorizationApi.Models
{
    /// <summary>
    /// User data
    /// </summary>
    public class UserInfoModel
    {
        public string Login { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}

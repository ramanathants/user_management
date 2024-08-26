namespace IdentityWebAPIAuthentication.Model
{
    public class UserModel
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;

        public List<string> Roles { get; set; } = null!;
    }
}

namespace LinkDev.Talabat.Core.Application.Abstraction.Models.Auth
{
    public class UserDTO
    {
        public required string Id { get; set; }
        public required string DisplayName { get; set; }
        public required string Email { get; set; }
        public required string Token { get; set; }
    }
}

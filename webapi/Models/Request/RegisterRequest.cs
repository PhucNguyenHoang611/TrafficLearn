namespace webapi.Models.Request
{
    public class RegisterRequest
    {
        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public DateTime Birthday { get; set; }

        public string Gender { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;

        public string Provider { get; set; } = null!;

        public string Role { get; set; } = null!;
    }
}
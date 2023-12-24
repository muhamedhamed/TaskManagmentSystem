namespace TaskManagmentSystem.Domain.Entities
{
    public class User
    {
        public string UserId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty; // currently I will consider the email and username the same
        public string Password { get; set; } = string.Empty; // first we will add it as string then we can add hashing
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsActive { get; set; } // Soft delete flag

        // Navigation properties
        // I will not consider the systm has No Navigation properties
    }
}

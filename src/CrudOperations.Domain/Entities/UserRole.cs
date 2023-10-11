namespace CrudOperations.Domain.Entities
{
    public class UserRole
    {
        public int UserId { get; set; } // Замените на Guid
        public User User { get; set; }

        public int RoleId { get; set; } // Замените на Guid
        public Role Role { get; set; }
    }
}

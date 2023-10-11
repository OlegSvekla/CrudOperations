using Sieve.Attributes;
using System.Text.Json.Serialization;

namespace CrudOperations.Domain.Entities
{
    public class User 
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public string Email { get; set; }

        [JsonIgnore]
        public ICollection<UserRole> UserRoles { get; set; }
    }
}

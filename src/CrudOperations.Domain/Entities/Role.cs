﻿using Sieve.Attributes;

namespace CrudOperations.Domain.Entities
{
    public class Role
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }
    }
}

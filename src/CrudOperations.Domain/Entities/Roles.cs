﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudOperations.Domain.Entities
{
    public class Roles
    {
        public int Id { get; set; }
        public const string User = "User";
        public const string Admin = "Admin";
        public const string Support = "Support";
        public const string SuperAdmin = "SuperAdmin";
        public int OnlyUserId { get; set; }
        public User OnlyUser = new();
    }
}

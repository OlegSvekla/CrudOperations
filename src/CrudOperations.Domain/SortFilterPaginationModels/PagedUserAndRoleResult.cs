﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrudOperations.Domain.Entities;

namespace CrudOperations.Domain.SortFilterPaginationModels
{
    public class PagedUserAndRoleResult
    {
        public IEnumerable<User> Users { get; set; }
        public IEnumerable<Role> Roles { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
    }

}

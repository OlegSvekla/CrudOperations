using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudOperations.Domain.SortFilterPaginationModels
{
    public class UserQueryParameters
    {
        public string? UserTerm { get; set; }
        public string? UserSort { get; set; }

        public string? RoleTerm { get; set; }
        public string? RoleSort { get; set; }

        public int Page { get; set; } = 1;
        public int Limit { get; set; } = 10;
    }
}

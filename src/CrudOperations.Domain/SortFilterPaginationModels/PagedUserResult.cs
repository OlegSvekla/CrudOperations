using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrudOperations.Domain.Entities;

namespace CrudOperations.Domain.SortFilterPaginationModels
{
    public class PagedUserResult
    {
        public IEnumerable<User> Users { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
    }
}

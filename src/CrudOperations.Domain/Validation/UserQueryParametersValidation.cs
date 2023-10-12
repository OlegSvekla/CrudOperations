using CrudOperations.Domain.SortFilterPaginationModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudOperations.Domain.Validation
{
    public class UserQueryParametersValidation : AbstractValidator<UserQueryParameters>
    {
        public UserQueryParametersValidation()
        {
            RuleFor(query => query.Page)
                .GreaterThanOrEqualTo(1).WithMessage("Page must be greater than or equal to 1.");

            RuleFor(query => query.Limit)
                .InclusiveBetween(1, 100).WithMessage("Limit must be between 1 and 100.");

            RuleFor(query => query.UserTerm)
                .MaximumLength(50).When(query => !string.IsNullOrWhiteSpace(query.UserTerm))
                .WithMessage("UserTerm must not exceed 50 characters.");

            RuleFor(query => query.UserSort)
                .MaximumLength(50).When(query => !string.IsNullOrWhiteSpace(query.UserSort))
                .WithMessage("UserSort must not exceed 50 characters.");

            RuleFor(query => query.RoleTerm)
                .MaximumLength(50).When(query => !string.IsNullOrWhiteSpace(query.RoleTerm))
                .WithMessage("RoleTerm must not exceed 50 characters.");

            RuleFor(query => query.RoleSort)
                .MaximumLength(50).When(query => !string.IsNullOrWhiteSpace(query.RoleSort))
                .WithMessage("RoleSort must not exceed 50 characters.");
        }
    }
}

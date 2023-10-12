using CrudOperations.Domain.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudOperations.Domain.Validation
{
    public class RoleDtoValidation : AbstractValidator<RoleDto>
    {
        public RoleDtoValidation()
        {
            RuleFor(role => role.Name)
                .NotEmpty().WithMessage("Имя роли не должно быть пустым")
                .MaximumLength(50).WithMessage("Имя роли не должно превышать 50 символов");
        }
    }
}

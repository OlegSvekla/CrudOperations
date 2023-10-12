using CrudOperations.Domain.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudOperations.Domain.Validation
{
    public class UserDtoValidation : AbstractValidator<UserDto>
    {
        public UserDtoValidation()
        {
            RuleFor(user => user.Name).NotEmpty().WithMessage("Имя пользователя является обязательным полем.");
            RuleFor(user => user.Age).GreaterThanOrEqualTo(0).WithMessage("Возраст должен быть положительным числом.");
            RuleFor(user => user.Email).NotEmpty().WithMessage("Email является обязательным полем.")
                .EmailAddress().WithMessage("Email имеет недопустимый формат.");
        }
    }
}

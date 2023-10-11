using CrudOperations.BL.Services.IService;
using CrudOperations.Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CrudOperations.Api.Controllers
{

    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService<PagedUserAndRoleResult> _userService;
        private readonly IValidator<UserQueryParameters> _validator;

        public UserController(
            IUserService<PagedUserAndRoleResult> userService,
            IValidator<UserQueryParameters> validator)
        {
            _userService = userService;
            _validator = validator;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(PagedUserAndRoleResult))]
        [ProducesResponseType(404)]
        public async Task<ActionResult<PagedUserAndRoleResult>> GetAllUsers(UserQueryParameters query)
        {
            var validationResult = await _validator.ValidateAsync(query);
            if (!validationResult.IsValid)
            {
                throw new InvalidValueException(validationResult.ToString());
            }

            var users = await _userService.GetAllUsersAndRoles (
                query.UserTerm,
                query.UserSort,
                query.RoleTerm,
                query.RoleSort,
                query.Page,
                query.Limit);

            return Ok(users);
        }
    }
}

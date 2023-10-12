using CrudOperations.BL.Services.IService;
using CrudOperations.Domain.Dtos;
using CrudOperations.Domain.Entities;
using CrudOperations.Domain.SortFilterPaginationModels;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace CrudOperations.Api.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IFilterService<PagedUserAndRoleResult> _filterService;
        private readonly IValidator<UserQueryParameters> _userQueryValidator;

        private readonly IValidator<UserDto> _userDtoValidator;
        private readonly IValidator<RoleDto> _roleDtoValidator;

        private readonly IUserService<User> _userService;

        public UsersController(
            IFilterService<PagedUserAndRoleResult> filterService,
            IValidator<UserQueryParameters> userQueryValidator,
            IUserService<User> userService,
            IValidator<UserDto> userDtoValidator,
            IValidator<RoleDto> roleDtoValidator)
        {
            _filterService = filterService;
            _userService = userService;
            _userQueryValidator = userQueryValidator;
            _userDtoValidator = userDtoValidator;
            _roleDtoValidator = roleDtoValidator;
        }
      
        /// <returns>Ok response containing books collection.</returns>
        /// <response code="200">Returns the sorted users.</response> 
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(PagedUserAndRoleResult))]
        public async Task<ActionResult<PagedUserAndRoleResult>> GetSortUsers([FromQuery] UserQueryParameters query)
        {
            var validationResult = await _userQueryValidator.ValidateAsync(query);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.ToString());
            }

            var users = await _filterService.SortUsers(
                query.UserTerm,
                query.UserSort,
                query.RoleTerm,
                query.RoleSort,
                query.Page,
                query.Limit);

            return Ok(users);
        }

        /// <remarks>
        /// We have five users and five Id identification key. Enter any number from 1 to 5 inclusive.
        /// </remarks>
        /// <response code="200">Returns one user.</response>
        /// <response code="404">The user with this Id was not found.</response>
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(404)]
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            var user = await _userService.GetUserById(id);

            return user == null ? NotFound("User not found woth this Id") : Ok(user);
        }

        /// <param name="userDto">The User to be created.</param>
        /// <returns>Ok response succesefully created user in DATA.</returns>
        ///<response code="201">User is created.</response>
        [ProducesResponseType(201, Type = typeof(UserDto))]
        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] UserDto userDto)
        {
            var validationResult = await _userDtoValidator.ValidateAsync(userDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.ToString());

            }

            var success = await _userService.AddUser(userDto);

            return success == false ? BadRequest("Your Email is not unique") : Ok();
        }

        /// <param name = "id" > The ID of the user to be updated.</param>
        /// <param name = "userDto" > The updated user data.</param>
        /// <response code = "204" > User is successfuly updated.</response>
        /// <response code="404">The user with this Id was not found.</response>
        [ProducesResponseType(204, Type = typeof(UserDto))]
        [ProducesResponseType(404)]
        [HttpPut("{id:int}/user")]
        public async Task<IActionResult> UpdateUser([FromRoute] int id, [FromBody] UserDto userDto)
        {
            var validationResult = await _userDtoValidator.ValidateAsync(userDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.ToString());
            }

            var success = await _userService.UpdateUser(id, userDto);

            return success == null ? NotFound("User not found woth this Id") : NoContent();
        }

        /// <param name = "newRoleDto" > The updated role for user in data.</param>
        /// <param name = "userId" > The ID of the user to be adde the role.</param>
        /// <response code = "204" > Role is successfuly Aadded.</response>
        /// <response code="404">The user with this Id was not found.</response>
        [ProducesResponseType(404)]
        [ProducesResponseType(204)]
        [HttpPost("{userId:int}/role")]
        public async Task<IActionResult> AddRoleToUser([FromRoute] int userId, [FromBody] RoleDto newRoleDto)
        {
            var validationResult = await _roleDtoValidator.ValidateAsync(newRoleDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.ToString());
            }

            var success = await _userService.AddRoleToUser(userId, newRoleDto);

            return success == 
                false ? NotFound("User not found or this role is already existing in Data") : NoContent();
        }

        /// <param name="id">The ID of the user to be removed.</param>
        /// <response code="404">The book with this Id was not found.</response>
        [ProducesResponseType(404)]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var deleted = await _userService.DeleteUser(id);

            return deleted == null ? NotFound("User not found woth this Id") : NoContent();
        }
    }
}

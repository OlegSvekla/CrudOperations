using CrudOperations.BL.Services.IService;
using CrudOperations.Domain.Entities;
using FluentValidation;
using LibraryAPI.Domain.Exeptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CrudOperations.Api.Controllers
{

    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IFilterService<PagedUserAndRoleResult> _filterService;
        private readonly IValidator<UserQueryParameters> _userQueryValidator;

        private readonly IValidator<User> _userValidator;
        private readonly IUserService<User> _userService;

        public UserController(
            IFilterService<PagedUserAndRoleResult> filterService,
            IValidator<UserQueryParameters> userQueryValidator,
            IUserService<User> userService,
            IValidator<User> userValidator)
        {
            _filterService = filterService;
            _userService = userService;
            _userQueryValidator = userQueryValidator;
            _userValidator = userValidator;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(PagedUserAndRoleResult))]
        [ProducesResponseType(404)]
        public async Task<ActionResult<PagedUserAndRoleResult>> GetAllUsers([FromQuery] UserQueryParameters query)
        {
            var validationResult = await _userQueryValidator.ValidateAsync(query);
            if (!validationResult.IsValid)
            {
                throw new InvalidValueException(validationResult.ToString());
            }

            var users = await _filterService.GetAllUsersAndRoles (
                query.UserTerm,
                query.UserSort,
                query.RoleTerm,
                query.RoleSort,
                query.Page,
                query.Limit);

            return Ok(users);
        }

        /// <summary>
        /// Gets the book by its own Id
        /// </summary>   
        /// <param name="id">ID of the Book to get.</param>
        /// <returns>Ok response containing a single book.</returns>
        /// <remarks>
        /// We have five books and five Id identification key. Enter any number from 1 to 5 inclusive.
        /// </remarks>
        /// <response code="200">Returns one book.</response>
        /// <response code="404">The book with this Id was not found.</response>
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(404)]
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            var user = await _userService.GetUserById(id);

            return user == null ? NotFound() : Ok(user);
        }

        /// <summary>
        /// Add book
        /// </summary>   
        /// <param name="bookDto">The Book to be created.</param>
        /// <returns>Ok response succesefully created book in DATA.</returns>
        /// <response code="201">Book is created.</response>
        [ProducesResponseType(201, Type = typeof(User))]
        [HttpPost]
        public async Task<IActionResult> AddBook(User user)
        {
            var validationResult = await _userValidator.ValidateAsync(user);
            if (!validationResult.IsValid)
            {
                throw new InvalidValueException(validationResult.ToString());
            }
            var success = await _userService.AddUser(user);

            return success == false? BadRequest("Ошибка добавления пользователя") : Ok(); 
        }













    }
}

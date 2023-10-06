using CrudOperations.BL.Services.IService;
using CrudOperations.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CrudOperations.Api.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService<User> _userService;

        public UserController(IUserService<User> userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Gets the list of books
        /// </summary>        
        /// <returns>Ok response containing books collection.</returns>
        /// <response code="200">Returns the list of books.</response> 
        /// <response code="404">The base is Empty. Books weren't found</response>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IList<User>))]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IList<User>>> GetAllUsers()
        {
            var users = await _userService.GetAllUsers();

            return Ok(users);
        }


    }
}

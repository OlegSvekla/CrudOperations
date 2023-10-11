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
        private readonly IUserService<PagedUserAndRoleResult> _userService;

        public UserController(IUserService<PagedUserAndRoleResult> userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(PagedUserAndRoleResult))]
        [ProducesResponseType(404)]
        public async Task<ActionResult<PagedUserAndRoleResult>> GetAllUsers(
            string userTerm = null, 
            string userSort = null, 
            string roleTerm = null,
            string roleSort = null,
            int page = 1,
            int limit = 10)
        {
            var users = await _userService.GetAllUsersAndRoles (userTerm,  userSort,  roleTerm,  roleSort,  page,  limit);

            return Ok(users);
        }
    }
}

using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service;
using Service.Exceptions;
using System;
using System.Threading.Tasks;
namespace NewsAPI.Controllers
{
    /// <summary>
    /// Api controller for handling Http requests regarding user entity
    /// </summary>
    [Route("/api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        /// <summary>
        /// readonly property for the service class required
        /// </summary>
        readonly IUserService userService;

        /// <summary>
        /// Paramterised constructor for injecting service class property
        /// </summary>
        /// <param name="userService"></param>
        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        /// <summary>
        /// Method to get details based on the user id
        /// </summary>
        /// <param name="userId">The id of the user whose details is to be fetched</param>
        /// <returns>The user profile object for the user with id mentioned</returns>
        /// <response code="200">If the user details were fetched successfuly</response>
        /// <response code="404">If the user details were not found</response>
        /// <response code="500">If some error occurred</response>
        [HttpGet("{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(string userId)
        {
            return Ok(await userService.GetUser(userId));
        }


        /// <summary>
        /// Method for adding a user
        /// </summary>
        /// <param name="user">The user profile object that is to be added</param>
        /// <returns>True if user was added successfully</returns>
        /// <response code="201">If user was added successfuly</response>
        /// <response code="409">If user already exists</response>
        /// <response code="500">If some error occurred</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post(UserProfile user)
        {
            bool added = await userService.AddUser(user);
            return Created("api/user", added);
        }

        /// <summary>
        /// Method for updating user details
        /// </summary>
        /// <param name="id">The id of the user whose details are to be updated</param>
        /// <param name="user">The modified user profile details</param>
        /// <returns>True if user was updated</returns>
        /// <response code="200">If user was updated successfuly</response>
        /// <response code="404">If user was not found</response>
        /// <response code="500">If some error occurred</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(string id, UserProfile user)
        {
            return Ok(await userService.UpdateUser(id, user));
        }

        /// <summary>
        /// Method to delete a user
        /// </summary>
        /// <param name="id">The id of the user to be deleted</param>
        /// <returns>True if user was deleted</returns>
        /// <response code="200">If user was deleted successfuly</response>
        /// <response code="404">If user was not found</response>
        /// <respones code="500">If some error occurred</respones>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(string id)
        {
            return Ok(await userService.DeleteUser(id));
        }
    }
}

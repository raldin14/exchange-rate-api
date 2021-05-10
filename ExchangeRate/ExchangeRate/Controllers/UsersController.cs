using ExchangeRate.Data.Services;
using ExchangeRate.Data.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExchangeRate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private UsersServices _usersServices;

        public UsersController(UsersServices usersServices)
        {
            _usersServices = usersServices;
        }

        [HttpGet("get-all-users")]
        public IActionResult GetAllUsers()
        {
            var allUsers = _usersServices.GetAllUsers();

            return Ok(allUsers);
        }

        [HttpGet("get-user-by-user/{id}")]
        public IActionResult GetUserById(int id)
        {
            var userById = _usersServices.GetUserById(id);
            return Ok(userById);
        }

        [HttpPost("add-user")]
        public IActionResult AddUser([FromBody] UserVM user)
        {
            _usersServices.AddUser(user);
            return Ok();
        }
    }
}

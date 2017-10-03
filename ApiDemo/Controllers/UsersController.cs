using ApiDemo.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace ApiDemo.Controllers
{
    public class UsersController : ApiController
    {

        private IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet, Route("api/v1/Users/GetAll")]
        public async Task<IHttpActionResult> GetAll()
        { 
            var users = await userService.GetAllUsersAsync(); 
            return Ok(users);
        }

        [HttpGet, Route("api/v1/Users/GetByName/{name}")]
        public async Task<IHttpActionResult> GetByName(string name)
        {
            var users = await userService.GetUsersByNameAsync(name);
            return Ok(users);
        }

        [HttpGet, Route("api/v1/Users/GetByEmail/{email}")]
        public async Task<IHttpActionResult> GetByEmail(string email)
        {
            var users = await userService.GetUserByEmail(email);
            return Ok(users);
        }

    }

}
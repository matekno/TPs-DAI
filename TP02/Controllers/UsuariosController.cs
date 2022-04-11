using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pizzas.API.Models;
using Pizzas.API.Utils;
using Pizzas.API.Services;

namespace Pizzas.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        [HttpPost]
        public IActionResult Login(Login u)
        {
            var user = UserService.Login(u.UserName, u.Password);
            if (user is Usuario)
            {
                return Ok(user);
            }
            else
            {
                return Unauthorized(user);
            }
        }


    }
}
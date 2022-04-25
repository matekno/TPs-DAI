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
            IActionResult toReturn = null;
            Usuario user = null;
            try
            {
                user = UserService.Login(u.UserName, u.Password);
            }
            catch (Exception ex)
            {
                string s = CustomLog.GetLogError(ex);
                CustomLog.WriteLogByAppSetting(s);
            }
            finally
            {
                if (user is Usuario)
                {
                    toReturn = Ok(user);
                }
                else
                {
                    toReturn = Unauthorized(user);
                }
            }
            return toReturn;

        }


    }
}
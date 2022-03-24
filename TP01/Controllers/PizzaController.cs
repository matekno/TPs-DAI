using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pizzas.API.Models;
using Pizzas.API.Utils;

namespace Pizzas.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PizzaController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            var pizzas = DB.GetAll();
            return Ok(pizzas);
        }

        [HttpGet("{id}")]
        public IActionResult GetByID(int id)
        {
            var p = DB.GetById(id); // no anda :C
            return Ok(p);
        }

        [HttpPost]

        public IActionResult Create(Pizza p)
        {
            var afRows = DB.Create(p);
            if (afRows == 1)
            {
                return Ok(p); // aca hay que hacer un 201 en realidad
            }
            else
            {
                return BadRequest(); // o errror
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateById(int id, Pizza p)
        {
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteById(int id)
        {
            return BadRequest();
        }
    }
}

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

        public IActionResult Create(Pizza p) //no se por que pero se crean con un id raro..
        {
            var created = DB.Create(p);
            var afRows = created.Item1;
            var idSql = created.Item2;

            if (afRows == 1)
            {
                return Created($"localhost:5000/api/Pizza/{idSql}", p); // en principio puse localhost. habria que hacer una variable general?
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
            var afRows = DB.Delete(id);
            if (afRows == 1)
            {
                return Ok($"Affected rows: {afRows}. Deleted pizza with ID {id}" ); // en principio puse localhost. habria que hacer una variable general?
            }
            else
            {
                return BadRequest(); // o errror
            }
            return BadRequest();
        }
    }
}

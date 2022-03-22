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
    [Route("[api]")]
    public class PizzaController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            return BadRequest();
        }

        [HttpGet("{id}")]
        public IActionResult GetByID(int id)
        {
            DB.GetById(id); // no anda :C
            return BadRequest();
        }

        [HttpPost]

        public IActionResult Create(Pizza p)
        {
            return BadRequest();
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

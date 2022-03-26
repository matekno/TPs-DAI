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

        public IActionResult Create(Pizza p) // no creo que este hecho de la manera mas optima, porque hago dos queries a la db, pero bueno...
        {
            var created = DB.Create(p);
            var afRows = created.Item1;
            var idSql = created.Item2;

            if (afRows == 1)
            {
                return Created($"localhost:5000/api/Pizza/{idSql}", new {id = idSql, nombre = p.Nombre, libreGluten = p.LibreGluten, importe = p.Importe, descripcion = p.Descripcion}); // en principio puse localhost. habria que hacer una variable general?
            }
            else
            {
                return BadRequest(); // o errror
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateById(int id, Pizza p)
        {
            //no termino de entender el caso para que mande un BadRequest..
            if (id != p.Id)
            {
                return BadRequest($"El ID del body ({p.Id}) es distinto al del request ({id})..");
            }
            var affRows = DB.Update(id, p);
            if (affRows == 1)
            {
                return Ok(p);
            }
            else
            {
                return NotFound($"No se encontro la pizza con el ID {id}");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteById(int id) 
        {
            if (id <= 0)
            {
                return BadRequest("ID es menor o igual a 0..");
            }
            else
            {
                var afRows = DB.Delete(id);
                if (afRows == 0)
                {
                    return NotFound("El ID no corresponde a ninguna pizza.."); 

                }
                else
                {
                    return Ok($"Affected rows: {afRows}. Deleted pizza with ID {id}" ); // en principio puse localhost. habria que hacer una variable general?

                }
            }
        }
    }
}

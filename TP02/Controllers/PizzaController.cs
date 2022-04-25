using System;
using Microsoft.AspNetCore.Mvc;
using Pizzas.API.Models;
using Pizzas.API.Services;
using Pizzas.API.Utils;

namespace Pizzas.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PizzasController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            string token = Request.Headers["token"];
            var isValid = UserService.IsValidToken(token);

            if (isValid == true)
            {
                try
                {
                    var pizzas = PizzaService.GetAll();
                    return Ok(pizzas);
                }
                catch (Exception ex)
                {
                    string s = CustomLog.GetLogError(ex);
                    CustomLog.WriteLogByAppSetting(s);
                    return Problem(s);
                }
            }
            else
            {
                return Unauthorized(token);
            }
        }

        [HttpGet("{id:int}")]
        public IActionResult GetByID(int id)
        {
            string token = Request.Headers["token"];
            var isValid = UserService.IsValidToken(token);
            if (isValid == true)
            {
                var p = PizzaService.GetById(id);
                return Ok(p);
            }
            else
            {
                return Unauthorized(token);
            }
        }

        [HttpPost]
        public IActionResult Create(Pizza p) // TODO No esta testeado este metodo
        {
            string token = Request.Headers["token"];
            var isValid = UserService.IsValidToken(token);
            if (isValid == true)
            {
                var idP = PizzaService.Create(p);
                if (idP is int)
                {
                    return CreatedAtAction(nameof(Create), new { id = idP, nombre = p.Nombre, libreGluten = p.LibreGluten, importe = p.Importe, descripcion = p.Descripcion }, p);
                }
                else
                {
                    return BadRequest(); // o errror
                }
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateById(int id, Pizza p) // TODO No esta testeado este metodo
        {
            if (id != p.Id)
            {
                return BadRequest($"El ID del body ({p.Id}) es distinto al del request ({id})..");
            }
            else
            {
                string token = Request.Headers["token"];
                var isValid = UserService.IsValidToken(token);
                if (isValid == true)
                {
                    var affRows = PizzaService.Update(id, p);
                    if (affRows == 1)
                    {
                        return Ok(p);
                    }
                    else
                    {
                        return NotFound($"No se encontro la pizza con el ID {id}");
                    }
                }
                else
                {
                    return Unauthorized(token);
                }
            }
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteById(int id)
        {
            if (id <= 0)
            {
                return BadRequest("ID es menor o igual a 0..");
            }
            else
            {
                string token = Request.Headers["token"];
                var isValid = UserService.IsValidToken(token);
                if (isValid == true)
                {
                    var afRows = PizzaService.Delete(id);
                    if (afRows == 0)
                    {
                        return NotFound("El ID no corresponde a ninguna pizza..");

                    }
                    else
                    {
                        return Ok($"Affected rows: {afRows}. Deleted pizza with ID {id}"); // en principio puse localhost. habria que hacer una variable general?

                    }
                }
                else
                {
                    return Unauthorized(token);
                }
            }
        }
    }
}
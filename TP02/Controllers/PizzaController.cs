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
                try
                {
                    var p = PizzaService.GetById(id);
                    return Ok(p);
                }
                catch (Exception ex)
                {
                    string s = CustomLog.GetLogError(ex, $"Error con la pizza ID: {id}");
                    CustomLog.WriteLogByAppSetting(s);
                    return Problem(s);
                }
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

            if (p is not Pizza)
            {
                // si le faltan arrgumentos seria
                return BadRequest(p);
            }
            else
            {
                if (isValid == true)
                {
                    try
                    {
                        var idP = PizzaService.Create(p);
                        return CreatedAtAction(nameof(Create), new { id = idP, nombre = p.Nombre, libreGluten = p.LibreGluten, importe = p.Importe, descripcion = p.Descripcion }, p);
                    }
                    catch (Exception ex)
                    {
                        string s = CustomLog.GetLogError(ex, p);
                        CustomLog.WriteLogByAppSetting(s);
                        return Problem(s);

                    }
                }
                else
                {
                    return Unauthorized();
                }
            }


        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateById(int id, Pizza p)
        {
            if (id != p.Id)
            {
                string s = CustomLog.GetLogError($"El ID del body ({p.Id}) es distinto al del request ({id})..");
                return BadRequest(s);
            }
            else
            {
                string token = Request.Headers["token"];
                var isValid = UserService.IsValidToken(token);
                if (isValid == true)
                {
                    try
                    {
                        PizzaService.Update(id, p);
                        return Ok(p);
                    }
                    catch (Exception ex)
                    {
                        string s = CustomLog.GetLogError(ex, p);
                        CustomLog.WriteLogByAppSetting(s);
                        return Problem(s);
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
                    try
                    {
                        PizzaService.Delete(id);
                        return Ok($"Deleted pizza with ID {id}");
                    }
                    catch (Exception ex)
                    {
                        string s = CustomLog.GetLogError(ex, $"Se recibio la pizza con ID {id}");
                        CustomLog.WriteLogByAppSetting(s);
                        return Problem(s);
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
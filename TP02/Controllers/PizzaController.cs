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
                    return Problem(s);
                }
            }
            else
            {
                string s = CustomLog.GetLogError($"Token no valido: \n", token);
                CustomLog.WriteLogByAppSetting(s);
                return Unauthorized(s);
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
                    return Problem(s);
                }
            }
            else
            {
                string s = CustomLog.GetLogError($"Token no valido: \n", token);
                CustomLog.WriteLogByAppSetting(s);
                return Unauthorized(s);
            }
        }


        [HttpPost]
        public IActionResult Create(Pizza p)
        {
            string token = Request.Headers["token"];
            var isValid = UserService.IsValidToken(token);

            if (p is not Pizza)
            {
                // si le faltan arrgumentos seria
                string s = CustomLog.GetLogError($"Faltan argumentos: \n", p);
                CustomLog.WriteLogByAppSetting(s);
                return BadRequest(p);

            }
            else
            {
                if (isValid == true)
                {
                    try
                    {
                        var idP = PizzaService.Create(p);
                        return CreatedAtAction(nameof(Create), new {id = idP, nombre = p.Nombre, libreGluten = p.LibreGluten, importe = p.Importe, descripcion = p.Descripcion}, p);
                    }
                    catch (Exception ex)
                    {
                        string s = CustomLog.GetLogError(ex, p);
                        return Problem(s);
                    }
                }
                else
                {
                    string s = CustomLog.GetLogError($"Token no valido: \n", token);
                    CustomLog.WriteLogByAppSetting(s);
                    return Unauthorized(s);
                }
            }
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateById(int id, Pizza p)
        {
            if (id != p.Id)
            {
                return BadRequest(CustomLog.GetLogError($"El ID del body ({p.Id}) es distinto al del request ({id})..", p));
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
                        return Problem(s);
                    }
                }
                else
                {
                    string s = CustomLog.GetLogError($"Token no valido: \n", token);
                    CustomLog.WriteLogByAppSetting(s);
                    return Unauthorized(s);
                }
            }
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteById(int id)
        {
            if (id <= 0)
            {
                string s = CustomLog.GetLogError($"ID ({id}) es menor o igual a 0..");
                CustomLog.WriteLogByAppSetting(s);
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
                        PizzaService.Delete(id);
                    }
                    catch (Exception ex)
                    {
                        return Problem(CustomLog.GetLogError(ex.Message));
                    }
                    return Ok($"Deleted pizza with ID {id}");

                }
                else
                {
                    string s = CustomLog.GetLogError($"Token no valido: \n", token);
                    CustomLog.WriteLogByAppSetting(s);
                    return Unauthorized(s);
                }
            }
        }
    }
}
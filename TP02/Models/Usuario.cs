using System;

namespace Pizzas.API.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Token {get; set;}
        public DateTime TokenExpirationDate { get; set; }
        

        // public Usuarios(int Id_,string Nombre_,string Apellido_,DateTime TokenExpirationDate_)
        // {
        // 	this.Id = Id_;
        // 	this.Nombre = Nombre_;
        // 	this.Apellido = Apellido_;
        // 	this.TokenExpirationDate = TokenExpirationDate_;
        // }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pizzas.API.Models;
using System.Data.SqlClient;
using Dapper;
using Pizzas.API.Utils;

namespace Pizzas.API.Services
{
    public class UserService
    {
        public static Usuario Login(string userName, string pwd)
        {
            var user = GetUsuario(userName, pwd);
            if (user.Password == pwd)// si el usuario existe
            {
                return user;
            }
            else
            {
                return null;
            }
        }

        public static Usuario GetUsuario(string userName, string pwd){
            var user = new Usuario();
            using (SqlConnection db = DB.GetConnection())
            {
                string sql = "SELECT * FROM [DAI-Pizzas].dbo.Usuarios WHERE Usuarios.UserName = @oUserName AND Usuarios.Password = @oPwd";
                user = db.QueryFirstOrDefault<Usuario>(sql, new { oUserName = userName, oPwd = pwd });
            }
			return user;
        }

        // public static Usuario RefreshToken(){

        // }
    }


}
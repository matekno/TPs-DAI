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
            if (user is Usuario)// si el usuario existe
            {
                var token = RefreshToken(user.Id);
                if (token != null) 
                {
                    user = GetUsuario(userName, pwd); // volvemos a pedir el usuario, refrescando token
                    return user;

                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public static Usuario GetUsuario(string userName, string pwd)
        {
            var user = new Usuario();
            using (SqlConnection db = DB.GetConnection())
            {
                string sql = "SELECT * FROM [DAI-Pizzas].dbo.Usuarios WHERE Usuarios.UserName = @oUserName AND Usuarios.Password = @oPwd";
                user = db.QueryFirstOrDefault<Usuario>(sql, new { oUserName = userName, oPwd = pwd });
            }
            if (user is Usuario)
            {
                return user;
            }
            else
            {
                return null;
            }
        }

        public static Usuario GetUsuarioByToken(string token)
        {
            var user = new Usuario();
            using (SqlConnection db = DB.GetConnection())
            {
                string sql = "SELECT * FROM [DAI-Pizzas].dbo.Usuarios WHERE Usuarios.Token = @oToken";
                user = db.QueryFirstOrDefault<Usuario>(sql, new { oToken = token });
            }
            return user;
        }

        public static string RefreshToken(int id)
        {
            string token = Guid.NewGuid().ToString();
            int affectedRows;

            using (SqlConnection db = DB.GetConnection())
            {
                string sql = "UPDATE [DAI-Pizzas].dbo.Usuarios SET Token = @oToken, TokenExpirationDate = DateAdd(MINUTE, 15, GetDate()) WHERE Usuarios.Id = @oId";
                affectedRows = db.Execute(sql, new { oToken = token, oId = id });
            }
            if (affectedRows == 1)
            {
                return token;
            }
            else
            {
                return null;
            }
        }
    }


}
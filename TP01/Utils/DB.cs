using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using Pizzas.API.Models;
using System.Data.SqlClient;
using Dapper;

namespace Pizzas.API.Utils
{
    public class DB
    {
        private static string _connectionString = @"Server=A-CAZ-07;Database=DAI-Pizzas;User ID=Pizzas;Password=Pizzas;";
        public static Pizza GetById(int id)
        {
            Pizza p = null;
            using (SqlConnection db = new SqlConnection(_connectionString))
            {
                string sql = "SELECT * FROM [DAI-Pizzas].[dbo].[Pizzas] WHERE [Pizzas].Id = @oId";
                p = db.QueryFirstOrDefault<Pizza>(sql, new { oId = id });
            }
            return p;
        }

        public static IEnumerable<Pizza> GetAll()
        {
            List<Pizza> pizzas = null;
            using (SqlConnection db = new SqlConnection(_connectionString))
            {
                string sql = "SELECT * FROM [DAI-Pizzas].dbo.Pizzas";
                pizzas = db.Query<Pizza>(sql).ToList();
            }
            return pizzas;
        }

        public static int Create(Pizza p)
        {
            int affectedRows;

            using (SqlConnection db = new SqlConnection(_connectionString))
            {
                string sql = "INSERT [DAI-Pizzas].dbo.[Pizzas]([Nombre], [LibreGluten], [Importe], [Descripcion]) VALUES( @oNombre, @oLibreGluten, @oImporte, @oDescripcion)";
                affectedRows = db.Execute(sql, new{ oNombre = p.Nombre, oLibreGluten = p.LibreGluten, oImporte = p.Importe, oDescripcion = p.Descripcion}); // no andan oNombre ni oDescripcion
            }
            return affectedRows;
        }

    }
}

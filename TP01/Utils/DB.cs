using System;
using Pizzas.API.Models;
using System.Data.SqlClient;
using Dapper;

namespace Pizzas.API.Utils
{   
    public class DB
    {
        private static string _connectionString = @"Server=A-BTA-03;Database=DAI-Pizzas;User=Pizzas;Password=Pizzas;";
        public static Pizza GetById(int id)
        {
            Pizza p = null;
            using (SqlConnection db = new SqlConnection(_connectionString))
            {
                string sql = "SELECT * FROM [DAI-Pizzas].[dbo].[Pizzas] WHERE [Pizzas].Id = @oId";
                p = db.QueryFirstOrDefault<Pizza>(sql, new {oId = id});
            }
            return p;
        }

    }
}

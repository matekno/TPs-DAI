using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using Dapper;
using Pizzas.API.Models;
using Pizzas.API.Utils;

namespace Pizzas.API.Services
{
    public class PizzaService
    {
        public static Pizza GetById(int id)
        {
            Pizza p = null;
            using (SqlConnection db = DB.GetConnection())
            {
                string sql = "GetByID";
                p = db.QueryFirstOrDefault<Pizza>(sql, new { Id = id }, commandType: CommandType.StoredProcedure);
            }
            return p;
        }

        public static IEnumerable<Pizza> GetAll()
        {
            List<Pizza> pizzas = null;
            using (SqlConnection db = DB.GetConnection())
            {
                string sql = "GetAll";
                pizzas = db.Query<Pizza>(sql, commandType: CommandType.StoredProcedure).ToList();
            }
            return pizzas;
        }

        public static (int, int) Create(Pizza p)
        {
            int affectedRows;
            int id;

            using (SqlConnection db = DB.GetConnection())
            {
                string sql = "CreatePizza";
                affectedRows = db.Execute(sql, new
                {
                    Nombre = p.Nombre,
                    LibreGluten = p.LibreGluten,
                    Importe = p.Importe,
                    Descripcion = p.Descripcion
                }, commandType: CommandType.StoredProcedure);

                // no termino de entender si para un status code Created necesito mandar la forma de acceder. por las dudas lo hice asi jaja
                id = db.QuerySingle<int>("SELECT CAST(SCOPE_IDENTITY() AS INT)");
            }
            return (affectedRows, id);
        }
        
        public static int Delete(int id)
        {
            int affectedRows;

            using (SqlConnection db = DB.GetConnection())
            {
                string sql = "DeletePizza";
                affectedRows = db.Execute(sql, new { Id = id }, commandType: CommandType.StoredProcedure);
            }
            return affectedRows;
        }

        public static int Update(int id, Pizza p)
        {
            string sql = "UpdatePizza";
            int affectedRows;
            using (SqlConnection db = DB.GetConnection())
            {
                affectedRows = db.Execute(sql, new
                {
                    Nombre = p.Nombre,
                    LibreGluten = p.LibreGluten,
                    Importe = p.Importe,
                    Descripcion = p.Descripcion,
                    Id = id
                }, commandType: CommandType.StoredProcedure);
            }
            return affectedRows;
        }
    }
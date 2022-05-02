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
    public static class PizzaService
    {
        public static Pizza GetById(int id)
        {
            Pizza p = null;

            try
            {
                using (SqlConnection db = DB.GetConnection())
                {
                    string sql = "GetByID";
                    p = db.QueryFirstOrDefault<Pizza>(sql, new {Id = id}, commandType: CommandType.StoredProcedure);
                }
            }
            catch (System.Exception ex)
            {
                string s = CustomLog.GetLogError(ex.Message);
                CustomLog.WriteLogByAppSetting(s);
                throw;
            }


            return p;
        }

        public static IEnumerable<Pizza> GetAll()
        {
            List<Pizza> pizzas = null;
            try
            {
                using (SqlConnection db = DB.GetConnection())
                {
                    string sql = "GetAll";
                    pizzas = db.Query<Pizza>(sql, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                string s = CustomLog.GetLogError(ex.Message);
                CustomLog.WriteLogByAppSetting(s);
                throw;
            }


            return pizzas;
        }


        public static int Create(Pizza p)
        {
            int id = -1;
            try
            {
                using (SqlConnection db = DB.GetConnection())
                {
                    string sql = "CreatePizza";
                    id = db.QuerySingle<int>(sql, new
                    {
                        Nombre = p.Nombre,
                        LibreGluten = p.LibreGluten,
                        Importe = p.Importe,
                        Descripcion = p.Descripcion
                    }, commandType: CommandType.StoredProcedure);
                }


                p.Id = id;
            }
            catch (Exception ex)
            {
                string s = CustomLog.GetLogError(ex, $"Error con la pizza ID: {id}");
                CustomLog.WriteLogByAppSetting(s);
                throw;
            }


            return id;
        }

        public static void Delete(int id)
        {
            int affectedRows;
            try
            {
                using (SqlConnection db = DB.GetConnection())
                {
                    string sql = "DeletePizza";
                    affectedRows = db.Execute(sql, new {Id = id}, commandType: CommandType.StoredProcedure);
                }
                
                if (affectedRows == 0)
                {
                    throw new Exception($"No se pudo borrar la pizza con ID {id}");
                }

            }
            catch (Exception ex)
            {
                string s = CustomLog.GetLogError(ex, $"No se pudo borrar la pizza con ID {id}");
                CustomLog.WriteLogByAppSetting(s);
                throw;
            }
        }

        public static void Update(int id, Pizza p)
        {
            string sql = "UpdatePizza";
            int affectedRows;

            try
            {
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
            }
            catch (Exception ex)
            {
                string s = CustomLog.GetLogError(ex, $"No se pudo actualizar la pizza con ID {id}");
                CustomLog.WriteLogByAppSetting(s);
                throw;
            }
        }
    }
}
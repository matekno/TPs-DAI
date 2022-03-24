using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Pizzas.API.Models;

namespace Pizzas.API.Utils 
{
	public class DB 
	{
		private static string
		_connectionString = @"Server=DESKTOP-FQBUJMB\SQLEXPRESS;Database=DAI-Pizzas;User ID=Pizzas;Password=Pizzas;";

		public static Pizza GetById(int id) {
			Pizza p = null;
			using(SqlConnection db = new SqlConnection(_connectionString)) {
				string sql = "SELECT * FROM [DAI-Pizzas].[dbo].[Pizzas] WHERE [Pizzas].Id = @oId";
				p = db.QueryFirstOrDefault < Pizza > (sql, new {
					oId = id
				});
			}

			return p;
		}

		public static IEnumerable < Pizza > GetAll() {
			List < Pizza > pizzas = null;
			using(SqlConnection db = new SqlConnection(_connectionString)) {
				string sql = "SELECT * FROM [DAI-Pizzas].dbo.Pizzas";
				pizzas = db.Query < Pizza > (sql).ToList();
			}

			return pizzas;
		}

		public static(int, int) Create(Pizza p) {
			int affectedRows;
			int id;

			using(SqlConnection db = new SqlConnection(_connectionString)) {
				string sql = "INSERT [DAI-Pizzas].dbo.[Pizzas]([Nombre], [LibreGluten], [Importe], [Descripcion]) VALUES( @oNombre, @oLibreGluten, @oImporte, @oDescripcion)";
				affectedRows = db.Execute(sql, new {
					oNombre = p.Nombre,
					oLibreGluten = p.LibreGluten,
					oImporte = p.Importe,
					oDescripcion = p.Descripcion
				});

				// no termino de entender si para un status code Created necesito mandar la forma de acceder. por las dudas lo hice asi jaja
				id = db.QueryFirstOrDefault < int > ("SELECT TOP 1 * FROM [DAI-Pizzas].dbo.Pizzas ORDER BY ID DESC");
			}

			return (affectedRows, id);
		}

		public static int Delete(int id) 
		{
			/**/
			int affectedRows;

			using(SqlConnection db = new SqlConnection(_connectionString)) {
				string sql = "DELETE FROM [DAI-Pizzas].dbo.Pizzas WHERE Pizzas.Id=@oId";
				affectedRows = db.Execute(sql, new {oId = id});
			}
			return affectedRows;
		}
	}
}

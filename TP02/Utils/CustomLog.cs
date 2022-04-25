using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using Pizzas.API.Helpers;

namespace Pizzas.API.Utils
{
    public class CustomLog
    {
        public static void WriteLogByAppSetting(string log)
        {
            string path = ConfigurationHelper.GetLogFolder();
            IOHelpers.AppendInFile("errores.txt", log, path);
        }


        public static string AddNewLineToLog()
        {
            string a = Environment.NewLine;
            a += "-----------------------------------------------------------";
            return a;
        }
        public static string GetLogError()
        {
            DateTime dateTime = DateTime.Now;
            string s = $"HORA:{DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt")}";
            s += AddNewLineToLog();
            return s;
        }

        public static string GetLogError(Exception ex)
        {
            string s = GetLogError();
            s += $"ERROR: {ex.Message}";
            s += AddNewLineToLog();
            return s;
        }

        public static string GetLogError(string errorData)
        {
            string s = GetLogError();
            s += $"ERROR: {errorData}";
            s += AddNewLineToLog();
            return s;
        }
        public static string GetLogError(Exception ex, object datos)
        {
            string o = JsonSerializer.Serialize(datos);
            string s = GetLogError(ex);
            s += $"DATOS: {o}";
            s += AddNewLineToLog();
            return s;
        }

        public static string GetLogError(string errorData, object datos)
        {
            string s = GetLogError(errorData);
            s += $"DATOS: {datos}";
            s += AddNewLineToLog();
            return s;
        }
    }
}
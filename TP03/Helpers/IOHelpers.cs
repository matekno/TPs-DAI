using System;
using System.IO;

namespace Pizzas.API.Helpers
{
    public static class IOHelpers
    {
        public static bool AppendInFile(string fileName, string data, string path)
        {
            string fullPath = Path.Combine(path, fileName);
            bool hasWritten = false;

            // si el archivo no existe, lo crea.
            if (!File.Exists(fullPath))
            {
                try
                {
                    using (StreamWriter sw = File.AppendText(fullPath))
                    {
                        sw.Write(data);
                    }
                }
                catch (UnauthorizedAccessException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (DirectoryNotFoundException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            // si el archivo ya existe, escribe el valor de data ahi.
            else
            {

                try
                {
                    using (StreamWriter sw = File.AppendText(fullPath))
                    {
                        sw.Write(data);
                        hasWritten = true;
                    }
                }
                catch (Exception ex)
                {
                    hasWritten = false;
                    Console.WriteLine(ex.Message);
                }
            }
            return hasWritten;
        }
    }
}
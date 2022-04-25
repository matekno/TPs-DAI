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

            FileStream logFile = null;

            // si el archivo no existe, lo crea.
            if (!File.Exists(fullPath))
            {
                try
                {
                    logFile = File.Create(fullPath);
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
                if (logFile is FileStream) // si el archivo fue creado correctamente aprox
                {
                    try
                    {
                        using (var logWriter = new StreamWriter(logFile))
                        {
                            logWriter.Write(data);
                            logWriter.Dispose();
                            hasWritten = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            return hasWritten;
        }
    }
}
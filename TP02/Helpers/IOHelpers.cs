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
                    throw;
                    // throw new ("El path es readonly, esta oculto o el usuario no tiene acceso.";
                }
                catch (ArgumentException ex)
                {
                    throw;
                }
                catch (DirectoryNotFoundException)
                {
                    throw;
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
                    catch (Exception)
                    {
                        throw;

                    }
                }
            }
            return hasWritten;
        }
    }
}
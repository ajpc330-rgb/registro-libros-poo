using System;
using System.IO;
using System.Text;

namespace RegistroLibrosPOO
{
    // Clase que representa la entidad Libro según los requisitos del ejercicio
    class Libros
    {
        public string Codigo { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public int Anio { get; set; }
        public string Editorial { get; set; }
        public int NumeroPaginas { get; set; }

        // Constructor vacío para instanciar y asignar propiedades luego
        public Libros() { }

        // Formatea los datos del libro para mostrarlos en consola
        public string ObtenerResumenConsola(int indice)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"--- Libro {indice} ---");
            sb.AppendLine($"Código: {Codigo}");
            sb.AppendLine($"Título: {Titulo}");
            sb.AppendLine($"Autor: {Autor}");
            sb.AppendLine($"Año: {Anio}");
            sb.AppendLine($"Editorial: {Editorial}");
            sb.AppendLine($"Número de páginas: {NumeroPaginas}");
            return sb.ToString();
        }

        // Formatea los datos en una línea o bloque claro para el archivo de texto
        public string FormatearParaArchivo(int indice)
        {
            return $"Libro {indice} | Código: {Codigo} | Título: {Titulo} | Autor: {Autor} | Año: {Anio} | Editorial: {Editorial} | Páginas: {NumeroPaginas}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Asegura soporte de tildes y caracteres especiales en consola y archivo
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            const int cantidadLibros = 5;
            Libros[] catalogo = new Libros[cantidadLibros];

            Console.WriteLine("=== REGISTRO DE LIBROS (POO + ARREGLOS + ARCHIVOS) ===\n");

            // 1. Captura de datos por teclado en el arreglo de objetos
            for (int i = 0; i < catalogo.Length; i++)
            {
                int numeroLibro = i + 1;
                Console.WriteLine($"*** Libro {numeroLibro} ***");

                Libros libro = new Libros();

                libro.Codigo = LeerTextoNoVacio("Ingrese el código: ");
                libro.Titulo = LeerTextoNoVacio("Ingrese el título: ");
                libro.Autor = LeerTextoNoVacio("Ingrese el autor: ");
                libro.Anio = LeerEnteroPositivo("Ingrese el año de publicación: ");
                libro.Editorial = LeerTextoNoVacio("Ingrese la editorial: ");
                libro.NumeroPaginas = LeerEnteroPositivo("Ingrese el número de páginas: ");

                catalogo[i] = libro;
                Console.WriteLine();
            }

            // 2. Mostrar los datos almacenados en el arreglo
            Console.WriteLine("\n=== DATOS INGRESADOS EN PANTALLA ===\n");
            for (int i = 0; i < catalogo.Length; i++)
            {
                Console.WriteLine(catalogo[i].ObtenerResumenConsola(i + 1));
            }

            // 3. Guardar los datos en un archivo de texto en el directorio de ejecución
            string nombreArchivo = "libros_registrados.txt";
            string rutaCompleta = Path.GetFullPath(nombreArchivo);

            try
            {
                using (StreamWriter escritor = new StreamWriter(rutaCompleta, false, Encoding.UTF8))
                {
                    escritor.WriteLine("REGISTRO DE LIBROS");
                    escritor.WriteLine($"Fecha de generación: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                    escritor.WriteLine(new string('-', 60));

                    for (int i = 0; i < catalogo.Length; i++)
                    {
                        escritor.WriteLine(catalogo[i].FormatearParaArchivo(i + 1));
                    }
                }

                Console.WriteLine("Archivo guardado correctamente.");
                Console.WriteLine($"Ruta del archivo: {rutaCompleta}");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Error de entrada/salida al escribir el archivo: {ex.Message}");
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine($"Sin permisos para escribir en la ruta destino: {ex.Message}");
            }

            Console.WriteLine("\nPresione cualquier tecla para salir...");
            Console.ReadKey();
        }

        // Valida que el usuario no deje campos de texto vacíos
        static string LeerTextoNoVacio(string mensaje)
        {
            while (true)
            {
                Console.Write(mensaje);
                string entrada = Console.ReadLine()?.Trim() ?? string.Empty;

                if (!string.IsNullOrEmpty(entrada))
                {
                    return entrada;
                }

                Console.WriteLine("Entrada inválida. Este campo no puede quedar vacío.");
            }
        }

        // Valida que el usuario ingrese un número entero mayor que cero
        static int LeerEnteroPositivo(string mensaje)
        {
            while (true)
            {
                Console.Write(mensaje);
                string entrada = Console.ReadLine()?.Trim() ?? string.Empty;

                if (int.TryParse(entrada, out int valor) && valor > 0)
                {
                    return valor;
                }

                Console.WriteLine("Entrada inválida. Ingrese un número entero positivo.");
            }
        }
    }
}
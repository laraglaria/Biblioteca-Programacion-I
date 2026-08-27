using AccesoDatos.Data;
using AccesoDatos.Models;
using AccesoDatos.Repositories;
using Microsoft.EntityFrameworkCore;

string rutaBaseDatos = Path.Combine(
    Directory.GetParent(AppContext.BaseDirectory)!.Parent!.Parent!.Parent!.FullName,
    "biblioteca.db"
);

var options = new DbContextOptionsBuilder<BibliotecaDbContext>()
    .UseSqlite($"Data Source={rutaBaseDatos}")
    .Options;

using var context = new BibliotecaDbContext(options);

var autorRepository = new AutorRepository(context);
var libroRepository = new LibroRepository(context);

bool continuar = true;

while (continuar)
{
    Console.Clear();

    Console.WriteLine("================================");
    Console.WriteLine("       GESTIÓN DE BIBLIOTECA");
    Console.WriteLine("================================");
    Console.WriteLine();
    Console.WriteLine("1. Alta de Autor");
    Console.WriteLine("2. Alta de Libro");
    Console.WriteLine("3. Ver Libros");
    Console.WriteLine("0. Salir");
    Console.WriteLine();
    Console.Write("Seleccione una opción: ");

    string? opcion = Console.ReadLine();

    switch (opcion)
    {
        case "1":
            Console.Clear();

            Console.WriteLine("================================");
            Console.WriteLine("          ALTA DE AUTOR");
            Console.WriteLine("================================");
            Console.WriteLine();

            Console.Write("Ingrese el nombre del autor: ");
            string? nombreAutor = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(nombreAutor))
            {
                Console.WriteLine();
                Console.WriteLine("El nombre del autor no puede estar vacío.");
            }
            else
            {
                Autor autor = new Autor
                {
                    Nombre = nombreAutor.Trim()
                };

                autorRepository.Agregar(autor);

                Console.WriteLine();
                Console.WriteLine("Autor registrado correctamente.");
            }

            Console.WriteLine();
            Console.WriteLine("Presione una tecla para continuar...");
            Console.ReadKey();
            break;

        case "2":
            Console.WriteLine();
            Console.WriteLine("Alta de Libro");
            break;

        case "3":
            Console.WriteLine();
            Console.WriteLine("Ver Libros");
            break;

        case "0":
            continuar = false;
            break;

        default:
            Console.WriteLine();
            Console.WriteLine("Opción no válida.");
            Console.WriteLine("Presione una tecla para continuar...");
            Console.ReadKey();
            break;
    }
}
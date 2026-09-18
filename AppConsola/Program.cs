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
var categoriaRepository = new CategoriaRepository(context);

bool continuar = true;

while (continuar)
{
    Console.Clear();

    Console.WriteLine("================================");
    Console.WriteLine("       GESTIÓN DE BIBLIOTECA");
    Console.WriteLine("================================");
    Console.WriteLine();
    Console.WriteLine("1. Alta de Autor");
    Console.WriteLine("2. Alta de Categoría");
    Console.WriteLine("3. Alta de Libro");
    Console.WriteLine("4. Ver Autores");
    Console.WriteLine("5. Ver Categorías");
    Console.WriteLine("6. Ver Libros");
    Console.WriteLine("7. Modificar Libro");
    Console.WriteLine("8. Eliminar Libro");
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
            Console.Clear();

            Console.WriteLine("================================");
            Console.WriteLine("        ALTA DE CATEGORÍA");
            Console.WriteLine("================================");
            Console.WriteLine();

            Console.Write("Ingrese el nombre de la categoría: ");
            string? nombreCategoria = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(nombreCategoria))
            {
                Console.WriteLine();
                Console.WriteLine("El nombre de la categoría no puede estar vacío.");
            }
            else
            {
                Categoria categoria = new Categoria
                {
                    Nombre = nombreCategoria.Trim()
                };

                categoriaRepository.Agregar(categoria);

                Console.WriteLine();
                Console.WriteLine("Categoría registrada correctamente.");
            }

            Console.WriteLine();
            Console.WriteLine("Presione una tecla para continuar...");
            Console.ReadKey();
            break;

        case "3":
            Console.Clear();

            Console.WriteLine("================================");
            Console.WriteLine("          ALTA DE LIBRO");
            Console.WriteLine("================================");
            Console.WriteLine();

            List<Autor> autores = autorRepository.ObtenerTodos();

            if (autores.Count == 0)
            {
                Console.WriteLine("No hay autores registrados.");
                Console.WriteLine("Debe registrar un autor antes de agregar un libro.");
                Console.WriteLine();
                Console.WriteLine("Presione una tecla para continuar...");
                Console.ReadKey();
                break;
            }

            Console.WriteLine("Autores disponibles:");
            Console.WriteLine();

            foreach (Autor autor in autores)
            {
                Console.WriteLine($"{autor.Id}. {autor.Nombre}");
            }

            Console.WriteLine();
            Console.Write("Seleccione el ID del autor: ");
            string? autorIdTexto = Console.ReadLine();

            if (!int.TryParse(autorIdTexto, out int autorId))
            {
                Console.WriteLine();
                Console.WriteLine("El ID del autor debe ser un número.");
                Console.WriteLine();
                Console.WriteLine("Presione una tecla para continuar...");
                Console.ReadKey();
                break;
            }

            Autor? autorSeleccionado = autores.FirstOrDefault(a => a.Id == autorId);

            if (autorSeleccionado == null)
            {
                Console.WriteLine();
                Console.WriteLine("No existe un autor con ese ID.");
                Console.WriteLine();
                Console.WriteLine("Presione una tecla para continuar...");
                Console.ReadKey();
                break;
            }

            Console.WriteLine();
            Console.WriteLine($"Autor seleccionado: {autorSeleccionado.Nombre}");
            Console.WriteLine($"ID del autor seleccionado: {autorSeleccionado.Id}");

            Console.WriteLine();
            Console.WriteLine("Categorías disponibles:");
            Console.WriteLine();

            List<Categoria> categoriasDisponibles = categoriaRepository.ObtenerTodos();

            if (categoriasDisponibles.Count == 0)
            {
                Console.WriteLine("No hay categorías registradas.");
                Console.WriteLine("Debe registrar una categoría antes de agregar un libro.");
                Console.WriteLine();
                Console.WriteLine("Presione una tecla para continuar...");
                Console.ReadKey();
                break;
            }

            foreach (Categoria categoriaActual in categoriasDisponibles)
            {
                Console.WriteLine($"{categoriaActual.Id}. {categoriaActual.Nombre}");
            }

            Console.WriteLine();
            Console.Write("Seleccione el ID de la categoría: ");
            string? categoriaIdTexto = Console.ReadLine();

            if (!int.TryParse(categoriaIdTexto, out int categoriaId))
            {
                Console.WriteLine();
                Console.WriteLine("El ID de la categoría debe ser un número.");
                Console.WriteLine();
                Console.WriteLine("Presione una tecla para continuar...");
                Console.ReadKey();
                break;
            }

            Categoria? categoriaSeleccionada = categoriasDisponibles.FirstOrDefault(c => c.Id == categoriaId);

            if (categoriaSeleccionada == null)
            {
                Console.WriteLine();
                Console.WriteLine("No existe una categoría con ese ID.");
                Console.WriteLine();
                Console.WriteLine("Presione una tecla para continuar...");
                Console.ReadKey();
                break;
            }

            Console.WriteLine();
            Console.WriteLine($"Categoría seleccionada: {categoriaSeleccionada.Nombre}");
            Console.WriteLine($"ID de la categoría seleccionada: {categoriaSeleccionada.Id}");

            Console.WriteLine();
            Console.Write("Ingrese el título del libro: ");
            string? titulo = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(titulo))
            {
                Console.WriteLine();
                Console.WriteLine("El título del libro no puede estar vacío.");
                Console.WriteLine();
                Console.WriteLine("Presione una tecla para continuar...");
                Console.ReadKey();
                break;
            }

            Console.WriteLine();
            Console.Write("Ingrese el año de publicación: ");
            string? anioTexto = Console.ReadLine();

            if (!int.TryParse(anioTexto, out int anioPublicacion))
            {
                Console.WriteLine();
                Console.WriteLine("El año de publicación debe ser un número.");
                Console.WriteLine();
                Console.WriteLine("Presione una tecla para continuar...");
                Console.ReadKey();
                break;
            }

            Libro libro = new Libro
            {
                Titulo = titulo.Trim(),
                AnioPublicacion = anioPublicacion,
                AutorId = autorSeleccionado.Id,
                CategoriaId = categoriaSeleccionada.Id,
                Activo = true
            };

            libroRepository.Agregar(libro);

            Console.WriteLine();
            Console.WriteLine("Libro registrado correctamente.");
            Console.WriteLine();
            Console.WriteLine("Presione una tecla para continuar...");
            Console.ReadKey();
            break;

        case "4":
            Console.Clear();

            Console.WriteLine("================================");
            Console.WriteLine("           VER AUTORES");
            Console.WriteLine("================================");
            Console.WriteLine();

            List<Autor> autoresRegistrados = autorRepository.ObtenerTodos();

            if (autoresRegistrados.Count == 0)
            {
                Console.WriteLine("No hay autores registrados.");
            }
            else
            {
                foreach (Autor autorActual in autoresRegistrados)
                {
                    Console.WriteLine($"ID: {autorActual.Id}");
                    Console.WriteLine($"Nombre: {autorActual.Nombre}");
                    Console.WriteLine("--------------------------------");
                }
            }

            Console.WriteLine();
            Console.WriteLine("Presione una tecla para continuar...");
            Console.ReadKey();
            break;

        case "5":
            Console.Clear();

            Console.WriteLine("================================");
            Console.WriteLine("        VER CATEGORÍAS");
            Console.WriteLine("================================");
            Console.WriteLine();

            List<Categoria> categorias = categoriaRepository.ObtenerTodos();

            if (categorias.Count == 0)
            {
                Console.WriteLine("No hay categorías registradas.");
            }
            else
            {
                foreach (Categoria categoriaActual in categorias)
                {
                    Console.WriteLine($"ID: {categoriaActual.Id}");
                    Console.WriteLine($"Nombre: {categoriaActual.Nombre}");
                    Console.WriteLine("--------------------------------");
                }
            }

            Console.WriteLine();
            Console.WriteLine("Presione una tecla para continuar...");
            Console.ReadKey();
            break;

        case "6":
            {
                Console.Clear();

                Console.WriteLine("================================");
                Console.WriteLine("           VER LIBROS");
                Console.WriteLine("================================");
                Console.WriteLine();

                List<Libro> libros = libroRepository.ObtenerTodos();

                if (libros.Count == 0)
                {
                    Console.WriteLine("No hay libros registrados.");
                }
                else
                {
                    foreach (Libro libroActual in libros)
                    {
                        Console.WriteLine($"Título: {libroActual.Titulo}");
                        Console.WriteLine($"Año de publicación: {libroActual.AnioPublicacion}");
                        Console.WriteLine($"AutorId: {libroActual.AutorId}");
                        Console.WriteLine($"Autor: {libroActual.Autor.Nombre}");
                        Console.WriteLine("--------------------------------");
                    }
                }

                Console.WriteLine();
                Console.WriteLine("Presione una tecla para continuar...");
                Console.ReadKey();
                break;
            }

        case "7":
            {
                Console.Clear();

                Console.WriteLine("================================");
                Console.WriteLine("        MODIFICAR LIBRO");
                Console.WriteLine("================================");
                Console.WriteLine();

                Console.Write("Ingrese el ID del libro que desea modificar: ");
                string? entradaId = Console.ReadLine();

                if (!int.TryParse(entradaId, out int idLibro))
                {
                    Console.WriteLine("El ID ingresado no es válido.");
                    Console.WriteLine();
                    Console.WriteLine("Presione una tecla para continuar...");
                    Console.ReadKey();
                    break;
                }

                Libro? libroAModificar = libroRepository.ObtenerPorId(idLibro);

                if (libroAModificar == null)
                {
                    Console.WriteLine("No existe un libro con ese ID.");
                    Console.WriteLine();
                    Console.WriteLine("Presione una tecla para continuar...");
                    Console.ReadKey();
                    break;
                }

                Console.WriteLine();
                Console.WriteLine("Libro encontrado:");
                Console.WriteLine($"Título: {libroAModificar.Titulo}");
                Console.WriteLine($"Año de publicación: {libroAModificar.AnioPublicacion}");
                Console.WriteLine($"Autor: {libroAModificar.Autor.Nombre}");
                Console.WriteLine($"Categoría: {libroAModificar.Categoria.Nombre}");

                Console.WriteLine();
                Console.Write($"Ingrese el nuevo título (actual: {libroAModificar.Titulo}): ");
                string? nuevoTitulo = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(nuevoTitulo))
                {
                    Console.WriteLine("El título no puede estar vacío.");
                    Console.WriteLine();
                    Console.WriteLine("Presione una tecla para continuar...");
                    Console.ReadKey();
                    break;
                }

                libroAModificar.Titulo = nuevoTitulo.Trim();

                Console.WriteLine();
                Console.Write($"Ingrese el nuevo año de publicación (actual: {libroAModificar.AnioPublicacion}): ");
                string? entradaNuevoAnio = Console.ReadLine();

                if (!int.TryParse(entradaNuevoAnio, out int nuevoAnio))
                {
                    Console.WriteLine("El año ingresado no es válido.");
                    Console.WriteLine();
                    Console.WriteLine("Presione una tecla para continuar...");
                    Console.ReadKey();
                    break;
                }

                libroAModificar.AnioPublicacion = nuevoAnio;

                libroRepository.Modificar(libroAModificar);

                Console.WriteLine();
                Console.WriteLine($"Nuevo título: {libroAModificar.Titulo}");
                Console.WriteLine($"Nuevo año de publicación: {libroAModificar.AnioPublicacion}");

                Console.WriteLine();
                Console.WriteLine("Presione una tecla para continuar...");
                Console.ReadKey();
                break;
            }

        case "8":
            {
                Console.Clear();

                Console.WriteLine("================================");
                Console.WriteLine("        ELIMINAR LIBRO");
                Console.WriteLine("================================");
                Console.WriteLine();

                Console.Write("Ingrese el ID del libro que desea eliminar: ");
                string? entradaId = Console.ReadLine();

                if (!int.TryParse(entradaId, out int idLibro))
                {
                    Console.WriteLine("El ID ingresado no es válido.");
                    Console.WriteLine();
                    Console.WriteLine("Presione una tecla para continuar...");
                    Console.ReadKey();
                    break;
                }

                Libro? libroAEliminar = libroRepository.ObtenerPorId(idLibro);

                if (libroAEliminar == null)
                {
                    Console.WriteLine("No existe un libro con ese ID.");
                    Console.WriteLine();
                    Console.WriteLine("Presione una tecla para continuar...");
                    Console.ReadKey();
                    break;
                }

                Console.WriteLine();
                Console.WriteLine("Libro encontrado:");
                Console.WriteLine($"Título: {libroAEliminar.Titulo}");
                Console.WriteLine($"Año de publicación: {libroAEliminar.AnioPublicacion}");
                Console.WriteLine($"Autor: {libroAEliminar.Autor.Nombre}");
                Console.WriteLine($"Categoría: {libroAEliminar.Categoria.Nombre}");

                Console.WriteLine();
                Console.Write("¿Está seguro que desea eliminar este libro? (s/n): ");
                string? confirmacion = Console.ReadLine();

                if (confirmacion?.ToLower() != "s")
                {
                    Console.WriteLine();
                    Console.WriteLine("Eliminación cancelada.");
                    Console.WriteLine();
                    Console.WriteLine("Presione una tecla para continuar...");
                    Console.ReadKey();
                    break;
                }

                libroRepository.EliminarLogicamente(libroAEliminar);

                Console.WriteLine();
                Console.WriteLine("El libro fue eliminado correctamente.");
                Console.WriteLine();
                Console.WriteLine("Presione una tecla para continuar...");
                Console.ReadKey();
                break;
            }

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
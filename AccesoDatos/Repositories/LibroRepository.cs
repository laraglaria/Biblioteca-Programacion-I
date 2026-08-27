using System;
using System.Collections.Generic;
using System.Text;
using AccesoDatos.Data;
using AccesoDatos.Models;
using Microsoft.EntityFrameworkCore;

namespace AccesoDatos.Repositories
{
    public class LibroRepository
    {
        private readonly BibliotecaDbContext _context;

        public LibroRepository(BibliotecaDbContext context)
        {
            _context = context;
        }

        public void Agregar(Libro libro)
        {
            _context.Libros.Add(libro);
            _context.SaveChanges();
        }

        public List<Libro> ObtenerTodos()
        {
            return _context.Libros
                .Include(l => l.Autor)
                .ToList();
        }
    }
}
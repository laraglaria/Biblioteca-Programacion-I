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
                .Where(l => l.Activo)
                .ToList();
        }

        public Libro? ObtenerPorId(int id)
        {
            return _context.Libros
                .Include(l => l.Autor)
                .Include(l => l.Categoria)
                .FirstOrDefault(l => l.Id == id);
        }

        public void Modificar(Libro libro)
        {
            _context.Libros.Update(libro);
            _context.SaveChanges();
        }

        public void EliminarLogicamente(Libro libro)
        {
            libro.Activo = false;
            _context.Libros.Update(libro);
            _context.SaveChanges();
        }
    }
}
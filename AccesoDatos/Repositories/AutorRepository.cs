using System;
using System.Collections.Generic;
using System.Text;
using AccesoDatos.Data;
using AccesoDatos.Models;

namespace AccesoDatos.Repositories
{
    public class AutorRepository
    {
        private readonly BibliotecaDbContext _context;

        public AutorRepository(BibliotecaDbContext context)
        {
            _context = context;
        }

        public void Agregar(Autor autor)
        {
            _context.Autores.Add(autor);
            _context.SaveChanges();
        }

        public List<Autor> ObtenerTodos()
        {
            return _context.Autores.ToList();
        }
    }
}

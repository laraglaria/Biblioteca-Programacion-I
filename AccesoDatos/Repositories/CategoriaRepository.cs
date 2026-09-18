using AccesoDatos.Data;
using AccesoDatos.Models;

namespace AccesoDatos.Repositories
{
    public class CategoriaRepository
    {
        private readonly BibliotecaDbContext _context;

        public CategoriaRepository(BibliotecaDbContext context)
        {
            _context = context;
        }

        public void Agregar(Categoria categoria)
        {
            _context.Categorias.Add(categoria);
            _context.SaveChanges();
        }

        public List<Categoria> ObtenerTodos()
        {
            return _context.Categorias
                .ToList();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace AccesoDatos.Data
{
    public class BibliotecaDbContextFactory : IDesignTimeDbContextFactory<BibliotecaDbContext>
    {
        public BibliotecaDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BibliotecaDbContext>();

            string rutaBaseDatos = Path.Combine(
                Directory.GetCurrentDirectory(),
                "..",
                "AppConsola",
                "biblioteca.db"
            );

            optionsBuilder.UseSqlite($"Data Source={rutaBaseDatos}");

            return new BibliotecaDbContext(optionsBuilder.Options);
        }
    }
}

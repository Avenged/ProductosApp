using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ProductosApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<ProductoCategorias> ProductoCategorias { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Marca> Marcas { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Producto>(p =>
            {
                p.HasData(new Producto[]
                {
                    new Producto
                    {
                        Id = -1,
                        Name = "Televisor Samsung",
                        Precio = 100,
                        Unidades = 10,
                    },
                    new Producto
                    {
                        Id = -2,
                        Name = "iPhone 14 Pro Max",
                        Precio = 1200,
                        Unidades = 10,
                    },
                });
            });
        }
    }
}
using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ProductosApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Estado> Estados { get; set; }
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

            builder.Entity<Estado>(p =>
            {
                p.HasData(new Estado[]
                {
                    new Estado
                    {
                        Id = -1,
                        Nombre = "ACTIVO",
                    },
                    new Estado
                    {
                        Id = -2,
                        Nombre = "INACTIVO",
                    },
                    new Estado
                    {
                        Id = -3,
                        Nombre = "BAJA",
                    },
                });
            });

            builder.Entity<Producto>(p =>
            {
                p.HasOne(x => x.Estado)
                    .WithMany(x => x.Productos)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            builder.Entity<Categoria>(p =>
            {
                p.HasOne(x => x.Estado)
                    .WithMany(x => x.Categorias)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            builder.Entity<Marca>(p =>
            {
                p.HasOne(x => x.Estado)
                    .WithMany(x => x.Marcas)
                    .OnDelete(DeleteBehavior.NoAction);
            });
        }
    }
}
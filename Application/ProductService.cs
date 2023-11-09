using Application.DTOs;
using Microsoft.EntityFrameworkCore;
using ProductosApp.Data;

namespace Application;

public class ProductService : IProductService
{
    private readonly ApplicationDbContext context;

    public ProductService(ApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task Crear()
    {

    }

    public async Task CrearProductosFicticios()
    {
        Random random = new();

        context.Productos.Add(new Producto
        {
            Name = random.Next(1000, 99999).ToString(),
            Precio = 123123,
            Unidades = 10,
        });

        context.Productos.Add(new Producto
        {
            Name = random.Next(1000, 99999).ToString(),
            Precio = 123,
            Unidades = 10,
        });

        await context.SaveChangesAsync();
    }

    public async Task<IEnumerable<ProductoDTO>> Obtener()
    {
        var productos = await context.Productos
            .AsNoTracking()
            .ToArrayAsync();

        var productosDTO = productos.Select(p => new ProductoDTO
        {
            Id = p.Id,
            Marca = p.Marca?.Nombre,
            Name = p.Name,
            Precio = p.Precio,
            Unidades = p.Unidades,
        }).ToArray();

        return productosDTO;
    }

    public async Task Actualizar()
    {

    }

    public async Task Eliminar(int id)
    {
        //throw new InvalidOperationException("El producto está siendo usado, no puede ser eliminado.");
        throw new Exception("Ocurrió un error al comunicarse con la base de datos.");

        Producto productoAEliminar = new()
        {
            Id = id,
        };

        context.Productos.Remove(productoAEliminar);
        await context.SaveChangesAsync();
    }
}

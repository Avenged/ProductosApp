using Application.DTOs;
using Microsoft.EntityFrameworkCore;
using ProductosApp.Data;
using System;

namespace Application;

public class ProductService : IProductService
{
    private readonly ApplicationDbContext context;

    public ProductService(ApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task AgregarProducto(string nombre, decimal precio, int unidades)
    {
        context.Productos.Add(new Producto
        {
            Name = nombre,
            Precio = precio,
            Unidades = unidades,
        });

        await context.SaveChangesAsync();
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

    public async Task Eliminar(int id)
    {
        var producto = await context.Productos
            .FirstOrDefaultAsync(x => x.Id == id);

        context.Productos.Remove(producto);
        await context.SaveChangesAsync();
    }

    public async Task ActualizarProducto(int productoId, string nombre, decimal precio, int unidades)
    {
        // Trae el producto de la base, y trackea sus cambios
        var producto = await context.Productos
            .FirstOrDefaultAsync(x => x.Id == productoId);

        producto.Name = nombre;
        producto.Precio = precio;
        producto.Unidades = unidades;

        await context.SaveChangesAsync();
    }

    public async Task<ProductoDTO> Obtener(int id)
    {
        var entidadesTrackeadas = context.ChangeTracker.Entries().ToList();

        // Si no encuentra el producto, entonces lanza una excepción
        //var producto = await context.Productos.FirstAsync(x => x.Id == id);

        // Si no encuentra el producto, entonces la variable "producto" quedará como nula
        // El producto acá se empieza a trackear.
        var producto = await context.Productos.FirstOrDefaultAsync(x => x.Id == id);
   
        if (producto is null)
        {
            return null;
        }

        return new ProductoDTO
        {
            Id = producto.Id,
            Marca = producto.Marca?.Nombre,
            Name = producto.Name,
            Precio = producto.Precio,
            Unidades = producto.Unidades,
        };
    }
}

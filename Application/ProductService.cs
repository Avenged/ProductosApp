using Application.DTOs;
using Domain;
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

    public async Task Agregar(string nombre, decimal precio, int unidades, int? marcaId, IEnumerable<int> categoriaIds)
    {
        var producto = new Producto
        {
            Name = nombre,
            Precio = precio,
            Unidades = unidades,
            MarcaId = marcaId,
        };
        context.Productos.Add(producto);
        await context.SaveChangesAsync();

        foreach (var categoriaId in categoriaIds)
        {
            var productoCategoria = new ProductoCategorias
            {
                ProductoId = producto.Id,
                CategoriaId = categoriaId,
            };
            context.ProductoCategorias.Add(productoCategoria);
        }
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
            .Include(p => p.Marca)
            .OrderBy(p => p.Name)
            .ToArrayAsync();

        var productosDTO = productos.Select(p => new ProductoDTO
        {
            Id = p.Id,
            Marca = p.Marca?.Nombre,
            MarcaId = p.MarcaId,
            //Categoria = p.Categorias?.Nombre,
            //CategoriaId = p.CategoriaIds,
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

    public async Task Actualizar(int productoId, string nombre, decimal precio, int unidades, int? marcaId, int? categoriaId)
    {
        // Trae el producto de la base, y trackea sus cambios
        var producto = await context.Productos
            .FirstOrDefaultAsync(x => x.Id == productoId);

        producto.Name = nombre;
        producto.Precio = precio;
        producto.Unidades = unidades;
        producto.MarcaId = marcaId;
        //producto.CategoriaIds = categoriaId;

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
            MarcaId = producto.MarcaId,
            //Categoria = producto.Categorias?.Nombre,
            //CategoriaId = producto.CategoriaIds,
            Name = producto.Name,
            Precio = producto.Precio,
            Unidades = producto.Unidades,
        };
    }
}

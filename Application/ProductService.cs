using Application.DTOs;
using Application.Enums;
using Application.Interfaces;
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
            EstadoId = (int)Estados.ACTIVO,
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
            .Where(x => x.EstadoId != (int)Estados.BAJA)
            .Include(x => x.Categorias).ThenInclude(x => x.Categoria)
            .Include(p => p.Marca)
            .Include(p => p.Estado)
            .OrderBy(p => p.Name)
            .ToArrayAsync();

        var productosDTO = productos.Select(p => new ProductoDTO
        {
            Id = p.Id,
            Marca = p.Marca?.Nombre,
            MarcaId = p.MarcaId,
            Estado = p.Estado.Nombre,
            EstadoId = p.EstadoId,
            Name = p.Name,
            Precio = p.Precio,
            Unidades = p.Unidades,
            Categorias = p.Categorias.Select(prodCat => new CategoriaDTO
            {
                Id = prodCat.CategoriaId,
                Nombre = prodCat.Categoria.Nombre,
            }).ToArray()
        }).ToArray();

        return productosDTO;
    }

    public async Task Eliminar(int id)
    {
        var producto = await context.Productos
            .FirstOrDefaultAsync(x => x.Id == id);

        if (producto is null)
        {
            throw new Exception($"No se encontró un producto con el id {id}.");
        }

        producto.EstadoId = (int)Estados.BAJA;
        context.Productos.Update(producto);

        //context.Productos.Remove(producto);

        await context.SaveChangesAsync();
    }

    public async Task Actualizar(int productoId, string nombre, decimal precio, int unidades, int? marcaId, IEnumerable<int> categoriaIds, int estadoId)
    {
        // Trae el producto de la base, y trackea sus cambios
        var producto = await context.Productos
            .FirstOrDefaultAsync(x => x.Id == productoId);

        if (producto is null)
        {
            throw new InvalidOperationException($"El producto con id {productoId} no existe.");
        }

        producto.Name = nombre;
        producto.Precio = precio;
        producto.Unidades = unidades;
        producto.MarcaId = marcaId;
        producto.EstadoId = estadoId;

        //var categoriasActuales = producto.Categorias.Select(x => x.CategoriaId);

        //List<int> categoriasAgregar = new();
        //foreach (var categoriaId in categoriaIds)
        //{
        //    var loContiene = categoriasActuales.Contains(categoriaId);
        //    if (!loContiene)
        //    {
        //        categoriasAgregar.Add(categoriaId);
        //    }
        //}

        var categoriasAgregar = categoriaIds.Where(categoriaId => !producto.Categorias.Any(tablaInter => tablaInter.CategoriaId == categoriaId));
        var categoriasEliminar = producto.Categorias.Where(x => !categoriaIds.Any(xx => xx == x.CategoriaId));

        context.ProductoCategorias.AddRange(categoriasAgregar.Select(categoriaId => new ProductoCategorias
        {
            CategoriaId = categoriaId,
            ProductoId = productoId,
        }));

        context.ProductoCategorias.RemoveRange(categoriasEliminar);

        await context.SaveChangesAsync();
    }

    public async Task<ProductoDTO> Obtener(int id)
    {
        var entidadesTrackeadas = context.ChangeTracker.Entries().ToList();

        // Si no encuentra el producto, entonces lanza una excepción
        //var producto = await context.Productos.FirstAsync(x => x.Id == id);

        // Si no encuentra el producto, entonces la variable "producto" quedará como nula
        // El producto acá se empieza a trackear.
        var producto = await context.Productos
            .Include(x => x.Categorias)
            .FirstOrDefaultAsync(x => x.Id == id);
   
        if (producto is null)
        {
            return null;
        }

        var dto = new ProductoDTO
        {
            Id = producto.Id,
            Marca = producto.Marca?.Nombre,
            MarcaId = producto.MarcaId,
            //Categoria = producto.Categorias?.Nombre,
            //CategoriaId = producto.CategoriaIds,
            Name = producto.Name,
            Precio = producto.Precio,
            Unidades = producto.Unidades,
            EstadoId = producto.EstadoId,
            Categorias = producto.Categorias.Select(prodCat => new CategoriaDTO
            {
                Id = prodCat.CategoriaId,
                Nombre = prodCat.Categoria.Nombre,
            }).ToArray()
        };

        return dto;
    }
}

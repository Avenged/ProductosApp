using Application.DTOs;
using Microsoft.EntityFrameworkCore;
using ProductosApp.Data;

namespace Application;

public class CategoriaService : ICategoriaService
{
    private readonly ApplicationDbContext context;

    public CategoriaService(ApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task Actualizar(int categoriaId, string nombre)
    {
        // ¿Existe en la db una marca la cual su nombre sea igual al nombre que quiere crear el usuario? (si/no) (true/false)
        var existe = await context.Categorias.AnyAsync(x => x.Nombre == nombre && x.Id != categoriaId);

        if (existe)
        {
            throw new InvalidOperationException($"Ya existe una categoria con el nombre {nombre}.");
        }

        var categoria = await context.Categorias.FirstOrDefaultAsync(x => x.Id == categoriaId);

        if (categoria is null)
        {
            throw new Exception($"La categoria con el id {categoriaId} no existe.");
        }

        categoria.Nombre = nombre;
        await context.SaveChangesAsync();
    }

    public async Task Agregar(string nombre)
    {
        // ¿Existe en la db una marca la cual su nombre sea igual al nombre que quiere crear el usuario? (si/no) (true/false)
        var existe = await context.Categorias.AnyAsync(x => x.Nombre == nombre);

        if (existe)
        {
            throw new InvalidOperationException($"Ya existe una categoria con el nombre {nombre}.");
        }

        Categoria nuevaCategoria = new()
        {
            Nombre = nombre,
        };

        context.Categorias.Add(nuevaCategoria);
        await context.SaveChangesAsync();
    }

    public async Task Eliminar(int id)
    {
        var categoria = await context.Categorias.FirstOrDefaultAsync(x => x.Id == id);

        if (categoria is null)
        {
            throw new Exception($"La categoria con el id {id} no existe.");
        }

        context.Categorias.Remove(categoria);
        await context.SaveChangesAsync();
    }

    public async Task<CategoriaDTO> Obtener(int id)
    {
        var categoria = await context.Categorias.FirstOrDefaultAsync(x => x.Id == id);

        if (categoria is null)
        {
            throw new Exception($"La categoria con el id {id} no existe.");
        }

        return new CategoriaDTO
        {
            Id = categoria.Id,
            Nombre = categoria.Nombre,
        };
    }

    public async Task<IEnumerable<CategoriaDTO>> Obtener()
    {
        var categorias = await context.Categorias.OrderBy(c => c.Nombre).ToArrayAsync();

        return categorias.Select(c => new CategoriaDTO
        {
            Id = c.Id,
            Nombre = c.Nombre,
        }).ToArray();
    }
}

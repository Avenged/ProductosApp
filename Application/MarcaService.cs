using Application.DTOs;
using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using ProductosApp.Data;

namespace Application;

public class MarcaService : IMarcaService
{
    private readonly ApplicationDbContext context;

    public MarcaService(ApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task Actualizar(int marcaId, string nombre)
    {
        // ¿Existe en la db una marca la cual su nombre sea igual al nombre que quiere crear el usuario? (si/no) (true/false)
        var existe = await context.Marcas.AnyAsync(x => x.Nombre == nombre && x.Id != marcaId);

        if (existe)
        {
            throw new InvalidOperationException($"Ya existe una marca con el nombre {nombre}.");
        }

        var marca = await context.Marcas.FirstOrDefaultAsync(x => x.Id == marcaId);

        if (marca is null)
        {
            throw new Exception($"La marca con el id {marcaId} no existe.");
        }

        marca.Nombre = nombre;
        await context.SaveChangesAsync();
    }

    public async Task Agregar(string nombre)
    {
        // ¿Existe en la db una marca la cual su nombre sea igual al nombre que quiere crear el usuario? (si/no) (true/false)
        var existe = await context.Marcas.AnyAsync(x => x.Nombre == nombre);

        if (existe)
        {
            throw new InvalidOperationException($"Ya existe una marca con el nombre {nombre}.");
        }

        Marca nuevaMarca = new()
        {
            Nombre = nombre,
        };

        context.Marcas.Add(nuevaMarca);
        await context.SaveChangesAsync();
    }

    public async Task Eliminar(int id)
    {
        var marca = await context.Marcas.FirstOrDefaultAsync(x => x.Id == id);

        if (marca is null)
        {
            throw new Exception($"La marca con el id {id} no existe.");
        }

        context.Marcas.Remove(marca);
        await context.SaveChangesAsync();
    }

    public async Task<MarcaDTO> Obtener(int id)
    {
        var marca = await context.Marcas.FirstOrDefaultAsync(x => x.Id == id);

        if (marca is null)
        {
            throw new Exception($"La marca con el id {id} no existe.");
        }

        return new MarcaDTO
        {
            Id = marca.Id,
            Nombre = marca.Nombre,
        };
    }

    public async Task<IEnumerable<MarcaDTO>> Obtener()
    {
        var marcas = await context.Marcas.OrderBy(m => m.Nombre).ToArrayAsync();

        return marcas.Select(m => new MarcaDTO
        {
            Id = m.Id,
            Nombre = m.Nombre,
        }).ToArray();
    }
}

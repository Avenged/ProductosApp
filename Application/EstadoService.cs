using Application.DTOs;
using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using ProductosApp.Data;

namespace Application;

public class EstadoService : IEstadoService
{
    private readonly ApplicationDbContext context;

    public EstadoService(ApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task<IEnumerable<EstadoDTO>> Obtener()
    {
        var estados = await context.Estados
            .Where(e => e.Nombre.ToUpper() != "BAJA")
            .OrderBy(e => e.Nombre)
            .ToArrayAsync();

        return estados.Select(e => new EstadoDTO
        {
            Id = e.Id,
            Nombre = e.Nombre,
        });
    }
}

using Application.DTOs;

namespace Application.Interfaces;

public interface IEstadoService
{
    Task<IEnumerable<EstadoDTO>> Obtener();
}

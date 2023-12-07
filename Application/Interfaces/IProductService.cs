using Application.DTOs;
using ProductosApp.Data;

namespace Application.Interfaces;

public interface IProductService
{
    Task<ProductoDTO> Obtener(int id);
    Task<IEnumerable<ProductoDTO>> Obtener();
    Task CrearProductosFicticios();
    Task Eliminar(int id);
    Task Actualizar(int productoId, string nombre, decimal precio, int unidades, int? marcaId, IEnumerable<int> categoriaId, int estadoId);
    Task Agregar(string nombre, decimal precio, int unidades, int? marcaId, IEnumerable<int> categoriaIds);
}

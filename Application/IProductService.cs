using Application.DTOs;

namespace Application;

public interface IProductService
{
    Task<ProductoDTO> Obtener(int id);
    Task<IEnumerable<ProductoDTO>> Obtener();
    Task CrearProductosFicticios();
    Task Eliminar(int id);
    Task Actualizar(int productoId, string nombre, decimal precio, int unidades, int? marcaId);
    Task Agregar(string nombre, decimal precio, int unidades, int? marcaId);
}

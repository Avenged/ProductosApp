using Application.DTOs;

namespace Application
{
    public interface IProductService
    {
        Task<ProductoDTO> Obtener(int id);
        Task<IEnumerable<ProductoDTO>> Obtener();
        Task CrearProductosFicticios();
        Task Eliminar(int id);
        Task ActualizarProducto(int productoId, string nombre, decimal precio, int unidades);
        Task AgregarProducto(string nombre, decimal precio, int unidades);
    }
}

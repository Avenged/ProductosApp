using Application.DTOs;

namespace Application
{
    public interface IProductService
    {
        Task CrearProductosFicticios();
        Task Eliminar(int id);
        Task<IEnumerable<ProductoDTO>> Obtener();
        Task AgregarProducto(string nombre, decimal precio, int unidades);
    }
}

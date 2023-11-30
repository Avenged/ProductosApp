using Domain;

namespace ProductosApp.Data;

public class Producto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Unidades { get; set; }
    public decimal Precio { get; set; }

    //Esto es una propiedad de navegación
    public int? MarcaId { get; set; }
    public Marca? Marca { get; set; }

    public int EstadoId { get; set; }
    public Estado Estado { get; set; } = null!;

    public ICollection<ProductoCategorias> Categorias { get; set; } = new HashSet<ProductoCategorias>();
}

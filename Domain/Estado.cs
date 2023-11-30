using ProductosApp.Data;

namespace Domain;

public class Estado
{
    public int Id { get; set; }
    public string Nombre { get; set; } = null!;
    public ICollection<Producto> Productos { get; set; } = new HashSet<Producto>();
    public ICollection<Categoria> Categorias { get; set; } = new HashSet<Categoria>();
    public ICollection<Marca> Marcas { get; set; } = new HashSet<Marca>();
}

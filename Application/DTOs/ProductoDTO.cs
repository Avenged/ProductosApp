namespace Application.DTOs;

public class ProductoDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Unidades { get; set; }
    public decimal Precio { get; set; }
    public string Marca { get; set; }
    public int? MarcaId { get; set; }
    public int? CategoriaId { get; set; }
    public string CategoriasNombres { get => string.Join(", ", Categorias.Select(x => x.Nombre)); }
    public string Estado { get; set; }
    public int EstadoId { get; set; }
    public IEnumerable<CategoriaDTO> Categorias { get; set; } = Enumerable.Empty<CategoriaDTO>();
}

namespace ProductosApp.Data
{
    public class Producto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Unidades { get; set; }
        public decimal Precio { get; set; }
        public int? MarcaId { get; set; }
        public Marca? Marca { get; set; }
    }
}

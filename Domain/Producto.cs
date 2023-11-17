namespace ProductosApp.Data
{
    public class Producto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Unidades { get; set; }
        public decimal Precio { get; set; }
        public int? MarcaId { get; set; }
        public int? CategoriaId { get; set; }

        //Esto es una propiedad de navegación
        public Marca? Marca { get; set; }

        //Esto es una propiedad de navegación
        public Categoria? Categoria { get; set; }
    }
}

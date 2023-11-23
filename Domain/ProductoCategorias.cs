using ProductosApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain;

public class ProductoCategorias
{
    public int Id { get; set; }

    public int ProductoId { get; set; }
    public Producto Producto { get; set; }

    public int CategoriaId { get; set; }
    public Categoria Categoria { get; set; }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{

    public class ProductoDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Unidades { get; set; }
        public decimal Precio { get; set; }
        public string Marca { get; set; }
        public int? MarcaId { get; set; }
    }
}

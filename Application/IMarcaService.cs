using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application;

public interface IMarcaService
{
    Task<MarcaDTO> Obtener(int id);
    Task<IEnumerable<MarcaDTO>> Obtener();
    Task Eliminar(int id);
    Task Actualizar(int marcaId, string nombre);
    Task Agregar(string nombre);
}

using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces;

public interface ICategoriaService
{
    Task<CategoriaDTO> Obtener(int id);
    Task<IEnumerable<CategoriaDTO>> Obtener();
    Task Eliminar(int id);
    Task Actualizar(int categoriaId, string nombre);
    Task Agregar(string nombre);
}

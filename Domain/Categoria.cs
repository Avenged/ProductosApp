﻿using Domain;

namespace ProductosApp.Data;

public class Categoria
{
    public int Id { get; set; }
    public string Nombre { get; set; } = null!;
    public int EstadoId { get; set; }
    public Estado Estado { get; set; } = null!;
}

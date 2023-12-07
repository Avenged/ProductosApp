using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace ProductosApp.Pages;

public partial class AgregarProducto : ComponentBase
{
    private IEnumerable<ProductoDTO>? productos;
    private IEnumerable<MarcaDTO>? marcas;
    private IEnumerable<CategoriaDTO>? categorias;
    private IEnumerable<EstadoDTO>? estados;

    [Inject] public NavigationManager NM { get; set; }
    [Inject] public NotificationService NS { get; set; }
    [Inject] public DialogService DialogService { get; set; }
    [Inject] public IProductService ProductService { get; set; }
    [Inject] public IMarcaService MarcaService { get; set; }
    [Inject] public ICategoriaService CategoriaService { get; set; }
    [Inject] public IEstadoService EstadoService { get; set; }

    class Model
    {
        public string? Name { get; set; }
        public decimal Precio { get; set; }
        public int Unidades { get; set; }
        public int? MarcaId { get; set; }
        public int EstadoId { get; set; }
        public List<int> CategoriaIds { get; set; } = new List<int>();
    }

    private Model model = new();
    private AccionABM accion;

    [Parameter]
    public int Id { get; set; }

    protected override void OnInitialized()
    {
        if (NM.Uri.Contains("ActualizarProducto"))
        {
            accion = AccionABM.Actualizar;
        }
        else if (NM.Uri.Contains("VerProducto"))
        {
            accion = AccionABM.Ver;
        }
        else
        {
            accion = AccionABM.Crear;
        }
    }

    protected override async Task OnInitializedAsync()
    {
        marcas = await MarcaService.Obtener();
        categorias = await CategoriaService.Obtener();
        estados = await EstadoService.Obtener();

        if (NM.Uri.Contains("ActualizarProducto") || NM.Uri.Contains("VerProducto"))
        {
            var producto = await ProductService.Obtener(Id);
            model.Name = producto.Name;
            model.Precio = producto.Precio;
            model.Unidades = producto.Unidades;
            model.MarcaId = producto.MarcaId;
            model.CategoriaIds = producto.Categorias.Select(x => x.Id).ToList();
            model.EstadoId = producto.EstadoId;
        }
    }

    private async Task Submit(Model model)
    {
        if (accion == AccionABM.Crear)
        {
            var result = await DialogService.Confirm("Desea agregar el producto?", "Validacion", new ConfirmOptions() { OkButtonText = "Si", CancelButtonText = "No" });

            if (result == false || result is null)
            {
                return;
            }

            try
            {
                await ProductService.Agregar(model.Name, model.Precio, model.Unidades, model.MarcaId, model.CategoriaIds);

                NS.Notify(new NotificationMessage
                {
                    Summary = "Operación exitosa",
                    Detail = "Producto agregado satisfactoriamente",
                    Severity = NotificationSeverity.Success,
                    Duration = 10000,
                });
            }
            catch (InvalidOperationException ex)
            {
                NS.Notify(new NotificationMessage
                {
                    Summary = "Error de validación",
                    Detail = $"{ex.Message}",
                    Severity = NotificationSeverity.Warning,
                    Duration = 10000,
                });
            }
            catch (Exception ex)
            {
                NS.Notify(new NotificationMessage
                {
                    Summary = "Ocurrió un error",
                    Detail = $"No se pudo agregar el producto. Contáctese con su administrador.",
                    Severity = NotificationSeverity.Error,
                    Duration = 10000,
                });
            }
        }
        else
        {
            var result = await DialogService.Confirm("Desea actualizar el producto?", "Validacion", new ConfirmOptions() { OkButtonText = "Si", CancelButtonText = "No" });

            if (result == false || result is null)
            {
                return;
            }

            try
            {
                await ProductService.Actualizar(Id, model.Name, model.Precio, model.Unidades, model.MarcaId, model.CategoriaIds, model.EstadoId);

                NS.Notify(new NotificationMessage
                {
                    Summary = "Operación exitosa",
                    Detail = "Producto actualizado satisfactoriamente",
                    Severity = NotificationSeverity.Success,
                    Duration = 10000,
                });
            }
            catch (InvalidOperationException ex)
            {
                NS.Notify(new NotificationMessage
                {
                    Summary = "Error de validación",
                    Detail = $"{ex.Message}",
                    Severity = NotificationSeverity.Warning,
                    Duration = 10000,
                });
            }
            catch (Exception ex)
            {
                NS.Notify(new NotificationMessage
                {
                    Summary = "Ocurrió un error",
                    Detail = $"No se pudo actualizar el producto. Contáctese con su administrador.",
                    Severity = NotificationSeverity.Error,
                    Duration = 10000,
                });
            }
        }

        NM.NavigateTo("/");
    }

    void Cancel()
    {
        NM.NavigateTo("/");
    }
}

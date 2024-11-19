using NearMindApp.Services;
using NearMindApp.Models;

namespace NearMindApp.Views;


public partial class ConfiguracionPage : ContentPage
{

    private readonly SupabaseService _supabaseService;

    public ConfiguracionPage()
    {
        InitializeComponent();
        _supabaseService = new SupabaseService();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await CargarPsicologos();
    }

    private async Task CargarPsicologos()
    {
        try
        {
            List<Usuario> psicologos = await _supabaseService.ObtenerElementosDeTabla<Usuario>();
            psicologos = psicologos.Where(u => u.rol == "Psicologo").ToList();
            PsicologosListView.ItemsSource = psicologos.Where(u => u.validado.Equals(false)).ToList();
        }
        catch (Exception ex)
        {
            // Manejar el error
            Console.WriteLine($"Error al cargar psicólogos: {ex.Message}");
        }
    }

    private async void OnPsicologoTapped(object sender, ItemTappedEventArgs e)
    {
        if (e.Item is Usuario usuarioSeleccionado)
    {
        string detalles = $"Nombre: {usuarioSeleccionado.nombre}\n" +
                          $"Email: {usuarioSeleccionado.email}\n" +
                          $"Teléfono: {usuarioSeleccionado.telefono}\n" +
                          $"Validado: {(usuarioSeleccionado.validado ? "Sí" : "No")}";

        var confirmacion = await DisplayAlert("Detalles del Psicólogo", detalles + "\n¿Validar este usuario?", "Sí", "No");

        if (confirmacion)
        {
            usuarioSeleccionado.validado = true;
            await _supabaseService.ActualizarElementoEnTabla(usuarioSeleccionado.id, usuarioSeleccionado); 
            await DisplayAlert("Éxito", "El usuario ha sido validado.", "Cerrar");
        }
    }
        
    }

}
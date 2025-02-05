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
            PsicologosListView.ItemsSource = psicologos.ToList();
        }
        catch (Exception ex)
        {
            // Manejar el error
            Console.WriteLine($"Error al cargar psicólogos: {ex.Message}");
        }
    }

    private async void OnPsicologoTapped(object sender, ItemTappedEventArgs e)
    {
        
    }

}
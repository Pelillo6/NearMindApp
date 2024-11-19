using NearMindApp.Services;
using NearMindApp.Models;

namespace NearMindApp.Views;

public partial class BuscadorPage : ContentPage
{
    private SupabaseService _supabaseService;

    public BuscadorPage()
	{
		InitializeComponent();
        _supabaseService = new SupabaseService();
    }

    // M�todo ejecutado al hacer clic en el bot�n
    private async void OnObtenerUsuariosClicked(object sender, EventArgs e)
    {
        try
        {
            // Llamada al m�todo para obtener los elementos de la tabla Usuario
            List<Usuario> usuarios = await _supabaseService.ObtenerElementosDeTabla<Usuario>();

            // Muestra los resultados (por ejemplo, en la consola)
            foreach (var usuario in usuarios)
            {
                Console.WriteLine($"Nombre: {usuario.nombre}, Email: {usuario.email}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al obtener usuarios: {ex.Message}");
        }
    }
}
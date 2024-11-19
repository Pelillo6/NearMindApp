using NearMindApp.Services;
using NearMindApp.Models;

namespace NearMindApp.Views;

public partial class BuscadorPage : ContentPage
{
    private SupabaseService _supabaseService;
    private string _rolUsuario;

    public BuscadorPage()
    {
        InitializeComponent();
        _supabaseService = new SupabaseService();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        try
        {
            List<Usuario> usuarios = await _supabaseService.ObtenerElementosDeTabla<Usuario>();
            var usuario = UsuarioService.Instance.UsuarioActual;

            // Filtrar la lista según el rol
            if (usuario.rol == "Psicologo")
            {
                UsuariosCollectionView.ItemsSource = usuarios.Where(u => u.rol == "Paciente").ToList();
            }
            else if (usuario.rol == "Paciente")
            {
                UsuariosCollectionView.ItemsSource = usuarios.Where(u => u.rol == "Psicologo").ToList();
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"No se pudieron cargar los usuarios: {ex.Message}", "OK");
        }
    }

    private async void OnUsuarioSeleccionado(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is Usuario usuarioSeleccionado)
        {
            string detalles = $"Nombre: {usuarioSeleccionado.nombre}\n" +
                              $"Email: {usuarioSeleccionado.email}\n" +
                              $"Teléfono: {usuarioSeleccionado.telefono}";

            await DisplayAlert("Detalles del Usuario", detalles, "Cerrar");
        }
    }

    // Método ejecutado al hacer clic en el botón
    private async void OnObtenerUsuariosClicked(object sender, EventArgs e)
    {
        try
        {
            // Llamada al método para obtener los elementos de la tabla Usuario
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
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
        BindingContext = this;
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

    public string ListaDeUsuariosTexto
    {
        get
        {
            var usuario = UsuarioService.Instance.UsuarioActual;
            return usuario.rol == "Paciente" ? "Lista de Psicólogos" : "Lista de Pacientes";
        }
    }

    private async void OnUsuarioSeleccionado(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is Usuario usuarioSeleccionado)
        {
            string detalles = $"Nombre: {usuarioSeleccionado.nombre}\n" +
                              $"Email: {usuarioSeleccionado.email}\n" +
                              $"Teléfono: {usuarioSeleccionado.telefono}\n";

            if (UsuarioService.Instance.UsuarioActual.rol == "Paciente") {
                detalles += $"Especialidad: {usuarioSeleccionado.especialidad} \n"; 
            }

            await DisplayAlert("Detalles del Usuario", detalles, "Cerrar");
        }
    }

    private async void OnUsuarioSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is Usuario usuarioSeleccionado)
        {
            await Navigation.PushAsync(new ChatPage(usuarioSeleccionado));
        }
    }
}
using NearMindApp.Services;
using NearMindApp.Models;

namespace NearMindApp.Views;

public partial class BuscadorPage : ContentPage
{
    private SupabaseService _supabaseService;
    private List<Usuario> _usuariosOriginales;
    private List<Usuario> _usuariosFiltrados; 

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
            var usuario = UsuarioService.Instance.GetUsuarioActual();

            // Filtrar la lista según el rol
            if (usuario.rol == "Psicologo")
            {
                UsuariosCollectionView.ItemsSource = usuarios.Where(u => u.rol == "Paciente").ToList();
            }
            else if (usuario.rol == "Paciente")
            {
                UsuariosCollectionView.ItemsSource = usuarios.Where(u => u.rol == "Psicologo").ToList();
            }
            _usuariosOriginales = usuarios.Where(u => u.rol == "Psicologo").ToList();
            _usuariosFiltrados = new List<Usuario>(_usuariosOriginales);
            UsuariosCollectionView.ItemsSource = _usuariosFiltrados;
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
            var usuario = UsuarioService.Instance.GetUsuarioActual();
            return usuario.rol == "Paciente" ? "Lista de Psicólogos" : "Lista de Pacientes";
        }
    }

    private void OnFiltroChanged(object sender, EventArgs e)
    {

        // Obtenemos los valores actuales de los filtros
        string textoFiltro = SearchBarFiltro.Text?.ToLower() ?? string.Empty;
        string filtroValoracion = PickerValoracion.SelectedItem?.ToString() ?? "Todos";

        // Filtramos la lista
        _usuariosFiltrados = _usuariosOriginales
     .Where(u =>
         u != null && // Validar que el objeto no sea nulo
         (
             // Filtro por nombre o ubicación
             (!string.IsNullOrWhiteSpace(u.nombre) && u.nombre.ToLower().Contains(textoFiltro)) ||
             (!string.IsNullOrWhiteSpace(u.ubicacion) && u.ubicacion.ToLower().Contains(textoFiltro))
         ) &&
         // Filtro por valoración
         (filtroValoracion == "Todos" ||
          (filtroValoracion == "9.0 o más" && (u.valoracion_media ?? 0) >= 9.0) ||
          (filtroValoracion == "8.0 o más" && (u.valoracion_media ?? 0) >= 8.0) ||
          (filtroValoracion == "7.0 o más" && (u.valoracion_media ?? 0) >= 7.0) ||
          (filtroValoracion == "6.0 o más" && (u.valoracion_media ?? 0) >= 6.0))
     )
     .ToList();

        // Actualizamos la vista
        UsuariosCollectionView.ItemsSource = _usuariosFiltrados;
    }

    private async void OnUsuarioSeleccionado(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is Usuario usuarioSeleccionado)
        {
            string detalles = $"Nombre: {usuarioSeleccionado.nombre}\n" +
                              $"Email: {usuarioSeleccionado.email}\n" +
                              $"Teléfono: {usuarioSeleccionado.telefono}\n";

            if (UsuarioService.Instance.GetUsuarioActual().rol == "Paciente") {
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
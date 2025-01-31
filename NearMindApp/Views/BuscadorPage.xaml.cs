using NearMindApp.Services;
using NearMindApp.Models;
using NearMindApp.Utilidades;
using System.Collections.ObjectModel;

namespace NearMindApp.Views;

public partial class BuscadorPage : ContentPage
{
    private SupabaseService _supabaseService;
    private ChatService _chatService;
    private List<BuscadorItem> _itemsOriginales;
    private List<BuscadorItem> _itemsFiltrados;
    private ObservableCollection<BuscadorItem> ListaPsicologos { get; set; }

    public BuscadorPage()
    {
        InitializeComponent();
        _supabaseService = new SupabaseService();
        _chatService = new ChatService();
        BindingContext = this;
        ListaPsicologos = new ObservableCollection<BuscadorItem>();
        PsicologosListView.ItemsSource = ListaPsicologos;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        try
        {
            var storage = _supabaseService.GetClient().Storage;
            var bucket = storage.From("imagenes-perfil");
            List<Usuario> usuarios = await _supabaseService.ObtenerElementosDeTabla<Usuario>();
            var usuario = UsuarioService.Instance.GetUsuarioActual();

            // Obtener las conversaciones del usuario actual
            var conversaciones = await _chatService.ObtenerConversacionesAsync(usuario.id);

            var psicologos = usuarios.Where(u => u.rol == "Psicologo" && !conversaciones.Contains(u.id)).ToList();

            ListaPsicologos.Clear();
            foreach (var psicologo in psicologos) {
                var imagenPerfilUrl = !string.IsNullOrEmpty(psicologo.imagen_perfil) ? bucket.GetPublicUrl(psicologo.imagen_perfil) : "anonimo.svg";
                var precioFormated = psicologo.precio.ToString();

                ListaPsicologos.Add(new BuscadorItem
                {
                    UsuarioId = psicologo.id,
                    NombreUsuario = psicologo.nombre.ToString(),
                    ImagenPerfil = imagenPerfilUrl,
                    Ciudad = psicologo.ubicacion,
                    Especialidad = psicologo.especialidad,
                    PrecioHora = precioFormated
                });
            }

        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"No se pudieron cargar los usuarios: {ex.Message}", "OK");
        }
    }

    private void OnFiltroChanged(object sender, EventArgs e)
    {
        string textoFiltro = SearchBarFiltro.Text?.ToLower() ?? string.Empty;
        _itemsFiltrados = ListaPsicologos.Where(i => i != null && (
             (!string.IsNullOrWhiteSpace(i.NombreUsuario) && i.NombreUsuario.ToLower().Contains(textoFiltro)) ||
             (!string.IsNullOrWhiteSpace(i.Especialidad) && i.Especialidad.ToLower().Contains(textoFiltro)) ||
             (!string.IsNullOrWhiteSpace(i.PrecioHora) && i.PrecioHora.ToLower().Contains(textoFiltro))
             )).ToList();

        PsicologosListView.ItemsSource = _itemsFiltrados;

    }

    private async void OnFrameTapped(object sender, EventArgs e)
    {
        if (sender is Frame frame && frame.BindingContext is BuscadorItem usuarioSeleccionado)
        {
            await Navigation.PushAsync(new PerfilPsicologoPage(usuarioSeleccionado.UsuarioId));
        }
    }

}
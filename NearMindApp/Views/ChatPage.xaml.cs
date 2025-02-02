using NearMindApp.Models;
using NearMindApp.Services;
using NearMindApp.Utilidades;
using System.Collections.ObjectModel;

namespace NearMindApp.Views;

public partial class ChatPage : ContentPage
{
    private readonly ChatService _chatService;
    private Guid _idUsuario;
    private Usuario _usuarioEmisor;
    private Usuario _usuarioReceptor;
    private SupabaseService _supabaseService;

    public ObservableCollection<Message> Messages { get; set; }

    public ChatPage(Guid idUsuario)
    {
        InitializeComponent();
        _supabaseService = new SupabaseService();
        _idUsuario = idUsuario;
        _usuarioEmisor = UsuarioService.Instance.GetUsuarioActual();

        _chatService = new ChatService();
        Messages = new ObservableCollection<Message>();

        MessagesCollectionView.ItemsSource = Messages;

        CargarMensajes();
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();

        var response = await _supabaseService.GetClient().From<Usuario>().Where(u => u.id == _idUsuario).Get();
        _usuarioReceptor = response.Models.FirstOrDefault();

        if (!string.IsNullOrEmpty(_usuarioReceptor.imagen_perfil))
        {
            var storage = _supabaseService.GetClient().Storage;
            var bucket = storage.From("imagenes-perfil");

            ImagenPerfil.Source = bucket.GetPublicUrl(_usuarioReceptor.imagen_perfil);
        }
        Nombre.Text = _usuarioReceptor.nombre;
    }

    private async void CargarMensajes()
    {
        // Carga mensajes históricos
        var mensajes = await _chatService.ObtenerMensajesAsync(_usuarioEmisor.id, _idUsuario);
        foreach (var mensaje in mensajes)
        {
            // Busca el nombre del emisor
            var usuario = await UsuarioService.Instance.ObtenerUsuarioPorId(mensaje.emisorId);
            mensaje.NombreEmisor = usuario?.nombre ?? "Desconocido";
            mensaje.EsEnviado = usuario.id == _usuarioEmisor.id;
            Messages.Add(mensaje);
        }


        await _chatService.SubscribeToMessagesAsync(_usuarioEmisor.id, _idUsuario, Messages);
    }

    private async void OnSendMessageClicked(object sender, EventArgs e)
    {
        var texto = MessageEntry.Text;
        if (!string.IsNullOrWhiteSpace(texto))
        {
            await _chatService.SendMessageAsync(_usuarioEmisor.id, _idUsuario, texto);
            MessageEntry.Text = string.Empty;
        }
    }
    

    private async void OnAbrirSelectorFecha(object sender, EventArgs e)
    {
        var response = await _supabaseService.GetClient().From<Usuario>().Where(u => u.id == _idUsuario).Get();
        _usuarioReceptor = response.Models.FirstOrDefault();
        Application.Current.MainPage = new CrearCitaPage(_usuarioReceptor.id);
    }

    
    private async void OnSalirTapped(object sender, EventArgs e)
    {
        ((App)Application.Current).MostrarAppShell();
        await Shell.Current.GoToAsync("//ChatsListPage");
    }


}
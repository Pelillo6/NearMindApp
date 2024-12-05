using NearMindApp.Models;
using NearMindApp.Utilidades;
using System.Collections.ObjectModel;

namespace NearMindApp.Views;

public partial class ChatPage : ContentPage
{
    private readonly ChatService _chatService;
    private Usuario _usuarioDestino;

    public ObservableCollection<Message> Messages { get; set; }

    public ChatPage(Usuario usuarioDestino)
    {
        InitializeComponent();

        _usuarioDestino = usuarioDestino;
        Title = $"Chat con {_usuarioDestino.nombre}";

        _chatService = new ChatService();
        Messages = new ObservableCollection<Message>();

        // Asocia la colección a la lista de mensajes en la interfaz
        MessagesCollectionView.ItemsSource = Messages;

        // Carga mensajes existentes y suscríbete a mensajes nuevos
        CargarMensajes();
    }

    private async void CargarMensajes()
    {
        // Carga mensajes históricos
        var mensajes = await _chatService.ObtenerMensajesAsync(Guid.Empty, _usuarioDestino.id);
        foreach (var mensaje in mensajes)
        {
            Messages.Add(mensaje);
        }

        // Suscríbete a nuevos mensajes en tiempo real
        await _chatService.SubscribeToMessagesAsync(Guid.Empty, _usuarioDestino.id, Messages);
    }

    private async void OnSendMessageClicked(object sender, EventArgs e)
    {
        var texto = MessageEntry.Text;
        if (!string.IsNullOrWhiteSpace(texto))
        {
            await _chatService.SendMessageAsync(Guid.Empty, _usuarioDestino.id, texto);
            MessageEntry.Text = string.Empty;
        }
    }
}
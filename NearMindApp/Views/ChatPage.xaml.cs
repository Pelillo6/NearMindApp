using NearMindApp.Models;
using NearMindApp.Utilidades;
using System.Collections.ObjectModel;

namespace NearMindApp.Views;

public partial class ChatPage : ContentPage
{
    private readonly ChatService _chatService;
    public ObservableCollection<Message> Messages { get; set; }

    public ChatPage()
	{
		InitializeComponent();

        _chatService = new ChatService();
        Messages = _chatService.Messages;

        // Asocia la colección a la lista de mensajes en la interfaz
        MessagesCollectionView.ItemsSource = Messages;

        // Conecta con Firebase para recibir mensajes en tiempo real
        _ = _chatService.SubscribeToMessagesAsync();
    }
    private async void OnSendMessageClicked(object sender, EventArgs e)
    {
        var message = MessageEntry.Text;
        if (!string.IsNullOrWhiteSpace(message))
        {
            await _chatService.SendMessageAsync("UsuarioActual", message);
            MessageEntry.Text = string.Empty;
        }
    }
}
using NearMindApp.Views;
using Microsoft.Maui.Controls;
using NearMindApp.Models;
using NearMindApp.Utilidades;
using NearMindApp.Services;
using System.Collections.ObjectModel;
using Supabase.Storage;
using System.Net.Sockets;

namespace NearMindApp.Views
{
    public partial class ChatsListPage : ContentPage
    {
        private Usuario _usuarioActual;
        private readonly ChatService _chatService;
        private SupabaseService _supabaseService;
        public ObservableCollection<ChatItem> Chats { get; set; }

        public ChatsListPage()
        {
            InitializeComponent();
            _usuarioActual = UsuarioService.Instance.GetUsuarioActual();
            _chatService = new ChatService();
            Chats = new ObservableCollection<ChatItem>();
            _supabaseService = new SupabaseService();
            ChatsListView.ItemsSource = Chats;
            
            CargarChats();

            _chatService.SubscribeToConversaciones(_usuarioActual.id, OnNuevaConversacion);
        }

        private async void CargarChats()
        {
            // Obtener las claves de las conversaciones
            var conversaciones = await _chatService.ObtenerConversacionesAsync(_usuarioActual.id);
            var storage = _supabaseService.GetClient().Storage;
            var bucket = storage.From("imagenes-perfil");

            Chats.Clear();
            foreach (var conversacion in conversaciones)
            {
                var usuarioId = conversacion; // ID del usuario con el que se conversa
                var usuario = await UsuarioService.Instance.ObtenerUsuarioPorId(usuarioId);
                
                if (usuario != null)
                {
                    var imagenPerfilUrl = !string.IsNullOrEmpty(usuario.imagen_perfil) ? bucket.GetPublicUrl(usuario.imagen_perfil) : "anonimo.svg";
                    Chats.Add(new ChatItem
                    {
                        UsuarioId = usuarioId,
                        NombreUsuario = usuario.nombre,
                        ImagenPerfil = imagenPerfilUrl
                    });
                }
            }
        }

        private async void OnChatSelected(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.FirstOrDefault() is ChatItem chatItem)
            {
                var usuarioDestino = await UsuarioService.Instance.ObtenerUsuarioPorId(chatItem.UsuarioId);
                if (usuarioDestino != null)
                {
                    await Navigation.PushAsync(new ChatPage(usuarioDestino));
                }
            }
        }

        private async void OnNuevaConversacion(Guid usuarioId)
        {
            var storage = _supabaseService.GetClient().Storage;
            var bucket = storage.From("imagenes-perfil");

            if (Chats.Any(c => c.UsuarioId == usuarioId))
                return;

            var usuario = await UsuarioService.Instance.ObtenerUsuarioPorId(usuarioId);
            if (usuario != null)
            {
                var imagenPerfilUrl = !string.IsNullOrEmpty(usuario.imagen_perfil) ? bucket.GetPublicUrl(usuario.imagen_perfil) : "anonimo.svg";

                Chats.Add(new ChatItem
                {
                    UsuarioId = usuarioId,
                    NombreUsuario = usuario.nombre,
                    ImagenPerfil = imagenPerfilUrl
                });
            }
        }
    }
}

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

    public ObservableCollection<Message> Messages { get; set; }

    public ChatPage(Guid idUsuario)
    {
        InitializeComponent();
        _idUsuario = idUsuario;
        _usuarioEmisor = UsuarioService.Instance.GetUsuarioActual();

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
        var mensajes = await _chatService.ObtenerMensajesAsync(_usuarioEmisor.id, _idUsuario);
        foreach (var mensaje in mensajes)
        {
            // Busca el nombre del emisor
            var usuario = await UsuarioService.Instance.ObtenerUsuarioPorId(mensaje.emisorId);
            mensaje.NombreEmisor = usuario?.nombre ?? "Desconocido";
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
    private async void OnSolicitarCitaClicked(object sender, EventArgs e)
    {
        try
        {
            // Mostrar un selector de fecha
            var fechaSeleccionada = await MostrarSelectorDeFecha();
            if (fechaSeleccionada == null)
            {
                await DisplayAlert("Aviso", "No se seleccionó una fecha.", "OK");
                return;
            }

            // Crear un nuevo objeto de cita
            var nuevaCita = new Cita
            {
                usuario1_id = _usuarioEmisor.id, // ID del usuario actual (emisor)
                usuario2_id = _idUsuario, // ID del destinatario
                fecha = fechaSeleccionada.Value, // Fecha seleccionada
                nota = $"Solicitud de cita de {_usuarioEmisor.nombre}"
            };

            // Llamar al servicio para insertar la cita
            var supabaseService = new SupabaseService();
            await supabaseService.InsertarElementoEnTabla<Cita>(nuevaCita);

            // Mostrar un mensaje de confirmación
            await DisplayAlert("Éxito", "La solicitud de cita se ha enviado correctamente.", "OK");
        }
        catch (Exception ex)
        {
            // Mostrar un mensaje de error
            await DisplayAlert("Error", $"No se pudo solicitar la cita: {ex.Message}", "OK");
        }
    }
    private async Task<DateTime?> MostrarSelectorDeFecha()
    {
        var tcs = new TaskCompletionSource<DateTime?>();

        var picker = new DatePicker
        {
            MinimumDate = DateTime.Now,
            MaximumDate = DateTime.Now.AddYears(1)
        };

        var aceptar = new Button { Text = "Aceptar" };
        aceptar.Clicked += (s, e) => tcs.TrySetResult(picker.Date);

        var cancelar = new Button { Text = "Cancelar" };
        cancelar.Clicked += (s, e) => tcs.TrySetResult(null);

        var layout = new StackLayout
        {
            Padding = 10,
            Children = { picker, aceptar, cancelar }
        };

        var page = new ContentPage
        {
            Content = layout
        };

        await Navigation.PushModalAsync(page);
        var resultado = await tcs.Task;
        await Navigation.PopModalAsync();
        return resultado;
    }
    private async void OnSalirTapped(object sender, EventArgs e)
    {
        ((App)Application.Current).MostrarAppShell();
        await Shell.Current.GoToAsync("//BuscadorPage");
    }
}
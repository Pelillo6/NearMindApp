using CommunityToolkit.Maui.Views;
using NearMindApp.Models;
using NearMindApp.Services;
using Syncfusion.Maui.Calendar;
using System.Collections.ObjectModel;
using System.Globalization;

namespace NearMindApp.Views;

public partial class CalendarioPage : ContentPage
{
    private DateTime diaSeleccionado;
    private List<Evento> eventosUsuario = new List<Evento>();

    private SupabaseService _supabaseService;
    private DateTime _currentMonth;
    public CalendarioPage()
	{
        InitializeComponent();
        _supabaseService = new SupabaseService();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        diaSeleccionado = DateTime.Now;
        eventosUsuario = await ObtenerEventosDelUsuario();

        Console.WriteLine($"Eventos obtenidos: {eventosUsuario.Count}");

        // Si eventosUsuario está vacío, puedes mostrar un mensaje o un calendario sin eventos
        if (eventosUsuario.Count == 0)
        {
            Console.WriteLine("No hay eventos para mostrar.");
        }
    }

    private async void OnSelectionChanged(object sender, Syncfusion.Maui.Calendar.CalendarSelectionChangedEventArgs e)
    {
        var fechaSeleccionada = e.NewValue as DateTime?;
        if (fechaSeleccionada == null) return;

        if(e.NewValue != null) {
            diaSeleccionado = (DateTime)e.NewValue;
        }

        if (eventosUsuario != null) { 
            var eventosDelDia = eventosUsuario
                .Where(ev => ev.fecha == fechaSeleccionada)
                .ToList();

            EventosStack.Children.Clear();

            if (eventosDelDia.Any())
            {
                EventosStack.IsVisible = true;

                foreach (var evento in eventosDelDia)
                {
                    var frame = new Frame
                    {
                        BorderColor = Color.FromRgba(0, 0, 0, 0),
                        CornerRadius = 20,
                        Padding = 10,
                        Margin = new Thickness(0, 10),
                        Opacity = 0
                    };

                    var stackLayout = new StackLayout
                    {
                        Spacing = 10
                    };

                    var nombreLabel = new Label
                    {
                        Text = evento.nombre,
                        FontAttributes = FontAttributes.Bold,
                        FontSize = 18,
                        TextColor = Color.FromRgba("#53518C")
                    };

                    var observacionesLabel = new Label
                    {
                        Text = evento.observaciones,
                        FontSize = 14,
                        TextColor = Color.FromRgba("#53518C")
                    };

                    var fechaLabel = new Label
                    {
                        Text = evento.fecha.ToString("dd/MM/yyyy") + " " + evento.hora.ToString(@"hh\:mm"),
                        FontSize = 14,
                        TextColor = Color.FromRgba("#53518C"),
                        HorizontalTextAlignment = TextAlignment.End
                    };

                    stackLayout.Children.Add(nombreLabel);
                    stackLayout.Children.Add(observacionesLabel);
                    stackLayout.Children.Add(fechaLabel);

                    frame.Content = stackLayout;
                    EventosStack.Children.Add(frame);

                    await frame.FadeTo(1, 500); 
                    await Task.Delay(300);
                }
            }
            else
            {
                EventosStack.IsVisible = false;
            }
        }
    }

    

    private async Task<List<Evento>> ObtenerEventosDelUsuario()
    {
        var usuarioActual = UsuarioService.Instance.GetUsuarioActual();
        if (usuarioActual == null)
        {
            Console.WriteLine("Usuario no encontrado.");
            return null;
        }

        // Filtrar eventos por usuario_id
        var eventos = await _supabaseService.ObtenerElementosDeTabla<Evento>();
        Console.WriteLine($"Eventos obtenidos: {eventos.ToString()}");

        return eventos.Where(e => e.usuario_id == usuarioActual.id).ToList();
    }

    private async void OnCrearEventoClicked(object sender, EventArgs e)
    {
        Application.Current.MainPage = new CrearEventoPage(diaSeleccionado);
    }

}
using NearMindApp.Services;
using NearMindApp.Models;

namespace NearMindApp.Views;

public partial class CrearCitaPage : ContentPage
{
    private Usuario _usuario;
    private Usuario _usuarioActual;
    private Guid _idUsuario;
    private SupabaseService _supabaseService;
    public CrearCitaPage(Guid idUsuario)
    {
        InitializeComponent();
        _supabaseService = new SupabaseService();
        _usuarioActual = UsuarioService.Instance.GetUsuarioActual();
        _idUsuario = idUsuario;
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();

        var response = await _supabaseService.GetClient().From<Usuario>().Where(u => u.id == _idUsuario).Get();
        _usuario = response.Models.FirstOrDefault();

        TituloLabel.Text = "Solicitud de cita con " + _usuarioActual.nombre;
        DescripcionEntry.Placeholder = "Opcional: Introduce una descripcion de la cita";

    }

    private async void OnCrearClicked(object sender, EventArgs e)
    {
        var response = await _supabaseService.GetClient().From<Usuario>().Where(u => u.id == _idUsuario).Get();
        _usuario = response.Models.FirstOrDefault();

        string descripcion = DescripcionEntry.Text;
        TimeSpan hora = HoraPicker.Time;
        DateTime fecha = FecCita.Date;

        if (fecha == null || hora == null)
        {
            DisplayAlert("Error", "Faltan campos obligatorios.", "OK");
            return;
        }

        var cita = new Cita
        {
            nombre = "Solicitud de cita con " + _usuarioActual.nombre,
            fecha = fecha,
            hora = hora,
            usuario1_id = _usuarioActual.id,
            usuario2_id = _usuario.id,
            observaciones = descripcion
        };

        var resultadoEvento = await _supabaseService.GetClient().From<Cita>().Insert(cita);

        ((App)Application.Current).MostrarAppShell();
        await Shell.Current.GoToAsync("//ChatsListPage");
    }

    private async void OnCancelarClicked(object sender, EventArgs e)
    {
        ((App)Application.Current).MostrarAppShell();
        await Shell.Current.GoToAsync("//ChatsListPage");
    }



    }
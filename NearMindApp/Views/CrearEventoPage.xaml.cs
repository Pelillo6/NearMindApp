using NearMindApp.Services;
using NearMindApp.Models;

namespace NearMindApp.Views;

public partial class CrearEventoPage : ContentPage
{
    private Usuario _usuarioActual;
    private DateTime diaEvento;
    private SupabaseService _supabaseService;
    public CrearEventoPage(DateTime diaSeleccionado)
    {
        InitializeComponent();
        _supabaseService = new SupabaseService();
        NavigationPage.SetHasNavigationBar(this, false);
        diaEvento = diaSeleccionado;
        _usuarioActual = UsuarioService.Instance.GetUsuarioActual();
    }

    private async void OnCrearClicked(object sender, EventArgs e)
    {
        // Obtener los valores ingresados
        string nombre = NombreEntry.Text;
        string descripcion = DescripcionEntry.Text;
        TimeSpan hora = HoraPicker.Time;

        // Validar los datos
        if (string.IsNullOrWhiteSpace(nombre) || hora == null)
        {
            DisplayAlert("Error", "Faltan campos obligatorios.", "OK");
            return;
        }

        var nuevoEvento = new Evento
        {
            nombre = nombre,
            observaciones = descripcion,
            fecha = diaEvento,
            hora = hora,
            usuario_id = _usuarioActual.id
            
        };

        var resultadoEvento = await _supabaseService.GetClient().From<Evento>().Insert(nuevoEvento);

        ((App)Application.Current).MostrarAppShell();
    }

    private void OnCancelarClicked(object sender, EventArgs e)
    {
        ((App)Application.Current).MostrarAppShell();
    }



    }
using NearMindApp.Services;
using System.Collections.ObjectModel;
using NearMindApp.Models;
namespace NearMindApp.Views
{
    public partial class RegistroCitasPage : ContentPage
    {
        private readonly SupabaseService _supabaseService;
        private readonly Usuario _usuarioActual;

        public RegistroCitasPage()
        {
            InitializeComponent();
            _supabaseService = new SupabaseService();
            _usuarioActual = UsuarioService.Instance.GetUsuarioActual();
            CargarCitas();
        }

        private async void CargarCitas()
        {
            var usuarioActual = UsuarioService.Instance.GetUsuarioActual();
            var response = await _supabaseService.GetClient().From<Cita>().Where(c => c.usuario2_id == usuarioActual.id).Get();

            var citas = response.Models;

            CitasCollectionView.ItemsSource = citas;

        }

        private async void OnAceptarCitaClicked(object sender, EventArgs e)
        {
            var boton = sender as Button;
            var citaSeleccionada = boton?.BindingContext as Cita;

            var usuario1 = await _supabaseService.GetClient().From<Usuario>().Where(u => u.id == citaSeleccionada.usuario1_id).Get();
            var usuario2 = await _supabaseService.GetClient().From<Usuario>().Where(u => u.id == citaSeleccionada.usuario2_id).Get();

            var nuevoEvento = new Evento
            {
                nombre = "Cita con " + usuario1.Model.nombre,
                fecha = citaSeleccionada.fecha,
                hora = citaSeleccionada.hora,
                usuario_id = usuario2.Model.id

            };

            var nuevoEvento2 = new Evento
            {
                nombre = "Cita con " + usuario2.Model.nombre,
                fecha = citaSeleccionada.fecha,
                hora = citaSeleccionada.hora,
                usuario_id = usuario1.Model.id

            };
            
            await _supabaseService.GetClient().From<Cita>().Delete(citaSeleccionada);

            var resultadoEvento = await _supabaseService.GetClient().From<Evento>().Insert(nuevoEvento);
            var resultadoEvento2 = await _supabaseService.GetClient().From<Evento>().Insert(nuevoEvento2);

            await DisplayAlert("Éxito", "Has aceptado la cita.", "OK");
            CargarCitas();
        }

        private async void OnDenegarCitaClicked(object sender, EventArgs e)
        {
            var boton = sender as Button;
            var citaSeleccionada = boton?.BindingContext as Cita;

            await _supabaseService.GetClient().From<Cita>().Delete(citaSeleccionada);

            await DisplayAlert("Éxito", "Has cancelado la cita.", "OK");
            CargarCitas();
        }
    }

}
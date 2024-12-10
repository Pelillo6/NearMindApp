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
            var citas = await _supabaseService.ObtenerElementosDeTabla<Cita>();
            CitasCollectionView.ItemsSource = citas;
        }

        private async void OnAceptarCitaClicked(object sender, EventArgs e)
        {
            var boton = sender as Button;
            var citaSeleccionada = boton?.BindingContext as Cita;

            if (citaSeleccionada == null || _usuarioActual.id != citaSeleccionada.usuario2_id)
            {
                await DisplayAlert("Error", "No puedes aceptar esta cita.", "OK");
                return;
            }

            citaSeleccionada.nota = "Cita aceptada";

            await _supabaseService.ActualizarElementoEnTabla(citaSeleccionada.id, citaSeleccionada);

            await DisplayAlert("Éxito", "Has aceptado la cita.", "OK");
            CargarCitas(); // Recargar citas
        }
    }

}
using NearMindApp.Services;
using NearMindApp.Models;

namespace NearMindApp.Views;


public partial class ConfiguracionPage : ContentPage
{
    
        private readonly SupabaseService _supabaseService;

        public ConfiguracionPage()
        {
            InitializeComponent();
            _supabaseService = new SupabaseService();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await CargarPsicologos();
        }

        private async Task CargarPsicologos()
        {
            try
            {
            List<Psicologo> psicologos = await _supabaseService.ObtenerElementosDeTabla<Psicologo>();
            psicologos = psicologos.Where(u => u.rol == "Psicologo").ToList();
            PsicologosListView.ItemsSource = psicologos.Where(u => u.validado.Equals(false)).ToList();
        }
            catch (Exception ex)
            {
                // Manejar el error
                Console.WriteLine($"Error al cargar psicólogos: {ex.Message}");
            }
        }

        private void OnPsicologoTapped(object sender, ItemTappedEventArgs e)
        {
            var psicologo = e.Item as Psicologo;
            if (psicologo != null)
            {
                // Maneja la selección del psicólogo, por ejemplo, navegar a una página de detalles
                Console.WriteLine($"Psicólogo seleccionado: {psicologo.nombre}");
            }
        }
    
}
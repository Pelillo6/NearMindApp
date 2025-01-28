using NearMindApp.Views;
using Microsoft.Maui.Controls;

namespace NearMindApp.Views
{
    public partial class Administrador : ContentPage
    {
        public Administrador()
        {
            InitializeComponent();
        }
        private async void OnNavigateToListaSolicitudesClicked(object sender, EventArgs e)
        {
            // Navegamos a la página Lista de Solicitudes
            await Navigation.PushAsync(new solicitudPage());
        }
    }
}

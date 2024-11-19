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

        // M�todo que se llama cuando el bot�n es presionado
        private async void OnNavigateToListaSolicitudesClicked(object sender, EventArgs e)
        {
            // Navegamos a la p�gina Lista de Solicitudes
            await Navigation.PushAsync(new ListaSolicitudes());
        }
    }
}

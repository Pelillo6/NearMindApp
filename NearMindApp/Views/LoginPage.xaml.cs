using NearMindApp.ModelViews;
using NearMindApp.Services;
using Supabase;

namespace NearMindApp.Views
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();

            // Asignar el ViewModel con la inyecci�n del cliente de Supabase
            BindingContext = new LoginViewModel();
        }
    }
}
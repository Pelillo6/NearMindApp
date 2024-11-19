using NearMindApp.Views;

namespace NearMindApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new LoginPage());
        }

        public void MostrarAppShell()
        {
            // Cambia a AppShell después del login exitoso
            MainPage = new AppShell();
        }
    }
}

using NearMindApp.Views;
using Syncfusion.Licensing;
using System.Globalization;

namespace NearMindApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            Preferences.Get("language", "es-ES");
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("es-ES");
            CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("es-ES");
            MainPage = new NavigationPage(new HomePage());
        }

        public void MostrarAppShell()
        {
            // Cambia a AppShell después del login exitoso
            MainPage = new AppShell();
        }
    }
}

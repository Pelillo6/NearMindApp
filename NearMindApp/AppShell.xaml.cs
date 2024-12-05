using NearMindApp.Services;
using NearMindApp.Views;

namespace NearMindApp
{
    public partial class AppShell : Shell
    {
        public bool IsAdmin { get; set; }
        public bool IsNotAdmin { get; set; }
        public AppShell()
        {
            InitializeComponent();

            var usuario = UsuarioService.Instance.GetUsuarioActual();
            IsAdmin = usuario.rol == "Admin";
            IsNotAdmin = !IsAdmin;
            BindingContext = this;
        }

    }
}

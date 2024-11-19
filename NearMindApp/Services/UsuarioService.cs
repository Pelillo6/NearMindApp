using NearMindApp.Models;

namespace NearMindApp.Services
{
    public class UsuarioService
    {
        private static UsuarioService _instance;
        public Usuario UsuarioActual { get; private set; }

        private UsuarioService() { }

        public static UsuarioService Instance => _instance ??= new UsuarioService();

        public void SetUsuarioActual(Usuario usuario)
        {
            UsuarioActual = usuario;
        }

        public void CerrarSesion()
        {
            UsuarioActual = null;
        }
    }
}

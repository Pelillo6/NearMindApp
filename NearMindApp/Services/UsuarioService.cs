using NearMindApp.Models;

namespace NearMindApp.Services
{
    public class UsuarioService
    {
        private static readonly Lazy<UsuarioService> _instance = new(() => new UsuarioService());

        private Usuario _usuarioActual;

        private UsuarioService() { }

        public static UsuarioService Instance => _instance.Value;

        public void SetUsuarioActual(Usuario usuario)
        {
            _usuarioActual = usuario;
        }

        public Usuario GetUsuarioActual()
        {
            return _usuarioActual;
        }

        public bool IsUsuarioLogueado()
        {
            return _usuarioActual != null;
        }
    }
}

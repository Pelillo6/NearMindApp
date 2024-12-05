using NearMindApp.Models;

namespace NearMindApp.Services
{
    public class UsuarioService
    {
        private static readonly Lazy<UsuarioService> _instance = new(() => new UsuarioService());

        private Usuario _usuarioActual;
        private SupabaseService _supabaseService;

        private UsuarioService() {
            _supabaseService = new SupabaseService();
        }

        public static UsuarioService Instance => _instance.Value;

        public void SetUsuarioActual(Usuario usuario)
        {
            _usuarioActual = usuario;
        }

        public Usuario GetUsuarioActual()
        {
            return _usuarioActual;
        }

        public async Task<Usuario> ObtenerUsuarioPorId(Guid id)
        {
            var usuarios = await _supabaseService.ObtenerElementosDeTabla<Usuario>();
            return usuarios.FirstOrDefault(u => u.id == id);
        }

        public bool IsUsuarioLogueado()
        {
            return _usuarioActual != null;
        }
    }
}

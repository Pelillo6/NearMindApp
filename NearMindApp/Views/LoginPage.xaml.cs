using NearMindApp.Services;
using NearMindApp.Models;

namespace NearMindApp.Views;

public partial class LoginPage : ContentPage
{
    private SupabaseService _supabaseService;

    public LoginPage()
    {
        InitializeComponent();
        _supabaseService = new SupabaseService();
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        string email = EmailEntry.Text?.Trim();
        string password = PasswordEntry.Text?.Trim();

        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            ErrorLabel.Text = "Por favor, complete todos los campos.";
            ErrorLabel.IsVisible = true;
            return;
        }

        try
        {
            // Llamada a Supabase para autenticar al usuario
            List<Usuario> usuarios = await _supabaseService.ObtenerElementosDeTabla<Usuario>();
            Usuario usuarioEncontrado = usuarios
                .FirstOrDefault(u => u.Email == email && u.Password == password);

            if (usuarioEncontrado != null)
            {
                // Guardar el usuario en el servicio compartido
                UsuarioService.Instance.SetUsuarioActual(usuarioEncontrado);

                // Navegar a la página de búsqueda
                ((App)Application.Current).MostrarAppShell();

            }
            else
            {
                ErrorLabel.Text = "Email o contraseña incorrectos.";
                ErrorLabel.IsVisible = true;
            }
        }
        catch (Exception ex)
        {
            ErrorLabel.Text = $"Error al iniciar sesión: {ex.Message}";
            ErrorLabel.IsVisible = true;
        }
    }
}

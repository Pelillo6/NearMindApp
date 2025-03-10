using NearMindApp.Services;
using NearMindApp.Models;
using Supabase.Gotrue;

namespace NearMindApp.Views;

public partial class LoginPage : ContentPage
{
    private SupabaseService _supabaseService;

    public LoginPage()
    {
        InitializeComponent();
        _supabaseService = new SupabaseService();
    }

    private async void OnRegisterClicked(object sender, EventArgs e)
    {
        // Navegar a la p�gina de registro
        await Navigation.PushAsync(new HomePage());
    }
    private async void OnLoginClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(Correo.Text) || string.IsNullOrEmpty(Contra.Text))
        {
            ErrorLabel.Text = "Por favor, complete todos los campos.";
            ErrorLabel.IsVisible = true;
            return;
        }

        try
        {
            /* Llamada a Supabase para autenticar al usuario*/
            List<Usuario> usuarios = await _supabaseService.ObtenerElementosDeTabla<Usuario>();

            Console.WriteLine(usuarios.Count);

            Usuario usuarioEncontrado = usuarios
                .FirstOrDefault(u => u.email == Correo.Text && u.password == Contra.Text);

            if (usuarioEncontrado != null)
            {
                UsuarioService.Instance.SetUsuarioActual(usuarioEncontrado);
                ((App)Application.Current).MostrarAppShell();

            }
            else
            {
                ErrorLabel.Text = "Email o contrase�a incorrectos.";
                ErrorLabel.IsVisible = true;
            }
        }
        catch (Exception ex)
        {
            ErrorLabel.Text = $"Error al iniciar sesi�n: {ex.Message}";
            ErrorLabel.IsVisible = true;
        }
    }
}

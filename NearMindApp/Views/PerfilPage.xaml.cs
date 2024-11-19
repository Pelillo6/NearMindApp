using NearMindApp.Services;
using NearMindApp.Models;

namespace NearMindApp.Views;

public partial class PerfilPage : ContentPage
{
    private SupabaseService _supabaseService;
    private Usuario _usuarioActual;

    public PerfilPage()
    {
        InitializeComponent();
        _supabaseService = new SupabaseService();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        // Obtener el usuario logueado
        _usuarioActual = UsuarioService.Instance.UsuarioActual;
        if (_usuarioActual != null)
        {
            // Mostrar los datos del usuario en los campos
            NombreEntry.Text = _usuarioActual.Nombre;
            CorreoEntry.Text = _usuarioActual.Email;
            PasswordEntry.Text = _usuarioActual.Password;
        }
    }

    private async void OnGuardarCambiosClicked(object sender, EventArgs e)
    {
        // Validar los campos
        string nuevoNombre = NombreEntry.Text?.Trim();
        string nuevoCorreo = CorreoEntry.Text?.Trim();
        string nuevaPassword = PasswordEntry.Text?.Trim();

        if (string.IsNullOrEmpty(nuevoNombre) || string.IsNullOrEmpty(nuevoCorreo) || string.IsNullOrEmpty(nuevaPassword))
        {
            MensajeLabel.Text = "Por favor, complete todos los campos.";
            MensajeLabel.TextColor = Colors.Red;
            MensajeLabel.IsVisible = true;
            return;
        }

        try
        {
            // Actualizar los datos en la base de datos
            _usuarioActual.Nombre = nuevoNombre;
            _usuarioActual.Email = nuevoCorreo;
            _usuarioActual.Password = nuevaPassword;

            await _supabaseService.ActualizarElementoEnTabla(_usuarioActual.Id, _usuarioActual);

            // Actualizar el servicio compartido
            UsuarioService.Instance.SetUsuarioActual(_usuarioActual);

            // Mostrar mensaje de �xito
            MensajeLabel.Text = "Cambios guardados correctamente.";
            MensajeLabel.TextColor = Colors.Green;
            MensajeLabel.IsVisible = true;
        }
        catch (Exception ex)
        {
            MensajeLabel.Text = $"Error al guardar cambios: {ex.Message}";
            MensajeLabel.TextColor = Colors.Red;
            MensajeLabel.IsVisible = true;
        }
    }
}
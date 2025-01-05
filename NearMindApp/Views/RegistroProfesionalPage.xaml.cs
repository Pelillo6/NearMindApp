using NearMindApp.Services;
using NearMindApp.Models;

namespace NearMindApp.Views;

public partial class RegistroProfesionalPage : ContentPage
{
    private SupabaseService _supabaseService;
    public RegistroProfesionalPage()
    {
        InitializeComponent();
        _supabaseService = new SupabaseService();
    }

    private async void OnSiguienteClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(Nombre.Text) || string.IsNullOrEmpty(Correo.Text) || string.IsNullOrEmpty(Contraseña.Text))
        {
            MensajeLabel.Text = "Los campos Nombre, Email y Contraseña son obligatorios";
            MensajeLabel.IsVisible = true;
            return;
        }

        DateTime fechaNacimiento = FecNacimiento.Date; // La fecha seleccionada en el DatePicker
        DateTime fechaLimite = DateTime.Now.AddYears(-18); // Fecha límite para ser mayor de 18 años

        if (fechaNacimiento > fechaLimite)
        {
            MensajeLabel.Text = "Debes tener al menos 18 años para registrarte.";
            MensajeLabel.IsVisible = true;
            return;
        }

        MensajeLabel.IsVisible = false;

        var psicologo = new Usuario
        {
            nombre = Nombre.Text,
            email = Correo.Text,
            password = Contraseña.Text,
            fecha_nacimiento = FecNacimiento.Date,
            telefono = Telefono.Text,
            rol = "Psicologo",
            ubicacion = Ubicacion.Text
        };

        await Navigation.PushAsync(new RegistroPsicologoPage(psicologo)); 
    }
    private async void OnIniciarSesionClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new LoginPage());
    }
}
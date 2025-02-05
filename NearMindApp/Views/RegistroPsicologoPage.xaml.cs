using NearMindApp.Services;
using NearMindApp.Models;

namespace NearMindApp.Views;

public partial class RegistroPsicologoPage : ContentPage
{
    private SupabaseService _supabaseService;
    private Usuario datosPsicologo;

    public RegistroPsicologoPage(Usuario psicologo)
    {
        InitializeComponent();
        _supabaseService = new SupabaseService();
        datosPsicologo = psicologo;
    }

    private async void OnRegistrarClicked(object sender, EventArgs e)
    {

        if (string.IsNullOrEmpty(Descripcion.Text) || string.IsNullOrEmpty(Precio.Text) || string.IsNullOrEmpty(Especialidad.Text))
        {
            MensajeLabel.Text = "Los campos Descripción, Especialidad y Precio son obligatorios";
            MensajeLabel.IsVisible = true;
            return;
        }
        double parsedPrecio;

        if (double.TryParse(Precio.Text, out parsedPrecio))
        {
            datosPsicologo.precio = parsedPrecio;
        }
        else
        {
            MensajeLabel.Text = "El campo Precio debe contener un valor numérico válido.";
            MensajeLabel.IsVisible = true;
            return;
        }

        MensajeLabel.IsVisible = false;

        datosPsicologo.precio = parsedPrecio;
        datosPsicologo.descripcion = Descripcion.Text;
        datosPsicologo.especialidad= Especialidad.Text;

        var resultadoPsicologo = await _supabaseService.GetClient().From<Usuario>().Insert(datosPsicologo);
        if (resultadoPsicologo.Models?.Any() == true)
        {
            UsuarioService.Instance.SetUsuarioActual(datosPsicologo);
            ((App)Application.Current).MostrarAppShell();
        }
        else
        {
            MensajeLabel.Text = "No se pudo registrar al psicólogo.";
            MensajeLabel.IsVisible = true;
        }
      
    }

}
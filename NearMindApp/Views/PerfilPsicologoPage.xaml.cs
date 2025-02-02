using NearMindApp.Models;
using NearMindApp.Services;
using NearMindApp.Utilidades;
using Supabase;
using System.Collections.ObjectModel;

namespace NearMindApp.Views;

public partial class PerfilPsicologoPage : ContentPage
{
    private Guid _idUsuario;
    private Usuario _psicologo;
    private SupabaseService _supabaseService;


    public PerfilPsicologoPage(Guid idUsuario)
    {
        InitializeComponent();
        _supabaseService = new SupabaseService();
        _idUsuario = idUsuario;
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();

        var response = await _supabaseService.GetClient().From<Usuario>().Where(u => u.id == _idUsuario).Get();
        _psicologo = response.Models.FirstOrDefault();

        if (!string.IsNullOrEmpty(_psicologo.imagen_perfil))
        {
            var storage = _supabaseService.GetClient().Storage;
            var bucket = storage.From("imagenes-perfil");

            ImagenPerfil.Source = bucket.GetPublicUrl(_psicologo.imagen_perfil);
        }
        Nombre.Text = _psicologo.nombre;
        Especialidad.Text = _psicologo.especialidad;
        Descripcion.Text = _psicologo.descripcion;
        Direccion.Text = _psicologo.direccion;

    }
    private async void OnAbrirChatClicked(object sender, EventArgs e)
    {
        Application.Current.MainPage = new ChatPage(_psicologo.id);
    }
    private async void OnSalirTapped(object sender, EventArgs e)
    {
        ((App)Application.Current).MostrarAppShell();
        await Shell.Current.GoToAsync("//BuscadorPage");
    }
    

}
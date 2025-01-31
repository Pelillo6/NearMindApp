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

    }

    }
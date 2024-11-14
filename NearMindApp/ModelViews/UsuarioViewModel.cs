using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Supabase;
using NearMindApp.Models;

namespace NearMindApp.ModelViews
{
    public partial class UsuarioViewModel : ObservableObject
    {
        private readonly Client _client;

        [ObservableProperty]
        private string email;

        [ObservableProperty]
        private string contraseña;

        [ObservableProperty]
        private string mensaje;

        public UsuarioViewModel(Client client)
        {
            _client = client;
        }

        [RelayCommand]
        public async Task IniciarSesionAsync()
        {
            try
            {
                // Intenta recuperar un usuario coincidente de la tabla "Usuario"
                var resultado = await _client.From<Usuario>()
                    .Where(u => u.email == email && u.password == contraseña)
                    .Get();

                if (resultado != null)
                {
                    mensaje = "Sesión iniciada correctamente";
                }
                else
                {
                    mensaje = "Credenciales incorrectas";
                }
            }
            catch (Exception ex)
            {
                mensaje = $"Error al iniciar sesión: {ex.Message}";
            }
        }
    }
}
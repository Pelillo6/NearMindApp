using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NearMindApp.Services;
using NearMindApp.Models;
using Supabase.Interfaces;
using Supabase;

namespace NearMindApp.ModelViews
{
    public partial class LoginViewModel : ObservableObject
    {

        private readonly Client _supabaseClient;

        [ObservableProperty]
        private string email;

        [ObservableProperty]
        private string password;

        [ObservableProperty]
        private string mensaje = "Esperando...";

        public ICommand IniciarSesionCommand { get; }

        public LoginViewModel()
        {
            _supabaseClient = new Client("https://ypjbezsniccydiqdhnvs.supabase.co", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6InlwamJlenNuaWNjeWRpcWRobnZzIiwicm9sZSI6ImFub24iLCJpYXQiOjE3Mjk4NzUwMjEsImV4cCI6MjA0NTQ1MTAyMX0.VJiINPZnNn9NCaCQrHwwUe51MCitZl-gT4AjI5nPhJw");
            IniciarSesionCommand = new AsyncRelayCommand(IniciarSesionAsync);
        }

        private async Task IniciarSesionAsync()
        {
            var resultado = await _supabaseClient.From<Usuario>()
            .Where(u => u.email == email && u.password == password)
            .Get();
            // Buscar usuario en la base de datos
            var usuario = resultado.Models.FirstOrDefault();

            // Verificar si el usuario existe
            if (usuario != null)
            {
                mensaje = "Sesión iniciada correctamente";
            }
            else
            {
                mensaje = "Credenciales incorrectas";
            }
        }
    }
}

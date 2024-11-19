using System;
using System.Threading.Tasks;
using Supabase;
using Supabase.Gotrue;
using Supabase.Postgrest.Models;

namespace NearMindApp.Services
{
    public class SupabaseService
    {
        private readonly Supabase.Client _client;

        public SupabaseService()
        {
            string url = "https://ypjbezsniccydiqdhnvs.supabase.co";
            string apiKey = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6InlwamJlenNuaWNjeWRpcWRobnZzIiwicm9sZSI6ImFub24iLCJpYXQiOjE3Mjk4NzUwMjEsImV4cCI6MjA0NTQ1MTAyMX0.VJiINPZnNn9NCaCQrHwwUe51MCitZl-gT4AjI5nPhJw";

            // Configuración inicial de Supabase
            _client = new Supabase.Client(url, apiKey);
        }

        public Supabase.Client GetClient() {
            return _client;
        }

        // Método de autenticación
        public async Task<User> IniciarSesion(string email, string password)
        {
            try
            {
                var session = await _client.Auth.SignIn(email, password);
                return session.User;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al iniciar sesión: {ex.Message}");
                return null;
            }
        }

        // Método para registrar un nuevo usuario
        public async Task<User> RegistrarUsuario(string email, string password)
        {
            try
            {
                var session = await _client.Auth.SignUp(email, password);
                return session.User;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al registrar usuario: {ex.Message}");
                return null;
            }
        }

        // Método genérico para obtener todos los elementos de una tabla
        public async Task<List<T>> ObtenerElementosDeTabla<T>() where T : BaseModel, new()
        {
            var response = await _client.From<T>().Get();
            return response.Models;
        }

        // Método para insertar un nuevo elemento en una tabla
        public async Task InsertarElementoEnTabla<T>(T elemento) where T : BaseModel, new()
        {
            await _client.From<T>().Insert(elemento);
        }
    }
}

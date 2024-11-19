using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NearMindApp.Models;
using Supabase;
using Supabase.Interfaces;

namespace NearMindApp.ModelViews
{
    public partial class RegistroViewModel : ObservableObject
    {
        private readonly Client _supabaseClient;

        // Propiedades de la vista
        [ObservableProperty]
        private string nombre;

        [ObservableProperty]
        private string mensaje;

        [ObservableProperty]
        private string email;

        [ObservableProperty]
        private string password;

        [ObservableProperty]
        private string telefono;

        [ObservableProperty]
        private string rol;

        [ObservableProperty]
        private List<string> roles = new List<string> { "Psicólogo", "Paciente" };

        [ObservableProperty]
        private bool isPsicologoSelected;

        [ObservableProperty]
        private List<Especialidad> especialidadesDisponibles = new List<Especialidad>
        {
            new Especialidad { Name = "Psicología Clínica" },
            new Especialidad { Name = "Psicología Infantil y Adolescente" },
            new Especialidad { Name = "Psicología Educativa" },
            new Especialidad { Name = "Psicología Organizacional" },
            new Especialidad { Name = "Psicología del Deporte" },
            new Especialidad { Name = "Psicología de la Salud" },
            new Especialidad { Name = "Psicoterapia Familiar y Pareja" },
            new Especialidad { Name = "Psicología Forense" },
            new Especialidad { Name = "Neuropsicología" },
            new Especialidad { Name = "Psicología de la Sexualidad" },
            new Especialidad { Name = "Psicología Social" },
            new Especialidad { Name = "Terapia de Adicciones" },
            new Especialidad { Name = "Psicología Transpersonal" },
            new Especialidad { Name = "Psicología Gerontológica" },
            new Especialidad { Name = "Psicología Positiva" }
        };

        [ObservableProperty]
        private List<Models.Especialidad> especialidadesSeleccionadas = new List<Models.Especialidad>();

        // Comando para registrar
        public ICommand RegistrarCommand { get; }

        public RegistroViewModel()
        {
            _supabaseClient = new Client("https://ypjbezsniccydiqdhnvs.supabase.co", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6InlwamJlenNuaWNjeWRpcWRobnZzIiwicm9sZSI6ImFub24iLCJpYXQiOjE3Mjk4NzUwMjEsImV4cCI6MjA0NTQ1MTAyMX0.VJiINPZnNn9NCaCQrHwwUe51MCitZl-gT4AjI5nPhJw");
            RegistrarCommand = new AsyncRelayCommand(RegistrarAsync);
        }

        // Método para registrar al usuario
        private async Task RegistrarAsync()
        {
            // Validar y crear el usuario según el rol
            if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                // Mostrar error si falta algún dato
                mensaje = "Los campos Nombre, Email y Contraseña son obligatorios";
                return;
            }

            // Aquí se crearía el usuario en la base de datos (Supabase) según el rol
            Usuario usuario;
            if (rol == "Psicólogo")
            {
                var psicologo = new Usuario
                {
                    nombre = nombre,
                    email = email,
                    password = password,
                    telefono = telefono,
                    rol = rol,
                    especialidades = especialidadesSeleccionadas,
                    validado = false
                };
                // Llamar a Supabase para registrar al psicólogo
                var resultadoPsicologo = await _supabaseClient.From<Usuario>().Insert(psicologo);
                if (resultadoPsicologo.Models == null || !resultadoPsicologo.Models.Any())
                {
                    mensaje = "No se pudo registrar al psicólogo.";
                }
                else
                {
                    mensaje = "Psicólogo registrado correctamente.";
                }
            }
            else if (rol == "Paciente")
            {
                // Crear paciente (hereda de Usuario)
                var paciente = new Usuario
                {
                    nombre = nombre,
                    email = email,
                    password = password,
                    telefono = telefono,
                    rol = rol
                };

                // Llamar a Supabase para registrar al paciente
                var resultadoPaciente = await _supabaseClient.From<Usuario>().Insert(paciente);
                if (resultadoPaciente.Models == null || !resultadoPaciente.Models.Any())
                {
                    mensaje = "No se pudo registrar al paciente.";
                }
                else
                {
                    mensaje = "Paciente registrado correctamente.";
                }
            }
            else
            {
                mensaje = "Rol no válido";
            }
        }

        // Propiedad calculada para mostrar u ocultar el campo de especialidad según el rol
        partial void OnRolChanged(string value)
        {
            isPsicologoSelected = value == "Psicólogo";
            OnPropertyChanged(nameof(IsPsicologoSelected));
        }
    }

    public class Especialidad
    {
        public string Name { get; set; }
        public bool IsSelected { get; set; }
    }
}
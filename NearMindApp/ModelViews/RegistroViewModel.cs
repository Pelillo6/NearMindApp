using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NearMindApp.Models;

namespace NearMindApp.ModelViews
{
    public partial class RegistroViewModel : ObservableObject
    {
        // Propiedades de la vista
        [ObservableProperty]
        private string nombre;

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
            RegistrarCommand = new AsyncRelayCommand(RegistrarAsync);
        }

        // Método para registrar al usuario
        private async Task RegistrarAsync()
        {
            // Validar y crear el usuario según el rol
            if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                // Mostrar error si falta algún dato
                return;
            }

            // Aquí se crearía el usuario en la base de datos (Supabase) según el rol
            Usuario usuario;
            if (rol == "Psicólogo")
            {
                usuario = new Psicologo
                {
                    Nombre = nombre,
                    Email = email,
                    Password = password,
                    Telefono = telefono,
                    Especialidades = especialidadesSeleccionadas // Asignar especialidades seleccionadas
                };
            }
            else
            {
                usuario = new Paciente
                {
                    Nombre = nombre,
                    Email = email,
                    Password = password,
                    Telefono = telefono
                };
            }

            // Llamada para guardar el usuario en la base de datos...
            // Ejemplo: await _usuarioService.RegistrarUsuarioAsync(usuario);

            // Mostrar un mensaje de éxito o redirigir a otra página si es necesario
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
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NearMindApp.Models;
using Supabase;
using NearMindApp.Services;
using Supabase.Interfaces;

namespace NearMindApp.ModelViews
{
    public partial class RegistroViewModel : ObservableObject
    {
        private SupabaseService _supabaseService;

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
        private string ubicacion;

        [ObservableProperty]
        private double? precio;

        [ObservableProperty]
        private List<string> roles = new List<string> { "Psicologo", "Paciente" };

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
        private List<Especialidad> especialidadesSeleccionadas = new List<Especialidad>();

        // Comando para registrar
        public ICommand RegistrarCommand { get; }

        public RegistroViewModel()
        {
            _supabaseService = new SupabaseService();
            RegistrarCommand = new AsyncRelayCommand(RegistrarAsync);
        }

        // Método para registrar al usuario
        private async Task RegistrarAsync()
        {
            if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                Mensaje = "Los campos Nombre, Email y Contraseña son obligatorios";
                return;
            }

            if (rol == "Psicologo")
            {
                if (!precio.HasValue)
                {
                    Mensaje = "El precio por hora es obligatorio para los psicólogos";
                    return;
                }

                var psicologo = new Usuario
                {
                    nombre = nombre,
                    email = email,
                    password = password,
                    telefono = telefono,
                    rol = rol,
                    ubicacion = ubicacion,
                    precio = precio,
                    validado = false
                };
                
                var resultadoPsicologo = await _supabaseService.GetClient().From<Usuario>().Insert(psicologo);
                if (resultadoPsicologo.Models?.Any() == true)
                {
                    Mensaje = "Psicólogo registrado correctamente y pendiente de validación.";
                    await Task.Delay(1500);
                    await Application.Current.MainPage.Navigation.PopAsync(); // Navega hacia atrás
                }
                else
                {
                    Mensaje = "No se pudo registrar al psicólogo.";
                }
            }
            else if (rol == "Paciente")
            {
                var paciente = new Usuario
                {
                    nombre = nombre,
                    email = email,
                    password = password,
                    telefono = telefono,
                    rol = rol,
                    ubicacion = ubicacion
                };

                var resultadoPaciente = await _supabaseService.GetClient().From<Usuario>().Insert(paciente);
                if (resultadoPaciente.Models?.Any() == true)
                {
                    Mensaje = "Paciente registrado correctamente.";
                    await Task.Delay(1500);
                    await Application.Current.MainPage.Navigation.PopAsync(); // Navega hacia atrás
                }
                else
                {
                    Mensaje = "No se pudo registrar al paciente.";
                }
            }
            else
            {
                Mensaje = "Rol no válido";
            }
        }

        // Propiedad calculada para mostrar u ocultar el campo de especialidad según el rol
        partial void OnRolChanged(string value)
        {
            isPsicologoSelected = value == "Psicologo";
            OnPropertyChanged(nameof(IsPsicologoSelected));
        }
    }

    public class Especialidad
    {
        public string Name { get; set; }
        public bool IsSelected { get; set; }
    }
    public class StringToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !string.IsNullOrEmpty(value as string);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
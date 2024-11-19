using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Supabase.Postgrest.Models;

namespace NearMindApp.Models
{
    public class Usuario : BaseModel
    {
        [JsonPropertyName("id")]
        public Guid id { get; set; }

        [Required]
        [JsonPropertyName("nombre")]
        public string nombre { get; set; }

        [Required]
        [JsonPropertyName("email")]
        public string email { get; set; }

        [JsonPropertyName("telefono")]
        public string telefono { get; set; }

        [JsonPropertyName("fecha_nacimiento")]
        public DateTime fecha_nacimiento { get; set; }

        [Required]
        [JsonPropertyName("password")]
        public string password { get; set; }

        // Historial compartido por Psicologo y Paciente
        [JsonPropertyName("historial")]
        public List<Cita> historial { get; set; }

        [JsonPropertyName("rol")]
        public string rol { get; set; }

        [JsonPropertyName("especialidades")]
        public List<Especialidad> especialidades
        {
            get
            {
                if (string.IsNullOrEmpty(especialidadesString))
                {
                    return new List<Especialidad>();
                }

                return especialidadesString.Split(',')
                    .Where(especialidad => !string.IsNullOrWhiteSpace(especialidad))
                    .Select(especialidad => Enum.TryParse<Especialidad>(especialidad, out var result) ? result : default)
                    .Where(especialidad => especialidad != default)
                    .ToList();
            }
            set
            {
                especialidadesString = value != null && value.Any()
                    ? string.Join(",", value.Select(e => e.ToString()))
                    : string.Empty;
            }
        }
        private string especialidadesString = string.Empty;

        [JsonPropertyName("validado")]
        public bool validado { get; set; }
        public Usuario()
        {
            id = Guid.NewGuid();
            historial = new List<Cita>();
            especialidades = new List<Especialidad>();
            especialidadesString = string.Empty;
        }
    }
}
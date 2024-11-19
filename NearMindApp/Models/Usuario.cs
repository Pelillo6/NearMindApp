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
        public Guid Id { get; set; }

        [Required]
        [JsonPropertyName("nombre")]
        public string Nombre { get; set; }

        [Required]
        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("telefono")]
        public string Telefono { get; set; }

        [JsonPropertyName("fecha_nacimiento")]
        public DateTime FechaNacimiento { get; set; }

        [Required]
        [JsonPropertyName("password")]
        public string Password { get; set; }

        // Historial compartido por Psicologo y Paciente
        [JsonPropertyName("historial")]
        public List<Sesion> Historial { get; set; }

        public Usuario()
        {
            Id = Guid.NewGuid();
            Historial = new List<Sesion>();
        }
    }
}
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

        [JsonPropertyName("rol")]
        public string Rol { get; set; } // Puede ser "Paciente", "Psicólogo", "Admin", etc.

        [Required]
        [JsonPropertyName("password")]
        public string Password { get; set; }

        public Usuario()
        {
            Id = Guid.NewGuid();
        }
    }
}
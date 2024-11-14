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

        [JsonPropertyName("rol")]
        public string rol { get; set; } // Puede ser "Paciente", "Psicólogo", "Admin", etc.

        [Required]
        [JsonPropertyName("password")]
        public string password { get; set; }

        public Usuario()
        {
            id = Guid.NewGuid();
        }
    }
}
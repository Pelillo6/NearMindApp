using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Supabase.Postgrest.Models;
using Supabase.Postgrest.Attributes;

namespace NearMindApp.Models
{
    public class Usuario : BaseModel
    {
        [PrimaryKey]
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

        [JsonPropertyName("historial")]
        public List<Cita> historial { get; set; }

        [JsonPropertyName("rol")]
        public string rol { get; set; }

        [JsonPropertyName("especialidad")]
        public string especialidad { get; set; }

        [JsonPropertyName("validado")]
        public bool validado { get; set; }

        [JsonPropertyName("precio")]
        public double? precio { get; set; }

        [JsonPropertyName("ubicacion")]
        public string ubicacion { get; set; }
        [JsonPropertyName("direccion")]
        public string direccion { get; set; }
        [JsonPropertyName("valoracion_media")]
        public double? valoracion_media { get; set; }
        [JsonPropertyName("descripcion")]
        public string descripcion { get; set; }
        [JsonPropertyName("imagen_perfil")]
        public string imagen_perfil { get; set; }
        public Usuario()
        {
            id = Guid.NewGuid();
            historial = new List<Cita>();
        }
        public bool isPsicologo() {
            return rol == "Psicologo";
        }
    }
}
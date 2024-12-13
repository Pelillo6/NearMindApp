using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Supabase.Postgrest.Models;
using NearMindApp.Services;
using Supabase.Postgrest.Attributes;
namespace NearMindApp.Models
{
    public class Cita : BaseModel
    {
        [PrimaryKey]
        [JsonPropertyName("id")]
        public Guid id { get; set; }

        [Required]
        [JsonPropertyName("fecha")]
        public DateTime fecha { get; set; }

        [Required]
        [JsonPropertyName("usuario1_id")]
        public Guid usuario1_id { get; set; }

        [Required]
        [JsonPropertyName("usuario2_id")]
        public Guid usuario2_id { get; set; }

        [JsonPropertyName("nota")]
        public string nota { get; set; }
        public Cita()
        {
            id = Guid.NewGuid();
        }
        
    }
}
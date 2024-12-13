using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Supabase.Postgrest.Models;
using System.Threading.Tasks;
using Supabase.Postgrest.Attributes;

namespace NearMindApp.Models
{
    public class Sesion
    {
        [PrimaryKey]
        [JsonPropertyName("id")]
        public Guid id { get; set; }
        [Required]
        [JsonPropertyName("psicologo")]
        public Guid psicologo { get; set; }

        [Required]
        [JsonPropertyName("paciente")]
        public Guid paciente { get; set; }

        [Required]
        [JsonPropertyName("fecha")]
        public DateTime fecha { get; set; }

        [Required]
        [JsonPropertyName("nota")]
        public String nota { get; set; }
    }
}

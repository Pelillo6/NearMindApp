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
    public class Cita : BaseModel
    {
        [JsonPropertyName("id")]
        public Guid id { get; set; }

        [Required]
        [JsonPropertyName("fecha")]
        public DateTime fecha { get; set; }

        [Required]
        [JsonPropertyName("psicologo_id")]
        public Guid osicologo_id { get; set; }

        [JsonPropertyName("psicologo")]
        public Psicologo psicologo { get; set; }

        [Required]
        [JsonPropertyName("paciente_id")]
        public Guid pacienteId { get; set; }

        [JsonPropertyName("paciente")]
        public Paciente paciente { get; set; }

        public Cita()
        {
            id = Guid.NewGuid();
        }
    }
}
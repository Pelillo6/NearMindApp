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
        public Guid Id { get; set; }

        [Required]
        [JsonPropertyName("fecha")]
        public DateTime Fecha { get; set; }

        [Required]
        [JsonPropertyName("psicologo_id")]
        public Guid PsicologoId { get; set; }

        [JsonPropertyName("psicologo")]
        public Psicologo Psicologo { get; set; }

        [Required]
        [JsonPropertyName("paciente_id")]
        public Guid PacienteId { get; set; }

        [JsonPropertyName("paciente")]
        public Paciente Paciente { get; set; }

        public Cita()
        {
            Id = Guid.NewGuid();
        }
    }
}
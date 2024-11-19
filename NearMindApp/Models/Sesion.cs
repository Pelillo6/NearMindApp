using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace NearMindApp.Models
{
    public class Sesion
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("fecha")]
        public DateTime Fecha { get; set; }

        [JsonPropertyName("nota")]
        public string Nota { get; set; }

        [JsonPropertyName("psicologo")]
        public Psicologo Psicologo { get; set; }

        [JsonPropertyName("paciente")]
        public Paciente Paciente { get; set; }

        public Sesion()
        {
            Id = Guid.NewGuid();
        }
    }
}
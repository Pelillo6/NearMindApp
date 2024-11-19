﻿using System;
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
        public List<Sesion> historial { get; set; }

        [JsonPropertyName("rol")]
        public string rol { get; set; }

        [JsonPropertyName("especialidades")]
        public List<Models.Especialidad> especialidades
        {
            get
            {
                return especialidadesString.Split(',')
                    .Select(especialidad => Enum.Parse<Especialidad>(especialidad))
                    .ToList();
            }
            set
            {
                especialidadesString = string.Join(",", value.Select(e => e.ToString()));
            }
        }
        private string especialidadesString;

        [JsonPropertyName("validado")]
        public bool validado { get; set; }
        public Usuario()
        {
            id = Guid.NewGuid();
            historial = new List<Sesion>();
            especialidades = new List<Especialidad>();
        }
    }
}
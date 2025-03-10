﻿using System;
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

        [JsonPropertyName("rol")]
        public string rol { get; set; }

        [JsonPropertyName("especialidad")]
        public string especialidad { get; set; }

        [JsonPropertyName("precio")]
        public double? precio { get; set; }

        [JsonPropertyName("ciudad")]
        public string ciudad { get; set; }
        [JsonPropertyName("direccion")]
        public string direccion { get; set; }
        [JsonPropertyName("descripcion")]
        public string descripcion { get; set; }
        [JsonPropertyName("imagen_perfil")]
        public string imagen_perfil { get; set; }
        public Usuario()
        {
            id = Guid.NewGuid();
        }
        public bool isPsicologo() {
            return rol == "Psicologo";
        }
    }
}
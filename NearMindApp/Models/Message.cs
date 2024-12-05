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
    public class Message : BaseModel
    {
        [JsonPropertyName("id")]
        public Guid id { get; set; }
        [JsonPropertyName("emisor_id")]
        public Guid emisor_id { get; set; }
        [JsonPropertyName("receptor_id")]
        public Guid receptor_id { get; set; }
        [JsonPropertyName("texto")]
        public String texto { get; set; }
        [JsonPropertyName("fecha_hora")]
        public DateTime fecha_hora { get; set; }
    }
}

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
        public Guid id { get; set; }
      
        public Guid emisorId { get; set; }
        public Guid receptorId { get; set; }
        public String texto { get; set; }
        public DateTime fechaHora { get; set; }
        public String NombreEmisor { get; set; }
        public bool EsEnviado { get; set; }

    }
}

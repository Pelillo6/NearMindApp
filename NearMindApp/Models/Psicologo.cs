using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace NearMindApp.Models
{
    public class Psicologo : Usuario
    {
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

        [JsonPropertyName("validado")]
        public bool validado { get; set; }


        private string especialidadesString;

        public Psicologo()
        {
            especialidades = new List<Especialidad>();
        }
    }
}
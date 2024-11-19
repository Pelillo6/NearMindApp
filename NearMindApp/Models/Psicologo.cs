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
        public Psicologo()
        {
            especialidades = new List<Especialidad>();
        }
    }
}
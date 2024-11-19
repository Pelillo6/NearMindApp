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
        public enum EspecialidadEnum
        {
            PsicologiaClinica,
            PsicologiaInfantilYAdolescente,
            PsicologiaEducativa,
            PsicologiaOrganizacional,
            PsicologiaDelDeporte,
            PsicologiaDeLaSalud,
            PsicoterapiaFamiliarYPareja,
            PsicologiaForense,
            Neuropsicologia,
            PsicologiaDeLaSexualidad,
            PsicologiaSocial,
            TerapiaDeAdicciones,
            PsicologiaTranspersonal,
            PsicologiaGerontologica,
            PsicologiaPositiva
        }

        [JsonPropertyName("especialidad")]
        public EspecialidadEnum Especialidad { get; set; }
    }
}
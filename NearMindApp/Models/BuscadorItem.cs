using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NearMindApp.Models
{
    internal class BuscadorItem
    {
        public Guid UsuarioId { get; set; }
        public string NombreUsuario { get; set; }
        public string ImagenPerfil { get; set; }
        public string Ciudad { get; set; }
        public string Especialidad { get; set; }
        public string PrecioHora { get; set; }
    }
}

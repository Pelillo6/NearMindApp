using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NearMindApp.Models
{
    public class ChatItem
    {
        public Guid UsuarioId { get; set; }
        public string NombreUsuario { get; set; }
        public string ImagenPerfil { get; set; }
        public string UltimoMensaje { get; set; }
        public DateTime FechaUltimoMensaje { get; set; }
    }
}

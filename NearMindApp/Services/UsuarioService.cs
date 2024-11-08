using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NearMindApp.Models;

namespace NearMindApp.Services
{
    public class UsuarioService
    {
        // Simulación de un usuario autenticado (esto debe ser reemplazado por la lógica real de autenticación)
        private static Usuario _usuarioActual;

        public UsuarioService()
        {
            // Crear un usuario de ejemplo para probar
            _usuarioActual = new Usuario
            {
                Id = Guid.NewGuid(),  // Genera un GUID único para el usuario
                Nombre = "Juan Pérez",
                Email = "juanperez@ejemplo.com",
                Telefono = "123456789",
                FechaNacimiento = new DateTime(1990, 1, 1),
                Rol = "Paciente"
            };
        }

        // Método para obtener el GUID del usuario actual
        public Guid ObtenerUsuarioActualId()
        {
            if (_usuarioActual != null)
            {
                return _usuarioActual.Id;
            }

            // Si no hay un usuario autenticado, puedes lanzar una excepción o devolver un GUID vacío.
            throw new InvalidOperationException("No se ha autenticado un usuario.");
        }
    }
}

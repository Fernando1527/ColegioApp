using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ColegioApp.Models
{
    public class Alumno
    {
        public int Id { get; set; }

        [Required]
        public string? Nombre { get; set; }

        public string? Apellido { get; set; }

        public ICollection<Expediente> Expedientes { get; set; }
    }
}
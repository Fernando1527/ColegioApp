using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ColegioApp.Models
{
    public class Materia
    {
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        public ICollection<Expediente> Expedientes { get; set; }
    }
}
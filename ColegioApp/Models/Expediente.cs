namespace ColegioApp.Models
{
    public class Expediente
    {
        public int Id { get; set; }

        public int AlumnoId { get; set; }
        public Alumno Alumno { get; set; }

        public int MateriaId { get; set; }
        public Materia Materia { get; set; }

        public double Nota { get; set; }

        public string Observacion { get; set; }
    }
}
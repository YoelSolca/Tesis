namespace Backend.Domain.Entities
{
    public class Persona
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public string Telefono { get; set; }

        public string Documento { get; set; }

        public string Genero { get; set; }

        public DateOnly FechaNacimiento { get; set; }
    }
}

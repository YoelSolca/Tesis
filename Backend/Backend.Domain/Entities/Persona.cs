namespace Backend.Domain.Entities
{
    public class Persona
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Documento { get; set; } = string.Empty;
        public string? Genero { get; set; }
        public DateOnly? FechaNacimiento { get; set; }
        public Psicopedagogo Psicopedagogo { get; set; } = null!;
        public Paciente Paciente { get; set; } = null!;
        public Administrador Administrador { get; set; } = null!;
        public Usuario Usuario { get; set; } = null!;

    }
}

namespace Backend.Domain.Entities
{
    public class Intervencion
    {
        public int Id { get; set; }
        public string? objetivo { get; set; }  
        public string? observaciones { get; set; }
        public ICollection<Paciente> Pacientes { get; set; } = new List<Paciente>();
    }
}

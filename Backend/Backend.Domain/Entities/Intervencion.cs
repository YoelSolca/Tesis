namespace Backend.Domain.Entities
{
    public class Intervencion
    {
        public int Id { get; set; }
        public string? objetivo { get; set; }
        public string? observaciones { get; set; }
        public ICollection<IntervencionTipoDificultad> TiposDificultad { get; set; } = new List<IntervencionTipoDificultad>();
        public PsicopedagogoPaciente? PsicopedagogoPaciente { get; set; }
    }
}

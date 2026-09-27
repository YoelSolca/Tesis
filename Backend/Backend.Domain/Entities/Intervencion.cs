namespace Backend.Domain.Entities
{
    public class Intervencion
    {
        public int Id { get; set; }
        public string? objetivo { get; set; }
        public string? observaciones { get; set; }

        /// <summary>La intervención es privada de un único vínculo psicopedagogo-paciente.</summary>
        public PsicopedagogoPaciente? PsicopedagogoPaciente { get; set; }
    }
}

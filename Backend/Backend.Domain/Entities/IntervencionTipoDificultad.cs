namespace Backend.Domain.Entities
{
    /// <summary>Dificultad trabajada en una intervención. Relación muchos a muchos entre Intervencion y TipoDificultad.</summary>
    public class IntervencionTipoDificultad
    {
        public int IntervencionId { get; set; }

        public Intervencion Intervencion { get; set; } = null!;

        public int TipoDificultadId { get; set; }

        public TipoDificultad TipoDificultad { get; set; } = null!;
    }
}

namespace Backend.Domain.Entities
{
    public class IntervencionTipoDificultad
    {
        public int IntervencionId { get; set; }
        public Intervencion Intervencion { get; set; } = null!;
        public int TipoDificultadId { get; set; }
        public TipoDificultad TipoDificultad { get; set; } = null!;
    }
}

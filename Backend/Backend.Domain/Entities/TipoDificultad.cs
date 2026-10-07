namespace Backend.Domain.Entities
{
    /// <summary>Catálogo de tipos de dificultad que un psicopedagogo trabaja en una intervención (ej. "Dificultades de atención").</summary>
    public class TipoDificultad
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public ICollection<IntervencionTipoDificultad> IntervencionTiposDificultad { get; set; } = new List<IntervencionTipoDificultad>();
    }
}

namespace Backend.Domain.Entities
{
    public class TipoDificultad
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public ICollection<IntervencionTipoDificultad> IntervencionTiposDificultad { get; set; } = new List<IntervencionTipoDificultad>();
    }
}

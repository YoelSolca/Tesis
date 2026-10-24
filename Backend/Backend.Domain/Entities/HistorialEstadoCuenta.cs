namespace Backend.Domain.Entities
{
    public class HistorialEstadoCuenta
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; } = null!;
        public int AdministradorId { get; set; }
        public Administrador Administrador { get; set; } = null!;
        public EstadoCuenta EstadoAnterior { get; set; }
        public EstadoCuenta EstadoNuevo { get; set; }
        public string? Motivo { get; set; }
        public DateTime Fecha { get; set; }
    }
}

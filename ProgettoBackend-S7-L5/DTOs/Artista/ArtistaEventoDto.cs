namespace ProgettoBackend_S7_L5.DTOs.Artista
{
    public class ArtistaEventoDto
    {
        public int ArtistaId { get; set; }
        public required string Nome { get; set; }
        public required string Genere { get; set; }
        public required string Biografia { get; set; }
    }
}

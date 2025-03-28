using ProgettoBackend_S7_L5.DTOs.Evento;

namespace ProgettoBackend_S7_L5.DTOs.Artista
{
    public class ArtistaDto
    {
        public int ArtistaId { get; set; }
        public required string Nome { get; set; }
        public required string Genere { get; set; }
        public required string Biografia { get; set; }
        public List<EventoArtistaDto>? Eventi { get; set; }
    }
}

using ProgettoBackend_S7_L5.DTOs.Evento;

namespace ProgettoBackend_S7_L5.DTOs.Artista
{
    public class CreateArtistaRequestDto
    {
        public string Nome { get; set; }
        public string Genere { get; set; }
        public string Biografia { get; set; }
    }
}

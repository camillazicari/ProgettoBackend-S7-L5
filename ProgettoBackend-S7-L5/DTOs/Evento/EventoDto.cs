using ProgettoBackend_S7_L5.DTOs.Artista;
using ProgettoBackend_S7_L5.DTOs.Biglietto;

namespace ProgettoBackend_S7_L5.DTOs.Evento
{
    public class EventoDto
    {
        public int EventoId { get; set; }
        public string Titolo { get; set; }
        public DateTime Data { get; set; }
        public string Luogo { get; set; }
        public ArtistaEventoDto Artista { get; set; }
    }
}

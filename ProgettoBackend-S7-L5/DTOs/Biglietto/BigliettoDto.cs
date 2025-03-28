using ProgettoBackend_S7_L5.DTOs.Evento;

namespace ProgettoBackend_S7_L5.DTOs.Biglietto
{
    public class BigliettoDto
    {
        public int BigliettoId { get; set; }
        public DateTime DataAcquisto { get; set; }
        public required EventoDto Evento { get; set; }
        public string UserId { get; set; }
    }
}

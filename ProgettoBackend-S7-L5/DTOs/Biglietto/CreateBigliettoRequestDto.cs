using ProgettoBackend_S7_L5.DTOs.Evento;

namespace ProgettoBackend_S7_L5.DTOs.Biglietto
{
    public class CreateBigliettoRequestDto
    {
        public required int EventoId { get; set; }

        public int Quantita { get; set; }
    }
}

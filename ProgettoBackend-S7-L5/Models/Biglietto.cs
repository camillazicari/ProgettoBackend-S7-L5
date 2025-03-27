using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProgettoBackend_S7_L5.Models.Auth;

namespace ProgettoBackend_S7_L5.Models
{
    public class Biglietto
    {
        [Key]
        public int BigliettoId { get; set; }

        [Required]
        public int EventoId { get; set; }
        [ForeignKey("EventoId")]

        public Evento Evento { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]

        public ApplicationUser User { get; set; }

        public DateTime DataAcquisto { get; set; }
    }
}

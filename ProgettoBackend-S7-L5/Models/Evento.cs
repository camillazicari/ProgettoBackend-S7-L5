﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProgettoBackend_S7_L5.Models
{
    public class Evento
    {
        [Key]
        public int EventoId { get; set; }

        [Required]
        public required string Titolo { get; set; }

        [Required]
        public DateTime Data { get; set; }

        [Required]
        public required string Luogo { get; set; }

        public int ArtistaId { get; set; }
        [ForeignKey("ArtistaId")]

        public Artista Artista { get; set; }

        public ICollection<Biglietto> Biglietti { get; set; }
    }
}

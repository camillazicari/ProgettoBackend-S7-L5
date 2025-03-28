using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProgettoBackend_S7_L5.Data;
using ProgettoBackend_S7_L5.DTOs.Artista;
using ProgettoBackend_S7_L5.DTOs.Evento;
using ProgettoBackend_S7_L5.Models;
using ProgettoBackend_S7_L5.Services;

namespace ProgettoBackend_S7_L5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistiController : ControllerBase
    {
        private readonly ArtistiService _artistiService;
        private readonly ILogger<ArtistiController> _logger;
        private readonly ApplicationDbContext _context;

        public ArtistiController(ArtistiService artistiService, ILogger<ArtistiController> logger, ApplicationDbContext context)
        {
            _artistiService = artistiService;
            _logger = logger;
            _context = context;
        }

        [HttpPost]
        [Authorize(Roles = "Amministratore")]
        public async Task<IActionResult> Create([FromBody] CreateArtistaRequestDto createArtistaRequestDto)
        {
            try
            {
                var artista = new Artista()
                {
                    Nome = createArtistaRequestDto.Nome,
                    Genere = createArtistaRequestDto.Genere,
                    Biografia = createArtistaRequestDto.Biografia,
                };

                var result = await _artistiService.CreateArtistaAsync(artista);

                return result ? Ok(new ArtistaResponseDto() { Message = "Artista creato!" }) : BadRequest(new ArtistaResponseDto() { Message = "Qualcosa è andato storto!" });

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Authorize(Roles = "Amministratore")]
        public async Task<IActionResult> GetArtisti()
        {
            try
            {
                var result = await _artistiService.GetArtisti();

                if (result == null)
                {
                    return BadRequest(new { message = "Qualcosa è andato storto." });
                }

                List<ArtistaDto> responseDto = result.Select(a => new ArtistaDto()
                {
                    ArtistaId = a.ArtistaId,
                    Nome = a.Nome,
                    Genere = a.Genere,
                    Biografia = a.Biografia,
                    Eventi = a.Eventi?.Select(e => new EventoArtistaDto()
                    {
                        EventoId = e.EventoId,
                        Titolo = e.Titolo,
                        Luogo = e.Luogo,
                        Data = e.Data
                    }).ToList()
                }).ToList();

                _logger.LogInformation($"Artisti info: {JsonSerializer.Serialize(responseDto, new JsonSerializerOptions() { WriteIndented = true })}");

                return Ok(new { message = "Artisti trovati!", artista = responseDto });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpPut]
        [Authorize(Roles = "Amministratore")]
        public async Task<IActionResult> Update([FromQuery] int id, [FromBody] CreateArtistaRequestDto artista)
        {
            try
            {
                var result = await _artistiService.UpdateArtista(id, artista);
                return result ? Ok(new ArtistaResponseDto { Message = "Artista aggiornato" }) : BadRequest(new ArtistaResponseDto { Message = "Qualcosa è andato storto" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete]
        [Authorize(Roles = "Amministratore")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _artistiService.DeleteArtist(id);

                return result ? Ok(new ArtistaResponseDto { Message = "Artista cancellato!" }) : BadRequest(new ArtistaResponseDto { Message = "Qualcosa è andato storto." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpGet("{id:int}")]
        [Authorize(Roles = "Amministratore")]
        public async Task<IActionResult> GetArtistaById(int id)
        {
            try
            {
                var result = await _artistiService.GetArtistaById(id);

                if (result == null)
                {
                    return BadRequest(new { message = "Qualcosa è andato storto." });
                }

                var artistaDto = new ArtistaDto()
                {
                    ArtistaId = result.ArtistaId,
                    Nome = result.Nome,
                    Genere = result.Genere,
                    Biografia = result.Biografia,
                    Eventi = result.Eventi?.Select(e => new EventoArtistaDto()
                    {
                        EventoId = e.EventoId,
                        Titolo = e.Titolo,
                        Luogo = e.Luogo,
                        Data = e.Data
                    }).ToList()
                };

                return Ok(new { message = "Artisti trovati!", artista = artistaDto });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);

            }
        }
    }
}


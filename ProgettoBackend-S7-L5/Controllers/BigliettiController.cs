using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProgettoBackend_S7_L5.Data;
using ProgettoBackend_S7_L5.DTOs.Artista;
using ProgettoBackend_S7_L5.DTOs.Biglietto;
using ProgettoBackend_S7_L5.DTOs.Evento;
using ProgettoBackend_S7_L5.Models;
using ProgettoBackend_S7_L5.Services;

namespace ProgettoBackend_S7_L5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BigliettiController : ControllerBase
    {
        private readonly BigliettiService _bigliettiService;
        private readonly ILogger<BigliettiController> _logger;
        private readonly ApplicationDbContext _context;

        public BigliettiController(BigliettiService bigliettiService, ILogger<BigliettiController> logger, ApplicationDbContext context)
        {
            _bigliettiService = bigliettiService;
            _logger = logger;
            _context = context;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateBigliettoRequestDto createBigliettoRequestDto)
        {

            try
            {
                var user = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);
                var email = user!.Value;
                var utente = _context.ApplicationUsers.FirstOrDefault(u => u.Email == email);

                for (var i = 0; i < createBigliettoRequestDto.Quantita; i++)
                {
                    var biglietto = new Biglietto()
                    {
                        EventoId = createBigliettoRequestDto.EventoId,
                        UserId = utente!.Id,
                    };
                    var result = await _bigliettiService.CreateBigliettoAsync(biglietto);
                    if (!result)
                    {
                        return BadRequest(new BigliettoResponseDto() { Message = "Qualcosa è andato storto!" });
                    }
                }

                return Ok(new BigliettoResponseDto() { Message = "Biglietto creato!" });

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Authorize(Roles = "Amministratore")]
        public async Task<IActionResult> GetBiglietti()
        {
            try
            {
                var result = await _bigliettiService.GetBiglietti();
                if (result == null)
                {
                    return BadRequest(new { message = "Qualcosa è andato storto." });
                }

                List<BigliettoDto> responseDto = result.Select(a => new BigliettoDto()
                {
                    BigliettoId = a.BigliettoId,
                    DataAcquisto = a.DataAcquisto,
                    UserId = a.UserId,
                    Evento = new EventoDto()
                    {
                        EventoId = a.EventoId,
                        Titolo = a.Evento.Titolo,
                        Luogo = a.Evento.Luogo,
                        Data = a.Evento.Data,
                        Artista = new ArtistaEventoDto()
                        {
                            ArtistaId = a.Evento.Artista.ArtistaId,
                            Nome = a.Evento.Artista.Nome,
                            Genere = a.Evento.Artista.Genere,
                            Biografia = a.Evento.Artista.Biografia
                        }
                    }
                }).ToList();

                _logger.LogInformation($"Biglietti info: {JsonSerializer.Serialize(responseDto, new JsonSerializerOptions() { WriteIndented = true })}");

                return Ok(new { message = "Biglietti trovati!", biglietti = responseDto });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpGet("mieiBiglietti")]
        [Authorize]
        public async Task<IActionResult> GetBigliettiUser()
        {
            try
            {
                var user = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);
                var email = user!.Value;
                var result = await _bigliettiService.GetBigliettiUser(email);

                if (result == null)
                {
                    return BadRequest(new { message = "Qualcosa è andato storto." });
                }

                List<BigliettoDto> responseDto = result.Select(a => new BigliettoDto()
                {
                    BigliettoId = a.BigliettoId,
                    DataAcquisto = a.DataAcquisto,
                    UserId = a.UserId,
                    Evento = new EventoDto()
                    {
                        EventoId = a.EventoId,
                        Titolo = a.Evento.Titolo,
                        Luogo = a.Evento.Luogo,
                        Data = a.Evento.Data,
                        Artista = new ArtistaEventoDto()
                        {
                            ArtistaId = a.Evento.Artista.ArtistaId,
                            Nome = a.Evento.Artista.Nome,
                            Genere = a.Evento.Artista.Genere,
                            Biografia = a.Evento.Artista.Biografia
                        }
                    }
                }).ToList();

                _logger.LogInformation($"Biglietti info: {JsonSerializer.Serialize(responseDto, new JsonSerializerOptions() { WriteIndented = true })}");

                return Ok(new { message = "Biglietti trovati!", biglietti = responseDto });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            try
            {
                var user = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);
                var email = user.Value;

                var result = await _bigliettiService.DeleteBigliettoById(id, email);

                if (result == false)
                {
                    return BadRequest(new BigliettoResponseDto { Message = "Qualcosa è andato storto." });
                }

                return Ok(new BigliettoResponseDto { Message = "Biglietto cancellato!" });

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<IActionResult> GetBigliettoById(int id)
        {
            try
            {
                var user = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);
                var email = user.Value;
                var result = await _bigliettiService.GetBigliettoById(id, email);

                if (result == null)
                {
                    return BadRequest(new { message = "Qualcosa è andato storto." });
                }

                var bigliettoDto = new BigliettoDto()
                {
                    BigliettoId = result.BigliettoId,
                    DataAcquisto = result.DataAcquisto,
                    UserId = result.UserId,
                    Evento = new EventoDto()
                    {
                        EventoId = result.EventoId,
                        Titolo = result.Evento.Titolo,
                        Luogo = result.Evento.Luogo,
                        Data = result.Evento.Data,
                        Artista = new ArtistaEventoDto()
                        {
                            ArtistaId = result.Evento.Artista.ArtistaId,
                            Nome = result.Evento.Artista.Nome,
                            Genere = result.Evento.Artista.Genere,
                            Biografia = result.Evento.Artista.Biografia
                        }
                    }
                };

                return Ok(new { message = "Biglietto trovato!", biglietto = bigliettoDto });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);

            }
        }
    }
}
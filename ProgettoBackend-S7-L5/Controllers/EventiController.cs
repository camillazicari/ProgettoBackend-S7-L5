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
    public class EventiController : ControllerBase
    {
        private readonly EventiService _eventiService;
        private readonly ILogger<EventiController> _logger;
        private readonly ApplicationDbContext _context;

        public EventiController(EventiService eventiService, ILogger<EventiController> logger, ApplicationDbContext context)
        {
            _eventiService = eventiService;
            _logger = logger;
            _context = context;
        }

        [HttpPost]
        [Authorize(Roles = "Amministratore")]
        public async Task<IActionResult> Create([FromBody] CreateEventoRequestDto createEventoRequestDto)
        {
            try
            {
                var evento = new Evento()
                {
                    Titolo = createEventoRequestDto.Titolo,
                    Data = createEventoRequestDto.Data,
                    Luogo = createEventoRequestDto.Luogo,
                    ArtistaId = createEventoRequestDto.ArtistaId
                };

                var result = await _eventiService.CreateEventoAsync(evento);

                return result ? Ok(new EventoResponseDto() { Message = "Evento creato!" }) : BadRequest(new EventoResponseDto() { Message = "Qualcosa è andato storto!" });

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetEventi()
        {
            try
            {
                var result = await _eventiService.GetEventi();
                if (result == null)
                {
                    return BadRequest(new { message = "Qualcosa è andato storto." });
                }

                List<EventoDto> responseDto = result.Select(e => new EventoDto()
                {
                    EventoId = e.EventoId,
                    Luogo = e.Luogo,
                    Titolo = e.Titolo,
                    Data = e.Data,
                    Artista = new ArtistaEventoDto()
                    {
                        ArtistaId = e.Artista.ArtistaId,
                        Nome = e.Artista.Nome,
                        Genere = e.Artista.Genere,
                        Biografia = e.Artista.Biografia
                    }
                }).ToList();

                _logger.LogInformation($"Eventi info: {JsonSerializer.Serialize(responseDto, new JsonSerializerOptions() { WriteIndented = true })}");

                return Ok(new { message = "Eventi trovati!", eventi = responseDto });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }


        }

        [HttpPut]
        [Authorize(Roles = "Amministratore")]
        public async Task<IActionResult> Update([FromQuery] int id, [FromBody] CreateEventoRequestDto evento)
        {
            try
            {
                var result = await _eventiService.UpdateEvento(id, evento);
                return result ? Ok(new EventoResponseDto { Message = "Evento aggiornato" }) : BadRequest(new EventoResponseDto { Message = "Qualcosa è andato storto" });
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
                var result = await _eventiService.DeleteEvento(id);

                return result ? Ok(new EventoResponseDto { Message = "Evento cancellato!" }) : BadRequest(new EventoResponseDto { Message = "Qualcosa è andato storto." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpGet("{id:int}")]
        [Authorize(Roles = "Amministratore")]
        public async Task<IActionResult> GetEventoById(int id)
        {
            try
            {
                var result = await _eventiService.GetEventoById(id);

                if (result == null)
                {
                    return BadRequest(new { message = "Qualcosa è andato storto." });
                }

                var eventoDto = new EventoDto()
                {
                    EventoId = result.EventoId,
                    Luogo = result.Luogo,
                    Data = result.Data,
                    Titolo = result.Titolo,
                    Artista = new ArtistaEventoDto()
                    {
                        ArtistaId = result.ArtistaId,
                        Nome = result.Artista.Nome,
                        Biografia = result.Artista.Biografia,
                        Genere = result.Artista.Genere
                    }
                };

                return Ok(new { message = "Artisti trovati!", evento = eventoDto });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);

            }
        }
    }
}
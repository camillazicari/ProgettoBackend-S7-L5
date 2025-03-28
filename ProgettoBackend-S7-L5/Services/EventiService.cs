using Microsoft.EntityFrameworkCore;
using ProgettoBackend_S7_L5.Data;
using ProgettoBackend_S7_L5.DTOs.Artista;
using ProgettoBackend_S7_L5.DTOs.Evento;
using ProgettoBackend_S7_L5.Models;

namespace ProgettoBackend_S7_L5.Services
{
    public class EventiService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<EventiService> _logger;

        public EventiService(ApplicationDbContext context, ILogger<EventiService> logger)
        {
            _context = context;
            _logger = logger;
        }

        private async Task<bool> SaveAsync()
        {
            try
            {
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return false;
            }
        }

        public async Task<bool> CreateEventoAsync(Evento evento)
        {
            try
            {
                _context.Eventi.Add(evento);
                return await SaveAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return false;
            }
        }

        public async Task<List<Evento>> GetEventi()
        {
            try
            {
                var eventi = await _context.Eventi.Include(a => a.Artista).ToListAsync();
                return eventi;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }

        public async Task<Evento> GetEventoById(int id)
        {
            try
            {
                var eventoEsistente = await _context.Eventi.Include(e => e.Artista).FirstOrDefaultAsync(a => a.EventoId == id);

                return eventoEsistente;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }

        public async Task<bool> UpdateEvento(int id, CreateEventoRequestDto evento)
        {
            try
            {
                var eventoEsistente = await GetEventoById(id);

                if (eventoEsistente == null)
                {
                    return false;
                }

                eventoEsistente.Titolo = evento.Titolo;
                eventoEsistente.Luogo = evento.Luogo;
                eventoEsistente.Data = evento.Data;
                eventoEsistente.ArtistaId = evento.ArtistaId;

                return await SaveAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteEvento(int id)
        {
            try
            {
                var eventoEsistente = await GetEventoById(id);


                if (eventoEsistente == null)
                {
                    return false;
                }

                _context.Eventi.Remove(eventoEsistente);

                return await SaveAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return false;
            }
        }
    }
}

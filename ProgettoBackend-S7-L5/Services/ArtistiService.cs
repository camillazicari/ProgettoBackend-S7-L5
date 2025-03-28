using Microsoft.EntityFrameworkCore;
using ProgettoBackend_S7_L5.Controllers;
using ProgettoBackend_S7_L5.Data;
using ProgettoBackend_S7_L5.DTOs.Artista;
using ProgettoBackend_S7_L5.Models;

namespace ProgettoBackend_S7_L5.Services
{
    public class ArtistiService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ArtistiService> _logger;

        public ArtistiService(ApplicationDbContext context, ILogger<ArtistiService> logger)
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

        public async Task<bool> CreateArtistaAsync(Artista artista)
        {
            try
            {
                _context.Artisti.Add(artista);
                return await SaveAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return false;
            }
        }

        public async Task<List<Artista>> GetArtisti()
        {
            try
            {
                var artisti = await _context.Artisti.Include(e => e.Eventi).ToListAsync();
                return artisti;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null!;
            }
        }

        public async Task<Artista> GetArtistaById(int id)
        {
            try
            {
                var artistaEsistente = await _context.Artisti.FirstOrDefaultAsync(a => a.ArtistaId == id);

                return artistaEsistente!;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null!;
            }
        }

        public async Task<bool> UpdateArtista(int id, CreateArtistaRequestDto artista)
        {
            try
            {
                var artistaEsistente = await GetArtistaById(id);

                if (artistaEsistente == null)
                {
                    return false;
                }

                artistaEsistente.Nome = artista.Nome;
                artistaEsistente.Genere = artista.Genere;
                artistaEsistente.Biografia = artista.Biografia;

                return await SaveAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteArtist(int id)
        {
            try
            {
                var artistaEsistente = await GetArtistaById(id);


                if (artistaEsistente == null)
                {
                    return false;
                }

                _context.Artisti.Remove(artistaEsistente);

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

using Microsoft.EntityFrameworkCore;
using ProgettoBackend_S7_L5.Data;
using ProgettoBackend_S7_L5.DTOs.Artista;
using ProgettoBackend_S7_L5.DTOs.Biglietto;
using ProgettoBackend_S7_L5.Models;

namespace ProgettoBackend_S7_L5.Services
{
    public class BigliettiService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<BigliettiService> _logger;

        public BigliettiService(ApplicationDbContext context, ILogger<BigliettiService> logger)
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

        public async Task<bool> CreateBigliettoAsync(Biglietto biglietto)
        {
            try
            {
                _context.Biglietti.Add(biglietto);
                return await SaveAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return false;
            }
        }

        public async Task<List<Biglietto>> GetBiglietti()
        {
            try
            {
                var biglietto = await _context.Biglietti.Include(e => e.Evento).ThenInclude(e => e.Artista).ToListAsync();
                return biglietto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null!;

            }
        }

        public async Task<List<Biglietto>> GetBigliettiUser(string email)
        {
            try
            {
                var user = _context.ApplicationUsers.FirstOrDefault(e => e.Email == email);
                var biglietto = await _context.Biglietti.Where(b => b.UserId == user!.Id).Include(e => e.Evento).ThenInclude(e => e.Artista).ToListAsync();
                return biglietto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null!;

            }
        }

        public async Task<Biglietto> GetBigliettoById(int id, string email)
        {
            try
            {
                var user = _context.ApplicationUsers.FirstOrDefault(e => e.Email == email);
                var bigliettoEsistente = await _context.Biglietti.Include(b => b.Evento).ThenInclude(a => a.Artista).Where(b => b.UserId == user.Id).FirstOrDefaultAsync(a => a.BigliettoId == id);


                return bigliettoEsistente!;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null!;
            }
        }

        public async Task<bool> DeleteBigliettoById(int id, string email)
        {
            try
            {
                var user = _context.ApplicationUsers.FirstOrDefault(e => e.Email == email);
                var bigliettoEsistente = await GetBigliettoById(id, email);


                if (bigliettoEsistente == null || bigliettoEsistente.UserId != user.Id)
                {
                    return false;
                }

                _context.Biglietti.Remove(bigliettoEsistente);

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

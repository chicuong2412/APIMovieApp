using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class GenereRepository
    {
        private readonly AppDbContext _context;
        public GenereRepository(AppDbContext context)
        {
            _context = context;
        }


        public async Task<List<Genere>> GetAllGenenres()
        {
            return await _context.Generes.Where(genere => genere.IsDeleted != true).ToListAsync();
        }

    }
}

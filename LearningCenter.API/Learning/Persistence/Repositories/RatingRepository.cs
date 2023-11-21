using LearningCenter.API.Learning.Domain.Models;
using LearningCenter.API.Learning.Domain.Repositories;
using LearningCenter.API.Shared.Persistence.Contexts;
using LearningCenter.API.Shared.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LearningCenter.API.Learning.Persistence.Repositories;

public class RatingRepository : BaseRepository, IRatingRepository
{
    public RatingRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Rating>> ListAsync()
    {
        return await _context.Ratings.ToListAsync();
    }

    public async Task AddAsync(Rating Rating)
    {
        await _context.Ratings.AddAsync(Rating);
    }

    public async Task<Rating> FindByIdAsync(int id)
    {
        return await _context.Ratings.FindAsync(id);
    }

    public async Task<IEnumerable<Rating>> FindByStoreIdAsync(int StoreId)
    {
        return await _context.Ratings
            .Where(p => p.StoreId == StoreId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Rating>> FindByStoreIdAndUserAsync(int StoreId, int UserId)
    {
        return await _context.Ratings
            .Where(r => r.StoreId == StoreId &&  r.UserId == UserId)
            .ToListAsync();
    }

    public void Update(Rating Rating)
    {
        _context.Ratings.Update(Rating);
    }

    public void Remove(Rating Rating)
    {
        _context.Ratings.Remove(Rating);
    }
}
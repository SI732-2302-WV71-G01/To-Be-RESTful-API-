using LearningCenter.API.Learning.Domain.Models;

namespace LearningCenter.API.Learning.Domain.Repositories;

public interface IRatingRepository
{
    Task<IEnumerable<Rating>> ListAsync();
    Task AddAsync(Rating Rating);
    Task<Rating> FindByIdAsync(int id);
    Task<IEnumerable<Rating>> FindByStoreIdAsync(int StoreId);
    Task<IEnumerable<Rating>> FindByStoreIdAndUserAsync(int StoreId, int UserId);
    void Update(Rating Rating);
    void Remove(Rating Rating);
}
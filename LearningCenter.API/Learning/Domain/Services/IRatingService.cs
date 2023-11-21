using LearningCenter.API.Learning.Domain.Models;
using LearningCenter.API.Learning.Domain.Services.Communication;

namespace LearningCenter.API.Learning.Domain.Services;

public interface IRatingService
{
    Task<IEnumerable<Rating>> ListAsync();
    Task<Rating> GetByIdAsync(int id);
    Task<IEnumerable<Rating>> ListByStoreIdAsync(int StoreId);
    Task<IEnumerable<Rating>> ListByStoreIdAndUserIdAsync(int StoreId, int UserId);
    Task<RatingResponse> SaveAsync(Rating Rating);
    Task<RatingResponse> UpdateAsync(int RatingId, Rating Rating);
    Task<RatingResponse> DeleteAsync(int RatingId);
}
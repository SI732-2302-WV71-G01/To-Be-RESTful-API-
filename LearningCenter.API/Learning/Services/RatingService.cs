using LearningCenter.API.Learning.Domain.Models;
using LearningCenter.API.Learning.Domain.Repositories;
using LearningCenter.API.Learning.Domain.Services;
using LearningCenter.API.Learning.Domain.Services.Communication;
using LearningCenter.API.Security.Domain.Repositories;
using LearningCenter.API.Security.Exceptions;

public class RatingService : IRatingService
{
    private readonly IRatingRepository _RatingRepository;
    private readonly IStoreRepository _StoreRepository;
    private readonly IUserRepository _UserRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RatingService(IRatingRepository RatingRepository, IUnitOfWork unitOfWork,
        IStoreRepository StoreRepository, IUserRepository UserRepository)
    {
        _RatingRepository = RatingRepository;
        _unitOfWork = unitOfWork;
        _StoreRepository = StoreRepository;
        _UserRepository = UserRepository;
    }

    public async Task<IEnumerable<Rating>> ListAsync()
    {
        return await _RatingRepository.ListAsync();
    }

    public async Task<Rating> GetByIdAsync(int id)
    {
        var Rating = await _RatingRepository.FindByIdAsync(id);
        if (Rating == null) throw new KeyNotFoundException("Rating not found");
        return Rating;
    }

    public async Task<IEnumerable<Rating>> ListByStoreIdAsync(int StoreId)
    {
        var existingStore = await _StoreRepository.FindByIdAsync(StoreId);

        if (existingStore == null)
            throw new AppException("Invalid Store");
        
        return await _RatingRepository.FindByStoreIdAsync(StoreId);
    }

    public async Task<IEnumerable<Rating>> ListByStoreIdAndUserIdAsync(int StoreId, int UserId)
    {
        var existingStore = await _StoreRepository.FindByIdAsync(StoreId);

        if (existingStore == null)
            throw new AppException("Invalid Store");
        
        var existingUser = await _UserRepository.FindByIdAsync(UserId);

        if (existingUser == null)
            throw new AppException("Invalid User");
        
        return await _RatingRepository.FindByStoreIdAndUserAsync(StoreId, UserId);
    }

    public async Task<RatingResponse> SaveAsync(Rating Rating)
    {
        var existingStore = await _StoreRepository.FindByIdAsync(Rating.StoreId);
        if (existingStore == null)
            return new RatingResponse("Invalid Store");
        
        var existingUser = await _UserRepository.FindByIdAsync(Rating.UserId);
        if (existingUser == null)
            return new RatingResponse("Invalid User");
        
        try
        {
            await _RatingRepository.AddAsync(Rating);
            await _unitOfWork.CompleteAsync();
            return new RatingResponse(Rating);
        }
        catch (Exception e)
        {
            return new RatingResponse($"An error occurred while saving the Rating: {e.Message}");
        }
    }

    public async Task<RatingResponse> UpdateAsync(int RatingId, Rating Rating)
    {
        var existingRating = await _RatingRepository.FindByIdAsync(RatingId);
        
        // Validate Rating
        if (existingRating == null)
            return new RatingResponse("Rating not found.");
        // Validate StoreId
        var existingStore = await _StoreRepository.FindByIdAsync(Rating.StoreId);
        if (existingStore == null)
            return new RatingResponse("Invalid Store");
        // Validate UserId
        var existingUser = await _UserRepository.FindByIdAsync(Rating.UserId);
        if (existingUser == null)
            throw new AppException("Invalid User");
        
        // Modify Fields
        existingRating.UserId = Rating.UserId;
        existingRating.StoreId = Rating.StoreId;
        existingRating.Value = Rating.Value;

        try
        {
            _RatingRepository.Update(existingRating);
            await _unitOfWork.CompleteAsync();

            return new RatingResponse(existingRating);
            
        }
        catch (Exception e)
        {
            // Error Handling
            return new RatingResponse($"An error occurred while updating the Rating: {e.Message}");
        }
    }

    public async Task<RatingResponse> DeleteAsync(int RatingId)
    {
        var existingRating = await _RatingRepository.FindByIdAsync(RatingId);
        
        if (existingRating == null)
            return new RatingResponse("Rating not found.");
        
        try
        {
            _RatingRepository.Remove(existingRating);
            await _unitOfWork.CompleteAsync();

            return new RatingResponse(existingRating);
            
        }
        catch (Exception e)
        {
            // Error Handling
            return new RatingResponse($"An error occurred while deleting the Rating: {e.Message}");
        }
    }
}
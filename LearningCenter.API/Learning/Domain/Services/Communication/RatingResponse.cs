using LearningCenter.API.Learning.Domain.Models;
using LearningCenter.API.Shared.Domain.Services.Communication;

namespace LearningCenter.API.Learning.Domain.Services.Communication;

public class RatingResponse : BaseResponse<Rating>
{
    public RatingResponse(string message) : base(message)
    {
    }

    public RatingResponse(Rating model) : base(model)
    {
    }
}
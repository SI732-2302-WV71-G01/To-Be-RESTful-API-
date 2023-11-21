using LearningCenter.API.Learning.Domain.Models;
using LearningCenter.API.Shared.Domain.Services.Communication;

namespace LearningCenter.API.Learning.Domain.Services.Communication;

public class CommentResponse : BaseResponse<Comment>
{
    public CommentResponse(string message) : base(message)
    {
    }

    public CommentResponse(Comment model) : base(model)
    {
    }
}
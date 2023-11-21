using LearningCenter.API.Learning.Domain.Models;
using LearningCenter.API.Learning.Domain.Services.Communication;

namespace LearningCenter.API.Learning.Domain.Services;

public interface ICommentService
{
    Task<IEnumerable<Comment>> ListAsync();
    Task<Comment> GetByIdAsync(int id);
    Task<IEnumerable<Comment>> ListByArticleIdAsync(int articleId);
    Task<IEnumerable<Comment>> ListByStoreIdAsync(int storeId);
    Task<CommentResponse> SaveAsync(Comment comment);
    Task<CommentResponse> UpdateAsync(int commentId, Comment comment);
    Task<CommentResponse> DeleteAsync(int commentId);
}
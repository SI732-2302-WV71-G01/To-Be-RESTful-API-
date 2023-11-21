using LearningCenter.API.Learning.Domain.Models;

namespace LearningCenter.API.Learning.Domain.Repositories;

public interface ICommentRepository
{
    Task<IEnumerable<Comment>> ListAsync();
    Task AddAsync(Comment comment);
    Task<Comment> FindByIdAsync(int id);
    Task<IEnumerable<Comment>> FindByArticleIdAsync(int articleId);
    Task<IEnumerable<Comment>> FindByStoreIdAsync(int storeId);
    void Update(Comment comment);
    void Remove(Comment comment);
}
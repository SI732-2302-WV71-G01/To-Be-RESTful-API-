using LearningCenter.API.Learning.Domain.Models;
using LearningCenter.API.Learning.Domain.Repositories;
using LearningCenter.API.Learning.Domain.Services;
using LearningCenter.API.Learning.Domain.Services.Communication;
using LearningCenter.API.Security.Domain.Repositories;
using LearningCenter.API.Security.Exceptions;

namespace LearningCenter.API.Learning.Services;

public class CommentService : ICommentService
{
     private readonly ICommentRepository _CommentRepository;
    private readonly IArticleRepository _ArticleRepository;
    private readonly IStoreRepository _storeRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CommentService(ICommentRepository CommentRepository, IUnitOfWork unitOfWork, IArticleRepository ArticleRepository, IStoreRepository storeRepository)
    {
        _CommentRepository = CommentRepository;
        _unitOfWork = unitOfWork;
        _ArticleRepository = ArticleRepository;
        _storeRepository = storeRepository;
    }

    public async Task<IEnumerable<Comment>> ListAsync()
    {
        return await _CommentRepository.ListAsync();
    }

    public async Task<Comment> GetByIdAsync(int id)
    {
        var Comment = await _CommentRepository.FindByIdAsync(id);
        if (Comment == null) throw new KeyNotFoundException("Comment not found");
        return Comment;
    }

    public async Task<IEnumerable<Comment>> ListByArticleIdAsync(int articleId)
    {
        var existingArticle = await _ArticleRepository.FindByIdAsync(articleId);

        if (existingArticle == null)
            throw new AppException("Invalid Article");
        
        return await _CommentRepository.FindByArticleIdAsync(articleId);
    }

    public async Task<IEnumerable<Comment>> ListByStoreIdAsync(int storeId)
    {
        var existingStore = await _storeRepository.FindByIdAsync(storeId);

        if (existingStore == null)
            throw new AppException("Invalid Store");
        
        return await _CommentRepository.FindByStoreIdAsync(storeId);
    }

    public async Task<CommentResponse> SaveAsync(Comment comment)
    {
        /*var existingArticle = await _ArticleRepository.FindByIdAsync(comment.ArticleId);
        if (existingArticle == null)
            return new CommentResponse("Invalid Article");*/
        
        try
        {
            await _CommentRepository.AddAsync(comment);
            await _unitOfWork.CompleteAsync();
            return new CommentResponse(comment);
        }
        catch (Exception e)
        {
            return new CommentResponse($"An error occurred while saving the Comment: {e.Message}");
        }
    }

    public async Task<CommentResponse> UpdateAsync(int commentId, Comment comment)
    {
        var existingComment = await _CommentRepository.FindByIdAsync(commentId);
        
        // Validate Comment
        if (existingComment == null)
            return new CommentResponse("Comment not found.");
        // Validate ArticleId
        /*var existingArticle = await _ArticleRepository.FindByIdAsync(comment.ArticleId);
        if (existingArticle == null)
            return new CommentResponse("Invalid Article");*/
        
        // Modify Fields
        existingComment.Username = comment.Username;
        existingComment.Content = comment.Content;
        existingComment.ArticleId = comment.ArticleId;
        existingComment.StoreId = comment.StoreId;

        try
        {
            _CommentRepository.Update(existingComment);
            await _unitOfWork.CompleteAsync();

            return new CommentResponse(existingComment);
            
        }
        catch (Exception e)
        {
            // Error Handling
            return new CommentResponse($"An error occurred while updating the Comment: {e.Message}");
        }
    }

    public async Task<CommentResponse> DeleteAsync(int commentId)
    {
        var existingComment = await _CommentRepository.FindByIdAsync(commentId);
        
        if (existingComment == null)
            return new CommentResponse("Comment not found.");
        
        try
        {
            _CommentRepository.Remove(existingComment);
            await _unitOfWork.CompleteAsync();

            return new CommentResponse(existingComment);
            
        }
        catch (Exception e)
        {
            // Error Handling
            return new CommentResponse($"An error occurred while deleting the comment: {e.Message}");
        }
    }
}
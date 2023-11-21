using AutoMapper;
using LearningCenter.API.Learning.Domain.Models;
using LearningCenter.API.Learning.Domain.Services;
using LearningCenter.API.Learning.Resources;
using LearningCenter.API.Security.Authorization.Attributes;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace LearningCenter.API.Learning.Controllers;

[AllowAnonymous]
[ApiController]
[Route("/api/v1/articles/{articleId}/comments")]
public class ArticleCommentsController : ControllerBase
{
    private readonly ICommentService _CommentService;
    private readonly IMapper _mapper;

    public ArticleCommentsController(ICommentService CommentService, IMapper mapper)
    {
        _CommentService = CommentService;
        _mapper = mapper;
    }
    

    [HttpGet]
    [SwaggerOperation(
        Summary = "Get All Comments for given Article",
        Description = "Get existing Comments associated with the specified Article",
        OperationId = "GetArticleComments",
        Tags = new[] { "Articles"}
    )]
    public async Task<IEnumerable<CommentResource>> GetAllByArticleIdAsync(int ArticleId)
    {
        var comments = await _CommentService.ListByArticleIdAsync(ArticleId);

        var resources = _mapper.Map<IEnumerable<Comment>, IEnumerable<CommentResource>>(comments);

        return resources;
    }
}
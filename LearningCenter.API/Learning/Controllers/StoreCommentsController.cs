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
[Route("/api/v1/stores/{storeId}/comments")]
public class StoreCommentsController : ControllerBase
{
    private readonly ICommentService _CommentService;
    private readonly IMapper _mapper;

    public StoreCommentsController(ICommentService CommentService, IMapper mapper)
    {
        _CommentService = CommentService;
        _mapper = mapper;
    }
    

    [HttpGet]
    [SwaggerOperation(
        Summary = "Get All Comments for given Store",
        Description = "Get existing Comments associated with the specified Store",
        OperationId = "GetStoreComments",
        Tags = new[] { "Stores"}
    )]
    public async Task<IEnumerable<CommentResource>> GetAllByStoreIdAsync(int StoreId)
    {
        var comments = await _CommentService.ListByStoreIdAsync(StoreId);

        var resources = _mapper.Map<IEnumerable<Comment>, IEnumerable<CommentResource>>(comments);

        return resources;
    }
}
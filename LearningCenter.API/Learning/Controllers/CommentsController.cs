using System.Net.Mime;
using AutoMapper;
using LearningCenter.API.Learning.Domain.Models;
using LearningCenter.API.Learning.Domain.Services;
using LearningCenter.API.Learning.Resources;
using LearningCenter.API.Security.Authorization.Attributes;
using LearningCenter.API.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace LearningCenter.API.Learning.Controllers;

[AllowAnonymous]
[ApiController]
[Route("/api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Create, read, update and delete Comments")]
public class CommentsController : ControllerBase
{
    private readonly ICommentService _CommentService;
    private readonly IMapper _mapper;
    
    
    public CommentsController(ICommentService CommentService, IMapper mapper)
    {
        _CommentService = CommentService;
        _mapper = mapper;
    }
    

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CommentResource>), 200)]
    public async Task<IEnumerable<CommentResource>> GetAllAsync()
    {
        return _mapper.Map<IEnumerable<Comment>, 
            IEnumerable<CommentResource>>(await _CommentService.ListAsync());
    }
   
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var Comment = await _CommentService.GetByIdAsync(id);
        var resource = _mapper.Map<Comment, CommentResource>(Comment);
        return Ok(resource);
    }
    
   
    [HttpPost]
    [ProducesResponseType(typeof(CommentResource), 201)]
    [ProducesResponseType(typeof(List<string>), 400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> PostAsync([FromBody] SaveCommentResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
        
        var CommentModel = _mapper.Map<SaveCommentResource, Comment>(resource);
        
        var CommentResponse = await _CommentService.SaveAsync(CommentModel);
        
        if (!CommentResponse.Success)
            return BadRequest(CommentResponse.Message);
        
        var CommentResource = _mapper.Map<Comment, CommentResource>(CommentResponse.Model);
        
        return Ok(CommentResource);
    }
    

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] SaveCommentResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var Comment = _mapper.Map<SaveCommentResource, Comment>(resource);

        var CommentResponse = await _CommentService.UpdateAsync(id, Comment); 

        if (!CommentResponse.Success)
            return BadRequest(CommentResponse.Message);

        var CommentResource = _mapper.Map<Comment, CommentResource>(CommentResponse.Model);

        return Ok(CommentResource);
    }
    
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _CommentService.DeleteAsync(id);
        
        if (!result.Success)
            return BadRequest(result.Message);

        var tutorialResource = _mapper.Map<Comment, CommentResource>(result.Model);

        return Ok(tutorialResource);
    }

}
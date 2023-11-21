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
[SwaggerTag("Create, read, update and delete Ratings")]
public class RatingsController : ControllerBase
{
    private readonly IRatingService _RatingService;
    private readonly IMapper _mapper;
    
    
    public RatingsController(IRatingService RatingService, IMapper mapper)
    {
        _RatingService = RatingService;
        _mapper = mapper;
    }
    

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<RatingResource>), 200)]
    public async Task<IEnumerable<RatingResource>> GetAllAsync()
    {
        return _mapper.Map<IEnumerable<Rating>, 
            IEnumerable<RatingResource>>(await _RatingService.ListAsync());
    }
   
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var Rating = await _RatingService.GetByIdAsync(id);
        var resource = _mapper.Map<Rating, RatingResource>(Rating);
        return Ok(resource);
    }
    
    [HttpGet("{storeId}/{userId}")]
    [ProducesResponseType(typeof(IEnumerable<RatingResource>), 200)]
    public async Task<IEnumerable<RatingResource>> GetAllByStoreIdAndUserIdAsync(int storeId, int userId)
    {
        var ratings = await _RatingService.ListByStoreIdAndUserIdAsync(storeId, userId);

        var resources = _mapper.Map<IEnumerable<Rating>, IEnumerable<RatingResource>>(ratings);

        return resources;

    }
    
   
    [HttpPost]
    [ProducesResponseType(typeof(RatingResource), 201)]
    [ProducesResponseType(typeof(List<string>), 400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> PostAsync([FromBody] SaveRatingResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
        
        var RatingModel = _mapper.Map<SaveRatingResource, Rating>(resource);
        
        var RatingResponse = await _RatingService.SaveAsync(RatingModel);
        
        if (!RatingResponse.Success)
            return BadRequest(RatingResponse.Message);
        
        var RatingResource = _mapper.Map<Rating, RatingResource>(RatingResponse.Model);
        
        return Ok(RatingResource);
    }
    

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] SaveRatingResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var Rating = _mapper.Map<SaveRatingResource, Rating>(resource);

        var RatingResponse = await _RatingService.UpdateAsync(id, Rating); 

        if (!RatingResponse.Success)
            return BadRequest(RatingResponse.Message);

        var RatingResource = _mapper.Map<Rating, RatingResource>(RatingResponse.Model);

        return Ok(RatingResource);
    }
    
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _RatingService.DeleteAsync(id);
        
        if (!result.Success)
            return BadRequest(result.Message);

        var tutorialResource = _mapper.Map<Rating, RatingResource>(result.Model);

        return Ok(tutorialResource);
    }

}
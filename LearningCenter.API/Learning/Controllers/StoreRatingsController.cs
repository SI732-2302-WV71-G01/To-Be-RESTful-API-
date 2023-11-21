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
[Route("/api/v1/stores/{storeId}/ratings")]
public class StoreRatingsController : ControllerBase
{
    private readonly IRatingService _RatingService;
    private readonly IMapper _mapper;

    public StoreRatingsController(IRatingService RatingService, IMapper mapper)
    {
        _RatingService = RatingService;
        _mapper = mapper;
    }
    

    [HttpGet]
    [SwaggerOperation(
        Summary = "Get All Ratings for given Store",
        Description = "Get existing Ratings associated with the specified Store",
        OperationId = "GetStoreRatings",
        Tags = new[] { "Stores"}
    )]
    public async Task<IEnumerable<RatingResource>> GetAllByStoreIdAsync(int StoreId)
    {
        var Ratings = await _RatingService.ListByStoreIdAsync(StoreId);

        var resources = _mapper.Map<IEnumerable<Rating>, IEnumerable<RatingResource>>(Ratings);

        return resources;
    }
}
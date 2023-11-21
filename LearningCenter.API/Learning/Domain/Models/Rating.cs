using LearningCenter.API.Security.Domain.Models;

namespace LearningCenter.API.Learning.Domain.Models;

public class Rating
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int StoreId { get; set; }
    public int Value { get; set; }
}
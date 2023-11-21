namespace LearningCenter.API.Learning.Resources;

public class RatingResource
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int StoreId { get; set; }
    public int Value { get; set; }
}
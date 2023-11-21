namespace LearningCenter.API.Learning.Resources;

public class SaveRatingResource
{
    public int UserId { get; set; }
    public int StoreId { get; set; }
    public int Value { get; set; }
}
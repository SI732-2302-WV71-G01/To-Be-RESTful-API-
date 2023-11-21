namespace LearningCenter.API.Learning.Resources;

public class SaveCommentResource
{
    public string Username { get; set; }
    public string Content { get; set; }
    public int ArticleId { get; set; }
    public int StoreId { get; set; }
}
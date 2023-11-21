namespace LearningCenter.API.Learning.Resources;

public class CommentResource
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Content { get; set; }
    public int ArticleId { get; set; }
    public int StoreId { get; set; }
}
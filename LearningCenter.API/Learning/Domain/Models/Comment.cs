namespace LearningCenter.API.Learning.Domain.Models;

public class Comment
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Content { get; set; }
    public int ArticleId { get; set; }
    public int StoreId { get; set; }
    /*public Article Article { get; set; }*/
}
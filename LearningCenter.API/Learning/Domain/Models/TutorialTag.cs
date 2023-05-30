namespace LearningCenter.API.Learning.Domain.Models;

public class TutorialTag
{
    // Relationships
    
    public int TutorialId { get; set; }
    public Tutorial Tutorial { get; set; }
    public int TagId { get; set; }
    public Tag Tag { get; set; }
}
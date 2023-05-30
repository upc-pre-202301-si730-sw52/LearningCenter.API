namespace LearningCenter.API.Learning.Domain.Models;

public class Tag
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    // Relationships
    public List<TutorialTag> TutorialTags { get; set; }
}
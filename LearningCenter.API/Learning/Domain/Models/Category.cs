namespace LearningCenter.API.Learning.Domain.Models;

public class Category
{
    public Category()
    {
        Tutorials = new List<Tutorial>();
    }

    public int Id { get; set; }
    
    public string Name { get; set; }
    
    // Relationships
    
    public List<Tutorial> Tutorials { get; set; }
}
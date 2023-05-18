using System.ComponentModel.DataAnnotations;

namespace LearningCenter.API.Learning.Resources;

public class SaveCategoryResource
{
    [Required]
    public string Name { get; set; }
}
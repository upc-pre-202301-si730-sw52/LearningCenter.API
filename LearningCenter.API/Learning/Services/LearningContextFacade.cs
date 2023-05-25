using LearningCenter.API.Learning.Interfaces.Internal;

namespace LearningCenter.API.Learning.Services;

public class LearningContextFacade : ILearningContextFacade
{
    private readonly CategoryService _categoryService;
    private readonly TutorialService _tutorialService;
    
    public int TotalTutorials()
    {
        return _tutorialService.ListAsync().Result.Count();
    }

    public int TotalCategories()
    {
        return _categoryService.ListAsync().Result.Count();
    }
}
using LearningCenter.API.Learning.Interfaces.Internal;

namespace LearningCenter.API.Learning.Services;

public class LearningContextFacade : ILearningContextFacade
{
    private readonly CategoryService _categoryService;
    
    public int TotalTutorials()
    {
        return 0;
    }

    public int TotalCategories()
    {
        return _categoryService.ListAsync().Result.Count();
    }
}
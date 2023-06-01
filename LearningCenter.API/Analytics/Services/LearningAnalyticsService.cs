using LearningCenter.API.Analytics.Domain.Services;
using LearningCenter.API.Learning.Interfaces.Internal;

namespace LearningCenter.API.Analytics.Services;

public class LearningAnalyticsService : ILearningAnalyticsService
{
    private readonly ILearningContextFacade _learningContext;

    public LearningAnalyticsService(ILearningContextFacade learningContext)
    {
        _learningContext = learningContext;
    }

    public int TotalLearningTutorialsCount()
    {
        return _learningContext.TotalTutorials();
    }
}
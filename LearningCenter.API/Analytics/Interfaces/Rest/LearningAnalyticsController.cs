using LearningCenter.API.Analytics.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace LearningCenter.API.Analytics.Interfaces.Rest;

[ApiController]
[Route("/api/v1/analytics/learning")]
public class LearningAnalyticsController : ControllerBase
{
    
    private readonly ILearningAnalyticsService _learningAnalyticsService;

    public LearningAnalyticsController(ILearningAnalyticsService learningAnalyticsService)
    {
        _learningAnalyticsService = learningAnalyticsService;
    }

    [HttpGet("tutorials/total")]
    public int GetTutorialsCount()
    {
        return _learningAnalyticsService.TotalLearningTutorialsCount();
    }
}
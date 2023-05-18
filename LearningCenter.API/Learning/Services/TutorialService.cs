using LearningCenter.API.Learning.Domain.Models;
using LearningCenter.API.Learning.Domain.Repositories;
using LearningCenter.API.Learning.Domain.Services;
using LearningCenter.API.Learning.Domain.Services.Communication;
using LearningCenter.API.Shared.Domain.Repositories;

namespace LearningCenter.API.Learning.Services;

public class TutorialService : ITutorialService
{
    private readonly ITutorialRepository _tutorialRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICategoryRepository _categoryRepository;

    public TutorialService(ITutorialRepository tutorialRepository, IUnitOfWork unitOfWork, ICategoryRepository categoryRepository)
    {
        _tutorialRepository = tutorialRepository;
        _unitOfWork = unitOfWork;
        _categoryRepository = categoryRepository;
    }

    public async Task<IEnumerable<Tutorial>> ListAsync()
    {
        return await _tutorialRepository.ListAsync();
    }

    public async Task<IEnumerable<Tutorial>> ListByCategoryIdAsync(int categoryId)
    {
        return await _tutorialRepository.FindByCategoryIdAsync(categoryId);
    }

    public async Task<TutorialResponse> SaveAsync(Tutorial tutorial)
    {
        // Validate existence of assigned category
        
        var existingCategory = await _categoryRepository.FindByIdAsync(tutorial.CategoryId);
        
        if (existingCategory == null)
            return new TutorialResponse("Invalid category.");
        
        // Validate if Title is already used
        
        var existingTutorialWithTitle = await _tutorialRepository.FindByTitleAsync(tutorial.Title);
        
        if (existingTutorialWithTitle != null)
            return new TutorialResponse("Title is already used.");
        
        // Perform adding
        
        try
        {
            await _tutorialRepository.AddAsync(tutorial);
            await _unitOfWork.CompleteAsync();
            
            return new TutorialResponse(tutorial);
        }
        catch (Exception e)
        {
            // Do some logging stuff
            return new TutorialResponse($"An error occurred while saving the tutorial: {e.Message}");
        }
    }

    public Task<TutorialResponse> UpdateAsync(int tutorialId, Tutorial tutorial)
    {
        throw new NotImplementedException();
    }

    public Task<TutorialResponse> DeleteAsync(int tutorialId)
    {
        throw new NotImplementedException();
    }
}
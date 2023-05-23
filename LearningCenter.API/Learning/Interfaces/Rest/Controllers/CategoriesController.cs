using System.Net.Mime;
using AutoMapper;
using LearningCenter.API.Learning.Domain.Models;
using LearningCenter.API.Learning.Domain.Services;
using LearningCenter.API.Learning.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace LearningCenter.API.Learning.Interfaces.Rest.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;
    private readonly IMapper _mapper;

    public CategoriesController(ICategoryService categoryService, IMapper mapper)
    {
        _categoryService = categoryService;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CategoryResource>), 200)]
    public async Task<IEnumerable<CategoryResource>> GetAllAsync()
    {
        var categories = await _categoryService.ListAsync();
        var resources = _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryResource>>(categories);
        return resources;
    }

}
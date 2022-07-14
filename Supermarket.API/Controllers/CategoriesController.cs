using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Supermarket.API.Domain.Models;
using Supermarket.API.Domain.Services;
using Supermarket.API.Resources;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Supermarket.API.Extensions;
using AutoMapper;

namespace Supermarket.API.Controllers
{
  [Route("/api/[controller]")]
  public class CategoriesController : Controller
  {
    private readonly ICategoryService _categoryService;
    private readonly IMapper _mapper;

    public CategoriesController(ICategoryService categoryService, IMapper mapper)
    {
      _categoryService = categoryService;
      _mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<CategoryResource>> GetAllAsync()
    {
      var categories = await _categoryService.ListAsync();
      var resources = _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryResource>>(categories);

      return resources;
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] SaveCategoryResource resource)
    {
      if (!ModelState.IsValid)
        return BadRequest(ModelState.GetErrorMessages());

      var category = _mapper.Map<SaveCategoryResource, Category>(resource);
      var result = await _categoryService.SaveAsync(category);

      if (!result.Success)
        return BadRequest(result.Message);

      var CategoryResource = _mapper.Map<Category, CategoryResource>(result.Resource);
      return Ok(CategoryResource);
    }
  }
}
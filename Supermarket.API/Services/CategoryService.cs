using System.Collections.Generic;
using System.Threading.Tasks;
using Supermarket.API.Domain.Models;
using Supermarket.API.Domain.Services;
using Supermarket.API.Domain.Repositories;

namespace Supermarket.API.Services
{
  public class CategoryService : ICategoryService
  {
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
      this._categoryRepository = categoryRepository;
    }

    public async Task<IEnumerable<Category>> ListAsync()
    {
      return await _categoryRepository.ListAsync();
    }
  }
}
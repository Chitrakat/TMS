using TaskManagementAPI.Interfaces;
using TaskManagementAPI.DTOs;

namespace TaskManagementAPI.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<IEnumerable<CategoryResponseDto>> GetAllCategoriesAsync()
    {
        var categories = await _categoryRepository.GetAllAsync();
        return categories.Select(EntityDtoMapper.ToCategoryResponseDto);
    }

    public async Task<CategoryResponseDto?> GetCategoryByIdAsync(int id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        return category is null ? null : EntityDtoMapper.ToCategoryResponseDto(category);
    }

    public async Task<CategoryResponseDto> CreateCategoryAsync(CategoryRequestDto category)
    {
        var newCategory = EntityDtoMapper.ToCategoryEntity(category);
        newCategory.Name = newCategory.Name.Trim();

        await _categoryRepository.AddAsync(newCategory);
        await _categoryRepository.SaveChangesAsync();

        return EntityDtoMapper.ToCategoryResponseDto(newCategory);
    }

    public async Task<bool> DeleteCategoryAsync(int id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        if (category is null)
        {
            return false;
        }

        if (await _categoryRepository.HasTasksAsync(id))
        {
            return false;
        }

        _categoryRepository.Delete(category);
        await _categoryRepository.SaveChangesAsync();

        return true;
    }
}

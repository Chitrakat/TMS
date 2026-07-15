using TaskManagementAPI.DTOs;

namespace TaskManagementAPI.Interfaces;

public interface ICategoryService
{
    Task<IEnumerable<CategoryResponseDto>> GetAllCategoriesAsync();
    Task<CategoryResponseDto?> GetCategoryByIdAsync(int id);
    Task<CategoryResponseDto> CreateCategoryAsync(CategoryRequestDto category);
    Task<bool> DeleteCategoryAsync(int id);
}

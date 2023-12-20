using AutoMapper;
using BusinessLogicLayer.Extended;
using BusinessLogicLayer.Interfaces;
using DataAccesLayer.Interfaces;
using DataAccesLayer.Models;
using DTO.DTOs.CategoryDtos;

namespace BusinessLogicLayer.Services;

public class CategoryService (IUnitOfWork unitOfWork,
                              IMapper mapper)
    : ICategoryService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task Add(AddCategoryDto categoryDto)
    {
        if (categoryDto == null)
        {
            throw new ArgumentNullException("Category null bo'lib qoldi!");
        }

        var category = _mapper.Map<Category>(categoryDto);
        if (!category.IsValid())
        {
            throw new CustomException("Invalid Category");
        }

        var categories = await _unitOfWork.CategoryInterface.GetAllAsync();
        if (category.IsExist(categories))
        {
            throw new CustomException.($"{category.Name} uje bor!");
        }

        await _unitOfWork.CategoryInterface.AddAsync(category);
        await _unitOfWork.SaveAsync();

    }

    public async Task Delete(int id)
    {
        var category = await _unitOfWork.CategoryInterface.GetByIdAsync(id);
        if (category is null)
        {
            throw new ArgumentException("Bunday Category mavjud emas!");
        }

        await _unitOfWork.CategoryInterface.DeleteAsync(category);
        await _unitOfWork.SaveAsync();
    }

    public async Task<List<CategoryDto>> GetAll()
    {
        var categories = await _unitOfWork.CategoryInterface.GetAllAsync();
        return categories.Select(c => _mapper.Map<CategoryDto>(c))
                         .ToList();
    }

    public Task<PagedList<CategoryDto>> GetAllPaged(int pageSize, int pageNumber)
    {
        throw new NotImplementedException();
    }

    public Task<CategoryDto> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Task Update(UpdateCategoryDto categoryDto)
    {
        throw new NotImplementedException();
    }
}

using AutoMapper;
using BusinessLogicLayer.Extended;
using BusinessLogicLayer.Interfaces;
using DataAccesLayer.Interfaces;
using DataAccesLayer.Models;
using DTO.DTOs.SubRegionDtos;

namespace BusinessLogicLayer.Services;
public class SubRegionService(IUnitOfWork unitOfWork,
                           IMapper mapper)
    : ISubRegionService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task AddAsync(AddSubRegionDto newSubRegion)
    {
        if (newSubRegion == null)
        {
            throw new ArgumentNullException("Region is null");
        }

        var model = _mapper.Map<SubRegion>(newSubRegion);
        if (!model.IsValid())
        {
            throw new CustomException("SubRegion is not valid");
        }

        var regions = await _unitOfWork.SubRegionInterface.GetAllAsync();
        if (model.IsExist(regions))
        {
            throw new CustomException("SubRegion is already exist");
        }

        await _unitOfWork.SubRegionInterface.AddAsync(model);
        await _unitOfWork.SaveAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var model = await _unitOfWork.SubRegionInterface.GetByIdAsync(id);
        if (model == null)
        {
            throw new ArgumentNullException("Region is not found");
        }

        await _unitOfWork.SubRegionInterface.DeleteAsync(model);
        await _unitOfWork.SaveAsync();
    }

    public async Task<List<SubRegionDto>> GetAllAsync()
    {
        var regions = await _unitOfWork.SubRegionInterface.GetAllAsync();
        if (regions == null)
        {
            return new();
        }

        return regions.Select(_mapper.Map<SubRegionDto>)
                      .ToList();
    }

    public async Task<SubRegionDto> GetByIdAsync(int id)
    {
        var model = await _unitOfWork.SubRegionInterface.GetByIdAsync(id);
        if (model == null)
        {
            throw new ArgumentNullException("Region is not found");
        }

        return _mapper.Map<SubRegionDto>(model);
    }

    public async Task UpdateAsync(UpdateSubRegionDto region)
    {
        var model = await _unitOfWork.SubRegionInterface.GetByIdAsync(region.Id);
        if (model == null)
        {
            throw new ArgumentNullException("Region is not found");
        }

        model = _mapper.Map<SubRegion>(model);
        if (!model.IsValid())
        {
            throw new CustomException("Region is not valid");
        }
        await _unitOfWork.SubRegionInterface.UpdateAsync(model);
        await _unitOfWork.SaveAsync();
    }
}
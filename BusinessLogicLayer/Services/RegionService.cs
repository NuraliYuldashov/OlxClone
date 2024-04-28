using AutoMapper;
using BusinessLogicLayer.Extended;
using BusinessLogicLayer.Interfaces;
using DataAccesLayer.Interfaces;
using DataAccesLayer.Models;
using DTO.DTOs.RegionDtos;
using DTO.DTOs.SubRegionDtos;

namespace BusinessLogicLayer.Services;
public class RegionService(IUnitOfWork unitOfWork,
                           IMapper mapper)
    : IRegionService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task AddAsync(AddRegionDto newRegion)
    {
        if (newRegion == null)
        {
            throw new ArgumentNullException("Region is null");
        }

        var model = _mapper.Map<Region>(newRegion);
        if (!model.IsValid())
        {
            throw new CustomException("Region is not valid");
        }

        var regions = await _unitOfWork.RegionInterface.GetAllAsync();
        if (model.IsExist(regions))
        {
            throw new CustomException("Region is already exist");
        }

        await _unitOfWork.RegionInterface.AddAsync(model);
        await _unitOfWork.SaveAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var model = await _unitOfWork.RegionInterface.GetByIdAsync(id);
        if (model == null)
        {
            throw new ArgumentNullException("Region is not found");
        }

        await _unitOfWork.RegionInterface.DeleteAsync(model);
        await _unitOfWork.SaveAsync();
    }

    public async Task<List<RegionDto>> GetAllAsync()
    {
        var regions = await _unitOfWork.RegionInterface.GetAllAsync();
        if (regions == null)
        {
            return new();
        }

        var subregions = await _unitOfWork.SubRegionInterface.GetAllAsync();

        var list = regions.Select(c => new RegionDto()
        {
            Id = c.Id,
            Name = c.Name,
            SubRegions = subregions.Where(s => s.RegionId == c.Id)
            .Select(s => new SubRegionDto()
            {
                Id = s.Id,
                Name = s.Name,
                RegionId = s.RegionId
            }).ToList()
        }).ToList();

        return list;
    }

    public async Task<RegionDto> GetByIdAsync(int id)
    {
        var model = await _unitOfWork.RegionInterface.GetByIdAsync(id);
        if (model == null)
        {
            throw new ArgumentNullException("Region is not found");
        }

        return _mapper.Map<RegionDto>(model);
    }

    public async Task UpdateAsync(UpdateRegionDto region)
    {
        var model = await _unitOfWork.RegionInterface.GetByIdAsync(region.Id);
        if (model == null)
        {
            throw new ArgumentNullException("Region is not found");
        }

        model = _mapper.Map<Region>(model);
        if (!model.IsValid())
        {
            throw new CustomException("Region is not valid");
        }
        await _unitOfWork.RegionInterface.UpdateAsync(model);
        await _unitOfWork.SaveAsync();
    }
}
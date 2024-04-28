using DTO.DTOs.RegionDtos;

namespace BusinessLogicLayer.Interfaces;
public interface IRegionService
{
    Task<List<RegionDto>> GetAllAsync();
    Task<RegionDto> GetByIdAsync(int id);
    Task AddAsync(AddRegionDto newRegion);
    Task UpdateAsync(UpdateRegionDto region);
    Task DeleteAsync(int id);
}
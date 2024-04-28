using DTO.DTOs.RegionDtos;
using DTO.DTOs.SubRegionDtos;

namespace BusinessLogicLayer.Interfaces;
public interface ISubRegionService
{
    Task<List<SubRegionDto>> GetAllAsync();
    Task<SubRegionDto> GetByIdAsync(int id);
    Task AddAsync(AddSubRegionDto newRegion);
    Task UpdateAsync(UpdateSubRegionDto region);
    Task DeleteAsync(int id);
}
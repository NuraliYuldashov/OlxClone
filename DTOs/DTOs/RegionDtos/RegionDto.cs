using DTO.DTOs.SubRegionDtos;

namespace DTO.DTOs.RegionDtos;

public class RegionDto : BaseDto
{
    public string? Name { get; set; }
    public List<SubRegionDto> SubRegions { get; set; } = new();
}

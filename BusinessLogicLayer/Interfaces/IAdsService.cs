using DTO.DTOs.AdsElonDtos;

namespace BusinessLogicLayer.Interfaces;
public interface IAdsService
{
    Task<List<AdsElonDto>> GetAllAsync();
    Task<AdsElonDto> GetByIdAsync(int id);
    Task AddAsync(AddAdsElon newAds);
    Task UpdateAsync(UpdateAdsDto Ads, string folderName);
    Task DeleteAsync(int id, string folderName);
}
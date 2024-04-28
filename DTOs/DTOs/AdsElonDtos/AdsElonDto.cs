using DataAccesLayer.Models;
using DTO.DTOs.SubCategoryDtos;
using DTO.DTOs.SubRegionDtos;
using DTO.UserDtos;

namespace DTO.DTOs.AdsElonDtos;

public partial class AdsElonDto : BaseDto
{
    public string? UserId { get; set; }

    public string? Title { get; set; }

    public decimal? Price { get; set; }

    public string? Description { get; set; }

    public int? SubCategoryId { get; set; }

    public int? SubRegionId { get; set; }

    public string? State { get; set; } 

    public List<string> ImageUrls { get; set; } = new();

    public virtual SubCategoryDto? SubCategoryDto { get; set; }

    public virtual SubRegionDto? SubRegionDto { get; set; }

    public UserDto? User { get; set; }


    public static implicit operator AdsElonDto(AdsElon v)
        => new()
        {
            Id = v.Id,
            UserId = v.UserId,
            Title = v.Title,
            Price = v.Price,
            Description = v.Description,
            SubRegionId = v.SubRegionId,
            SubRegionDto = new SubRegionDto()
            {
                Id = v.SubRegionNavigation.Id,
                Name = v.SubRegionNavigation.Name,
                RegionId = v.SubRegionNavigation.RegionId
            },
            SubCategoryId = v.SubCategoryId,
            SubCategoryDto = new SubCategoryDto()
            {
                Id = v.SubCategory.Id,
                Name = v.SubCategory.Name,
                CategoryId = v.SubCategory.CategoryId
            },
            State = v.State,
            ImageUrls = v.Images.Select(t => t.Url).ToList(),
            User = new UserDto()
            {
                Id = v.UserId,
                PhoneNumber = v.User.PhoneNumber,
                FullName = v.User.FullName,
                ImageUrl = v.User.ImageUrl,
                LastActive = v.User.LastActive
            }
        };
}

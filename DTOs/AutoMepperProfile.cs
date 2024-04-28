

using AutoMapper;
using DataAccesLayer.Models;
using DTO.DTOs.AdsElonDtos;
using DTO.DTOs.CategoryDtos;
using DTO.DTOs.RegionDtos;
using DTO.DTOs.SubCategoryDtos;
using DTO.DTOs.SubRegionDtos;

namespace DTO;

public class AutoMepperProfile : Profile
{
    public AutoMepperProfile()
    {
        CreateMap<Category, CategoryDto>();
        CreateMap<AddCategoryDto, Category>();
        CreateMap<UpdateCategoryDto, Category>();

        CreateMap<SubCategory, SubCategoryDto>();
        CreateMap<AddSubCategoryDto, SubCategory>();
        CreateMap<UpdateSubCategoryDto, SubCategory>();

        CreateMap<AddRegionDto, Region>();
        CreateMap<Region, RegionDto>();
        CreateMap<UpdateRegionDto, Region>();

        CreateMap<AddSubRegionDto, SubRegion>();
        CreateMap<SubRegion, SubRegionDto>();
        CreateMap<UpdateSubRegionDto, SubRegion>();

        CreateMap<AddAdsElon, AdsElon>();
        
        // CreateMap<AdsElon, AdsElonDto>()
        //     .ForMember(t => t.SubCategory, opt => opt.MapFrom(t => new SubCategoryDto() 
        //         { Id = t.SubCategory.Id, Name = t.SubCategory.Name }))
        //     .ForMember(t => t.SubRegionDto, opt => opt.MapFrom(t => new SubRegionDto() 
        //         { Id = t.SubRegionNavigation.Id, Name = t.SubRegionNavigation.Name }))
        //     .ForMember(t => t.ImageUrls, opt => opt.MapFrom(t => t.Images.Select(t => t.Url).ToList()));

        CreateMap<UpdateAdsDto, AdsElon>();
    }
}

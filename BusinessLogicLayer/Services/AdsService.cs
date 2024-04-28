using AutoMapper;
using BusinessLogicLayer.Extended;
using BusinessLogicLayer.Interfaces;
using DataAccesLayer.Interfaces;
using DataAccesLayer.Models;
using DTO.DTOs.AdsElonDtos;

namespace BusinessLogicLayer.Services;
public class AdsService (IUnitOfWork unitOfWork,
                         IMapper mapper,
                         ImageInterface imageService)
: IAdsService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task AddAsync(AddAdsElon newAds)
    {
        if (newAds is null)
            throw new ArgumentNullException(nameof(newAds));

        var ads = _mapper.Map<AdsElon>(newAds);
        ads.SubCategory = null;
        ads.SubRegionNavigation = null;

        if (!ads.IsValid())
            throw new ArgumentException("Ads is not valid");

        var model = await _unitOfWork.AdsElonInterface.AddAsync(ads);
        await _unitOfWork.SaveAsync();

        foreach (var item in newAds.ImageUrls)
        {
            var image = new Image
            {
                AdsElonId = model.Id,
                Url = item
            };
            await _unitOfWork.ImageInterface.AddAsync(image);
            await _unitOfWork.SaveAsync();
        }
    }

    public async Task DeleteAsync(int id, string folderName)
    {
        var model = await _unitOfWork.AdsElonInterface.GetByIdAsync(id);
        if (model is null)
            throw new ArgumentException("Ads not found");

        var images = await _unitOfWork.ImageInterface.Filter(t => t.AdsElonId == id);
        foreach (var item in images)
        {
            await imageService.DeleteAsync(item.Url, folderName);
            await _unitOfWork.ImageInterface.DeleteAsync(item);
            await _unitOfWork.SaveAsync();
        }

        await _unitOfWork.AdsElonInterface.DeleteAsync(model);
        await _unitOfWork.SaveAsync();
    }

    public async Task<List<AdsElonDto>> GetAllAsync()
    {
        var models = await _unitOfWork.AdsElonInterface.GetAllWithDependenciesAsync();
        return models.Select(a => (AdsElonDto)a)
                     .ToList();
    }

    public async Task<AdsElonDto> GetByIdAsync(int id)
    {
        var model = await _unitOfWork.AdsElonInterface.GetByIdAsync(id);
        if (model is null)
            throw new ArgumentException("Ads not found");

        return (AdsElonDto)model;
    }

    public async Task UpdateAsync(UpdateAdsDto newAds, string folderName)
    {
        if (newAds is null)
        {
            throw new ArgumentNullException(nameof(newAds));
        }

        var model = await _unitOfWork.AdsElonInterface.GetByIdAsync(newAds.Id);
        if (model is null)
        {
            throw new ArgumentException("Ads not found");
        }

        model = _mapper.Map<AdsElon>(newAds);
        model.SubCategory = null;
        model.SubRegionNavigation = null;

        if (!model.IsValid())
            throw new ArgumentException("Ads is not valid");

        await _unitOfWork.AdsElonInterface.UpdateAsync(model);
        await _unitOfWork.SaveAsync();

        var images = await _unitOfWork.ImageInterface.Filter(t => t.AdsElonId == model.Id);

        var oldImagesUrls = images.Select(t => t.Url).ToList();
        var newImagesUrls = newAds.ImageUrls;

        var imageUrlsForDelete = oldImagesUrls.Except(newImagesUrls).ToList();
        foreach (var item in imageUrlsForDelete)
        {
            var image = images.FirstOrDefault(t => t.Url == item);
            await imageService.DeleteAsync(image.Url, folderName);
            await _unitOfWork.ImageInterface.DeleteAsync(image);
            await _unitOfWork.SaveAsync();
        }

        var imageUrlsForAdd = newImagesUrls.Except(oldImagesUrls).ToList();
        foreach (var item in imageUrlsForAdd)
        {
            var image = new Image
            {
                AdsElonId = model.Id,
                Url = item
            };
            await _unitOfWork.ImageInterface.AddAsync(image);
            await _unitOfWork.SaveAsync();
        }
    }
}
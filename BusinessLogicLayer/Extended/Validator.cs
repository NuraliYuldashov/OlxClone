using DataAccesLayer.Models;

namespace BusinessLogicLayer.Extended;
public static class Validator
{
    public static bool IsValid(this Category category)
        => category != null
        && !string.IsNullOrEmpty(category.Name);

    public static bool IsExist(this Category category, 
                              IEnumerable<Category> categories)
        => categories.Any(c => c.Name == category.Name 
                               && c.Id != category.Id);

    public static bool IsValid(this SubCategory subcategory)
        => subcategory != null
            && !string.IsNullOrEmpty(subcategory.Name)
            && subcategory.CategoryId > 0;

    public static bool IsExist(this SubCategory subcategory,
                              IEnumerable<SubCategory> subcategories)
        => subcategories.Any(c => c.Name == subcategory.Name
                               && c.Id != subcategory.Id);

    public static bool IsValid(this Region region)
        => region != null
        && !string.IsNullOrEmpty(region.Name);

    public static bool IsValid(this SubRegion region)
        => region != null
        && !string.IsNullOrEmpty(region.Name)
        && region.RegionId > 0;

    public static bool IsExist(this Region region,
                             IEnumerable<Region> regions)
       => regions.Any(c => c.Name == region.Name
                           && c.Id != region.Id);

    public static bool IsExist(this SubRegion region,
                             IEnumerable<SubRegion> regions)
       => regions.Any(c => c.Name == region.Name
                           && c.Id != region.Id
                           && c.RegionId == region.RegionId);

    public static bool IsValid(this AdsElon ads)
        => ads != null
        && !string.IsNullOrEmpty(ads.Title)
        && !string.IsNullOrEmpty(ads.Description)
        && ads.Price > 0
        && ads.SubCategoryId > 0
        && ads.SubRegionId > 0
        && !string.IsNullOrEmpty(ads.UserId);
}
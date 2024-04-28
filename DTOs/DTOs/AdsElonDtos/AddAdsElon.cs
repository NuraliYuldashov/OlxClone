namespace DTO.DTOs.AdsElonDtos;

public class AddAdsElon
{
    public string? UserId { get; set; }

    public string? Title { get; set; }

    public decimal? Price { get; set; }

    public string? Description { get; set; }

    public string? State { get; set; }

    public int? SubCategoryId { get; set; }

    public int? SubRegionId { get; set; }

    public List<string> ImageUrls { get; set; } = new();
}
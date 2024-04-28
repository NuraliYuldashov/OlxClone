using DTOs.CategoryDtos;
using Microsoft.AspNetCore.Http;

namespace BusinessLogicLayer.Interfaces;
public interface ImageInterface
{
    Task<string> UploadAsync(IFormFile file, string folderName, string domain);
    Task DeleteAsync(RemoveImageDto dto, string folderName);
    Task DeleteAsync(string url, string folderName);
}
using BusinessLogicLayer.Interfaces;
using DTOs.CategoryDtos;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayers.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ImagesController(ImageInterface imageService,
                              IWebHostEnvironment hostEnvironment,
                              IConfiguration configuration)
    : ControllerBase
{
    private readonly ImageInterface _imageService = imageService;
    private readonly IWebHostEnvironment _hostEnvironment = hostEnvironment;
    private readonly IConfiguration _configuration = configuration;

    [HttpPost]
    public async Task<IActionResult> Upload(IFormFile file)
    {
        try
        {
            var folderName = Path.Combine(_hostEnvironment.WebRootPath, "images");
            var domain = _configuration["Domain"]!;
            var result = await _imageService.UploadAsync(file, folderName, domain);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPost("delete")]
    public async Task<IActionResult> Delete([FromBody] RemoveImageDto dto)
    {
        try
        {
            var folderName = Path.Combine(_hostEnvironment.WebRootPath, "images");
            await _imageService.DeleteAsync(dto, folderName);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}
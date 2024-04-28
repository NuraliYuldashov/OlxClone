using BusinessLogicLayer.Extended;
using BusinessLogicLayer.Interfaces;
using DTO.DTOs.AdsElonDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace PresentationLayers.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AdsController(IAdsService adsService,
                            IWebHostEnvironment hostEnvironment)
    : ControllerBase
{
    private readonly IAdsService _adsService = adsService;
    private readonly IWebHostEnvironment _hostEnvironment = hostEnvironment;

    [HttpGet("getall")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get()
    {
        try
        {
            var categories = await _adsService.GetAllAsync();

            var json = JsonConvert.SerializeObject(categories, Formatting.Indented,
                new JsonSerializerSettings() 
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });

            return Ok(json);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet("getall/{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll(string userId)
    {
        try
        {
            var categories = await _adsService.GetAllAsync();
            var list = categories.Where(x => x.UserId == userId).ToList();

            var json = JsonConvert.SerializeObject(list, Formatting.Indented,
                new JsonSerializerSettings() 
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });

            return Ok(json);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        try
        {
            var Ads = await _adsService.GetByIdAsync(id);
            return Ok(Ads);
        }
        catch(ArgumentNullException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Post(AddAdsElon dto)
    {
        try
        {
            await _adsService.AddAsync(dto);
            return Ok();
        }
        catch (ArgumentNullException ex)
        {
            return NotFound(ex.Message);
        }
        catch (CustomException ex)
        {
            return BadRequest(ex.ErrorMessage);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Put(UpdateAdsDto dto)
    {
        try
        {
            var folderName = Path.Combine(_hostEnvironment.WebRootPath, "images");
            await _adsService.UpdateAsync(dto, folderName);
            return Ok();
        }
        catch (ArgumentNullException ex)
        {
            return NotFound(ex.Message);
        }
        catch (CustomException ex)
        {
            return BadRequest(ex.ErrorMessage);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var folderName = Path.Combine(_hostEnvironment.WebRootPath, "images");            
            await _adsService.DeleteAsync(id, folderName);
            return Ok();
        }
        catch (ArgumentNullException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}
﻿namespace DTO.DTOs.CategoryDtos;

public class UpdateCategoryDto : BaseDto
{
    public string? Name { get; set; }
    public string? ImageUrl { get; set; }
}
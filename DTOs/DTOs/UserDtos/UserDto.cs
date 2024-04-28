using DTO.DTOs;

namespace DTO.UserDtos;
public class UserDto
{
    public string Id { get; set; } = string.Empty;
    public string? FullName { get; set; }
    public string? ImageUrl { get; set; }
    public DateTime? LastActive { get; set; }
    public string? PhoneNumber { get; set; }
}
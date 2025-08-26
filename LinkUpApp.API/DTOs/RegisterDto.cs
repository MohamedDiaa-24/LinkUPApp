using System;
using System.ComponentModel.DataAnnotations;

namespace LinkUpApp.API.DTOs;

public class RegisterDto
{
    [Required]
    public string Username { get; set; } = string.Empty;

    [Required]
    [Length(4,8)]
    public string Password { get; set; }= string.Empty;

}

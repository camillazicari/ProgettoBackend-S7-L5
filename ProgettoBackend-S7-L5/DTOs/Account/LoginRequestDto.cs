namespace ProgettoBackend_S7_L5.DTOs.Account;

public class LoginRequestDto
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}
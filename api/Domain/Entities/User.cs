namespace Domain.Entities;

public class User
{
    public required string Id { get; set; }

    public required string Email { get; set; } = string.Empty;

    public required string Password { get; set; } = string.Empty;
}

using MediatR;

namespace Application.Users.Create;

public class CreateUserRequest : IRequest
{
    public required string Email { get; set; }

    public required string Password { get; set; }
}

using MediatR;

namespace Application.Authentication.Login;

public class LoginRequest : IRequest<LoginResponse>
{
    public required string Email { get; set; }

    public required string Password { get; set; }
}

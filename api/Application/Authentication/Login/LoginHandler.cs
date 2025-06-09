using Application.Common.DataContext;
using Application.Common.JwtToken;
using Domain.Entities;
using MediatR;

namespace Application.Authentication.Login;

public class LoginHandler(IDataContext dataContext, IAuthTokenService authTokenService) : IRequestHandler<LoginRequest, LoginResponse>
{
    public Task<LoginResponse> Handle(LoginRequest request, CancellationToken cancellationToken)
    {
        var user = dataContext
            .ReadSet<User>()
            .FirstOrDefault(x => x.Email == request.Email && x.Password == request.Password);

        if (user == null) throw new Exception("Invalid credentials");

        var token = authTokenService.Generate(user);

        return Task.FromResult(new LoginResponse { Token = token });
    }
}

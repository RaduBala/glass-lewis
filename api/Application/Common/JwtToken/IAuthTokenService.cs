using Domain.Entities;

namespace Application.Common.JwtToken;

public interface IAuthTokenService
{
    string Generate(User user);
}

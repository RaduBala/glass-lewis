using Domain.Entities;

namespace Application.Users.Create;

public static class CreateUserMapping
{
    public static User ToEntity(this CreateUserRequest request)
    {
        return new User
        {
            Id = Guid.NewGuid().ToString(),
            Email = request.Email,
            Password = request.Password
        };
    }
}

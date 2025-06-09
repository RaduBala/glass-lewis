using Application.Common.DataContext;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Users.Create;

public class CreateUserHandler(IDataContext dataContext) : IRequestHandler<CreateUserRequest>
{
    public async Task Handle(CreateUserRequest request, CancellationToken cancellationToken)
    {
        var user = await dataContext.ReadSet<User>().FirstOrDefaultAsync(x => x.Email == request.Email, cancellationToken);

        if (user is not null)
        {
            throw new ArgumentException("User already exist");
        }

        dataContext.Set<User>().Add(request.ToEntity());

        await dataContext.SaveChangesAsync(cancellationToken);
    }
}

using Application.Users.Create;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("users")]
[ApiController]
public class UsersController(IMediator mediator) : ControllerBase
{
    [HttpPost("")]
    public async Task<ActionResult> Create([FromBody] CreateUserRequest request, CancellationToken cancellationToken)
    {
        await mediator.Send(request, cancellationToken);

        return Accepted();
    }
}

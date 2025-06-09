using Application.Authentication.Login;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;


[Route("authentication")]
[ApiController]
public class AuthenticationController(IMediator mediator) : ControllerBase
{
    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> Login(LoginRequest request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(request, cancellationToken);

        return Ok(result);
    }
}

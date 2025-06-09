using Application.Companies.Create;
using Application.Companies.GetById;
using Application.Companies.GetByIsin;
using Application.Companies.GetList;
using Application.Companies.Update;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("companies")]
[Authorize]
public class CompaniesController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CompanyCreateRequest request, CancellationToken cancellationToken)
    {
        var response = await mediator.Send(request, cancellationToken);

        return Created("companies", response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] string id, CancellationToken cancellationToken)
    {
        var response = await mediator.Send(new CompanyGetByIdRequest() { Id = id }, cancellationToken);

        if (response == null) return NotFound();

        return Ok(response);
    }

    [HttpGet("by-isin/{isin}")]
    public async Task<IActionResult> GetByIsin([FromRoute] string isin, CancellationToken cancellationToken)
    {
        var response = await mediator.Send(new CompanyGetByIsinRequest() { Isin = isin }, cancellationToken);

        if (response == null) return NotFound();

        return Ok(response);
    }

    [HttpGet("list")]
    public async Task<IActionResult> GetList(CancellationToken cancellationToken)
    {
        var response = await mediator.Send(new CompanyGetListRequest(), cancellationToken);

        return Ok(response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] string id, [FromBody] CompanyUpdateRequest request, CancellationToken cancellationToken)
    {
        request.Id = id;

        await mediator.Send(request, cancellationToken);

        return Ok();
    }
}

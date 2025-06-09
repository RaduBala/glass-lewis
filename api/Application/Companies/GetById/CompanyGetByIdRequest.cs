using Application.Common.DTOs;
using MediatR;

namespace Application.Companies.GetById;

public class CompanyGetByIdRequest : IRequest<CompanyDto?>
{
    public string Id { get; set; }
}

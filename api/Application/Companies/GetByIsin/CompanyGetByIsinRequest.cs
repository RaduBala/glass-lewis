using Application.Common.DTOs;
using MediatR;

namespace Application.Companies.GetByIsin;

public class CompanyGetByIsinRequest : IRequest<CompanyDto?>
{
    public string Isin { get; set; }
}

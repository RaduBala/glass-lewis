using Application.Common.DTOs;
using MediatR;

namespace Application.Companies.GetList;

public class CompanyGetListRequest : IRequest<IEnumerable<CompanyDto>>
{
}

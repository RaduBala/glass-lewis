using Application.Common.DataContext;
using Application.Common.DTOs;
using Application.Common.Mapping;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Companies.GetList;

public class CompanyGetListHandler(IDataContext dataContext) : IRequestHandler<CompanyGetListRequest, IEnumerable<CompanyDto>>
{
    public async Task<IEnumerable<CompanyDto>> Handle(CompanyGetListRequest request, CancellationToken cancellationToken)
    {
        var companies = await dataContext.ReadSet<Company>().ToListAsync(cancellationToken);

        return companies.Select(c => c.ToDto());
    }
}
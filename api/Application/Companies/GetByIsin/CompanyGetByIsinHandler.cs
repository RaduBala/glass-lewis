using Application.Common.DataContext;
using Application.Common.DTOs;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Common.Mapping;

namespace Application.Companies.GetByIsin;

public class CompanyGetByIsinHandler(IDataContext dataContext) : IRequestHandler<CompanyGetByIsinRequest, CompanyDto?>
{
    public async Task<CompanyDto?> Handle(CompanyGetByIsinRequest request, CancellationToken cancellationToken)
    {
        var company = await dataContext.ReadSet<Company>().FirstOrDefaultAsync(c => c.Isin == request.Isin, cancellationToken);

        return company == null ? null : company.ToDto();
    }
}
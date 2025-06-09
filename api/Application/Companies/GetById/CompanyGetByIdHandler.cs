using Application.Common.DataContext;
using Application.Common.DTOs;
using Application.Common.Mapping;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Companies.GetById;

public class CompanyGetByIdHandler(IDataContext dataContext) : IRequestHandler<CompanyGetByIdRequest, CompanyDto?>
{
    public async Task<CompanyDto?> Handle(CompanyGetByIdRequest request, CancellationToken cancellationToken)
    {
        var company = await dataContext.ReadSet<Company>().FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

        return company == null ? null : company.ToDto();
    }
}

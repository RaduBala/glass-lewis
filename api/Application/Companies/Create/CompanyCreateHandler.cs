using Application.Common.DataContext;
using Domain.Entities;
using MediatR;

namespace Application.Companies.Create;

public class CompanyCreateHandler(IDataContext dataContext) : IRequestHandler<CompanyCreateRequest, CompanyCreateResponse>
{
    public async Task<CompanyCreateResponse> Handle(CompanyCreateRequest request, CancellationToken cancellationToken)
    {
        var company = request.ToEntity();

        dataContext.Set<Company>().Add(company);

        await dataContext.SaveChangesAsync(cancellationToken);

        return new CompanyCreateResponse() { Id = company.Id };
    }
}

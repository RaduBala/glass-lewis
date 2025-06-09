using Application.Common.DataContext;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Companies.Update;

public class CompanyUpdateHandler(IDataContext dataContext) : IRequestHandler<CompanyUpdateRequest>
{
    public async Task Handle(CompanyUpdateRequest request, CancellationToken cancellationToken)
    {
        var company = await dataContext.Set<Company>().FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

        if (company == null) throw new ArgumentException("NotFound");

        company.Name = request.Name;
        company.Ticker = request.Ticker;
        company.Exchange = request.Exchange;
        company.Isin = request.Isin;
        company.Website = request.Website;

        dataContext.Set<Company>().Update(company);

        await dataContext.SaveChangesAsync(cancellationToken);
    }
}
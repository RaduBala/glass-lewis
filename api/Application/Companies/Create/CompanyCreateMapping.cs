using Domain.Entities;

namespace Application.Companies.Create;

public static class CompanyCreateMapping
{
    public static Company ToEntity(this CompanyCreateRequest request)
    {
        return new Company
        {
            Id = Guid.NewGuid().ToString(),
            Name = request.Name,
            Ticker = request.Ticker,
            Isin = request.Isin,
            Exchange = request.Exchange,
            Website = request.Website
        };
    }
}

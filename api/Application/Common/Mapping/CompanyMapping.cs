using Application.Common.DTOs;
using Domain.Entities;

namespace Application.Common.Mapping;

public static class CompanyMapping
{
    public static CompanyDto ToDto(this Company entity)
    {
        return new CompanyDto()
        {
            Id = entity.Id,
            Name = entity.Name,
            Ticker = entity.Ticker,
            Isin = entity.Isin,
            Exchange = entity.Exchange,
            Website = entity.Website
        };
    }
}

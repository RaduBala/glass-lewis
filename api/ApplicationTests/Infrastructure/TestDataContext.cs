using Application.Common.DataContext;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApplicationTests.Infrastructure;

public class TestDataContext : DbContext, IDataContext
{
    public TestDataContext(DbContextOptions<TestDataContext> options) : base(options) { }

    public DbSet<Company> Companies => Set<Company>();

    public IQueryable<TEnt> ReadSet<TEnt>() where TEnt : class
    {
        return Set<TEnt>();
    }
}

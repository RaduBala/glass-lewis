using Application.Common.DTOs;
using Application.Companies.Create;
using Application.Companies.GetByIsin;
using ApplicationTests.Infrastructure;
using Domain.Entities;
using FluentAssertions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace ApplicationTests.CompanyTests
{
    public class CreateHandlerTests
    {
        private readonly TestDataContext _context;
        private readonly CompanyCreateHandler _handler;
        private readonly CompanyCreateValidator _validator;
        private readonly IMediator _mediator;

        public CreateHandlerTests()
        {
            var options = new DbContextOptionsBuilder<TestDataContext>()
                .UseInMemoryDatabase("TestDb")
                .Options;

            var mockMediator = new Moq.Mock<IMediator>();

            _context = new TestDataContext(options);
            _mediator = mockMediator.Object;
            _handler = new CompanyCreateHandler(_context);
            _validator = new CompanyCreateValidator(_mediator);

            mockMediator
               .Setup(m => m.Send(It.IsAny<CompanyGetByIsinRequest>(), It.IsAny<CancellationToken>()))
               .ReturnsAsync((CompanyGetByIsinRequest req, CancellationToken _) =>
               {
                   var company = _context.Companies.FirstOrDefault(c => c.Isin == req.Isin);

                   return company == null ? null : new CompanyDto
                   {
                       Id = company.Id,
                       Name = company.Name,
                       Exchange = company.Exchange,
                       Isin = company.Isin,
                       Ticker = company.Ticker,
                       Website = company.Website
                   };
               });
        }

        [Fact]
        public async Task ReturnId_WhenSuccessfullyCreated()
        {
            var request = new CompanyCreateRequest
            {
                Name = "Test Company",
                Ticker = "TST",
                Exchange = "NYSE",
                Isin = "US1234567890",
                Website = "Https://example"
            };

            var response = await _handler.Handle(request, CancellationToken.None);

            response.Should().NotBeNull();
            response.Id.Should().Be(response.Id);

            var addedCompany = _context.ReadSet<Company>().First(x => x.Id == response.Id);

            addedCompany.Should().NotBeNull();

            addedCompany.Name.Should().Be(request.Name);
            addedCompany.Ticker.Should().Be(request.Ticker);
            addedCompany.Exchange.Should().Be(request.Exchange);
            addedCompany.Isin.Should().Be(request.Isin);
            addedCompany.Website.Should().Be(request.Website);
        }

        [Fact]
        public async Task ReturnBadRequest_WhenIsinConflict()
        {
            var request1 = new CompanyCreateRequest
            {
                Name = "Test Company Duplicate Isin 1",
                Ticker = "TST",
                Exchange = "NYSE",
                Isin = "US1234567111",
                Website = "Https://example"
            };

            var request2 = new CompanyCreateRequest
            {
                Name = "Test Company Duplicate Isin 2",
                Ticker = "TST1",
                Exchange = "NYSE",
                Isin = "US1234567111"
            };

            var responseValidator1 = await _validator.ValidateAsync(request1);
            var responseHandler1 = await _handler.Handle(request1, CancellationToken.None);

            var responseValidator2 = await _validator.ValidateAsync(request2);

            responseValidator1.IsValid.Should().BeTrue();
            responseHandler1.Id.Should().NotBeNull();

            responseValidator2.IsValid.Should().BeFalse();
            responseValidator2.Errors.Should().ContainSingle(e => e.PropertyName == "Isin" && e.ErrorMessage.Contains("already exists"));
        }
    }
}

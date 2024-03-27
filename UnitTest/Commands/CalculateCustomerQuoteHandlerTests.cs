using Application.Customers;
using Application.Customers.CalculateCustomerQuote;
using Application.Interfaces;
using Application.Products;
using Domain.Entities.Customers;
using Domain.Entities.Products;
using Domain.Errors;
using Domain.Shared;
using FluentAssertions;
using Moq;

namespace UnitTest.Commands;
public class CalculateCustomerQuoteHandlerTests
{
    private readonly Mock<ICustomerRepository> _customerRepositoryMock;
    private readonly Mock<IProductRepository> _productRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;

    public CalculateCustomerQuoteHandlerTests()
    {
        _customerRepositoryMock = new();
        _productRepositoryMock = new();
        _unitOfWorkMock = new();
    }

    [Fact]
    public async Task Handle_Should_ReturnFailureResult_WhenCustomerOrProductNotFound()
    {
        // Arrange
        var request = new CalculateCustomerQuoteRequest(
            Guid.NewGuid(),
            Guid.NewGuid(),
            5000,
            24);

        var customers = new List<Customer>()
        {
            Customer.Create(
                Guid.NewGuid(),
                title: "Title",
                firstName: "",
                lastName: "",
                dateOfBirth: DateTime.Now,
                mobileNumber: "",
                email: "",
                redirectURL: "",
                term: "24",
                amountRequired: "500")
        };

        var products = new List<Product>()
        {
            new(
                Guid.NewGuid(),
                name: "",
                perAnnumInterestRate: 0m,
                minimumDuration: 0,
                monthsOfFreeInterest: 0,
                establishmentFee: 300)
        };

        _customerRepositoryMock.Setup(
            x => x.FindByIdAsync(
                It.IsAny<Guid>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync((Guid id, CancellationToken cancellationToken) => customers.SingleOrDefault(c => c.Id == id));

        _productRepositoryMock.Setup(
            x => x.GetByIdAsync(
                It.IsAny<Guid>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync((Guid id, CancellationToken cancellationToken) => products.FirstOrDefault(p => p.Id == id));

        var handler = new CalculateCustomerQuoteHandler(
            _customerRepositoryMock.Object,
            _productRepositoryMock.Object,
            _unitOfWorkMock.Object);

        // Act
        Result result = await handler.Handle(request, default);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.Customer.CustomerOrProductNotFound);
    }

    [Fact]
    public async Task Handle_Should_Call_UnitOfWork_When_Amount_Or_TermInMonths_Are_Updated()
    {
        // Arrange
        var customerId = Guid.Parse("4B2271B7-B352-4935-A14D-33F88F213CDE");
        var productId = Guid.Parse("32D02406-FCDE-4A94-9FB0-321713A4F6FE");
        int amountRequired = 600;
        int termInMonths = 24;

        var request = new CalculateCustomerQuoteRequest(
            customerId,
            productId,
            amountRequired,
            termInMonths);

        var customers = new List<Customer>()
        {
            Customer.Create(
                customerId,
                title: "Title",
                firstName: "",
                lastName: "",
                dateOfBirth: DateTime.Now,
                mobileNumber: "",
                email: "",
                redirectURL: "",
                term: "2",
                amountRequired: "500")
        };

        var products = new List<Product>()
        {
            new(
                productId,
                name: "",
                perAnnumInterestRate: 0m,
                minimumDuration: 0,
                monthsOfFreeInterest: 0,
                establishmentFee: 300)
        };

        _customerRepositoryMock.Setup(
            x => x.FindByIdAsync(
                It.IsAny<Guid>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync((Guid id, CancellationToken cancellationToken) => customers.SingleOrDefault(c => c.Id == id));

        _productRepositoryMock.Setup(
            x => x.GetByIdAsync(
                It.IsAny<Guid>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync((Guid id, CancellationToken cancellationToken) => products.FirstOrDefault(p => p.Id == id));

        var handler = new CalculateCustomerQuoteHandler(
            _customerRepositoryMock.Object,
            _productRepositoryMock.Object,
            _unitOfWorkMock.Object);

        // Act
        Result result = await handler.Handle(request, default);

        // Assert
        // check if the update repository is called
        _customerRepositoryMock.Verify(
            x => x.Update(It.Is<Customer>(c => c.Id == customers.First().Id)));

        // Check if the SaveChangesAsync from unit of work called. 
        // It should never call.
        _unitOfWorkMock.Verify(
            x => x.SaveChangesAsync(It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Theory]
    // done
    //[InlineData(5000, 9.2, 24, 0, 241.38, 0)]

    // some 1 to 2 number discrepancy
    [InlineData(5000, 9.2, 24, 2, 242.52, 208.33)] // Typical scenario with interest-free period

    // done
    //[InlineData(10000, 5.0, 12, 0, 881.07, 0)] // High principal amount with no interest-free period

    // not tested
    //[InlineData(1000, 10.0, 6, 4, 166.67)] // Low principal amount with interest-free period

    // done
    //[InlineData(2000, 8.0, 36, 0, 71.01, 0)] // Long term with no interest-free period
    public async Task Handle_Should_Calculate_MonthlyPayment_With_Interest_Accurately(
        int amountRequired,
        decimal perAnnumInterestRate,
        int termInMonths,
        int monthsOfFreeInterest,
        decimal expectedMonthlyRepaymentWithInterest,
        decimal expectedMonthlyRepaymentWithoutInterest)
    {
        // Arrange
        var customerId = Guid.Parse("4B2271B7-B352-4935-A14D-33F88F213CDE");
        var productId = Guid.Parse("32D02406-FCDE-4A94-9FB0-321713A4F6FE");

        var request = new CalculateCustomerQuoteRequest(
            customerId,
            productId,
            amountRequired,
            termInMonths);

        var customers = new List<Customer>()
        {
            Customer.Create(
                customerId,
                title: "Title",
                firstName: "",
                lastName: "",
                dateOfBirth: DateTime.Now,
                mobileNumber: "",
                email: "",
                redirectURL: "",
                term: "2",
                amountRequired: "5000")
        };

        var products = new List<Product>()
        {
            new(
                productId,
                name: "Product",
                perAnnumInterestRate: perAnnumInterestRate,
                minimumDuration: 0,
                monthsOfFreeInterest: monthsOfFreeInterest,
                establishmentFee: 300)
        };

        _customerRepositoryMock.Setup(
            x => x.FindByIdAsync(
                It.IsAny<Guid>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync((Guid id, CancellationToken cancellationToken) => customers.SingleOrDefault(c => c.Id == id));

        _productRepositoryMock.Setup(
            x => x.GetByIdAsync(
                It.IsAny<Guid>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync((Guid id, CancellationToken cancellationToken) => products.FirstOrDefault(p => p.Id == id));

        var handler = new CalculateCustomerQuoteHandler(
            _customerRepositoryMock.Object,
            _productRepositoryMock.Object,
            _unitOfWorkMock.Object);

        // Act
        Result<CalculateCustomerQuoteResponse> result = await handler.Handle(request, default);

        // Assert
        // Allow slight difference due to floating point precision
        // with interest months
        Assert.Equal(expectedMonthlyRepaymentWithInterest, Math.Round(result.Value.Repayment, 2), 2);

        // without interest months
        Assert.Equal(expectedMonthlyRepaymentWithoutInterest, Math.Round(result.Value.RepaymentWithoutInterest, 2), 2);

    }

}

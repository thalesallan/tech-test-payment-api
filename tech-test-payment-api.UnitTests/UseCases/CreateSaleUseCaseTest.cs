using AutoMapper;
using FluentValidation;
using Moq;
using tech_test_payment.Borders.Dtos;
using tech_test_payment.Borders.Dtos.Request;
using tech_test_payment.Borders.Entities;
using tech_test_payment.Borders.Mapper;
using tech_test_payment.Borders.Repositories;
using tech_test_payment.Borders.Validators;
using tech_test_payment.Helpers.Constants;
using tech_test_payment.Helpers.Enums;
using tech_test_payment.Helpers.ExceptionHelper;
using tech_test_payment.UnitTests.Mocks;
using tech_test_payment.UnitTests.Mocks.Dto;
using tech_test_payment.UnitTests.Mocks.Entities;
using tech_test_payment.UseCases.UseCases;
using Xunit;

namespace tech_test_payment.UnitTests.UseCases
{
    public class CreateSaleUseCaseTest
    {
        private readonly Mock<ISaleRepository> _saleRepositoryMock;
        private readonly IValidator<SaleRequest> _validatorMock;
        private readonly IMapper _mapper;

        private readonly CreateSalesUseCase useCase;

        public CreateSaleUseCaseTest()
        {
            _saleRepositoryMock = new Mock<ISaleRepository>();

            MapperConfiguration mapperConfig = new(mc =>
            {
                mc.AddProfile(new SaleMapper());
            });

            _mapper = mapperConfig.CreateMapper();

            _validatorMock = new SaleRequestValidator();

            useCase = new CreateSalesUseCase(
                _validatorMock,
                _saleRepositoryMock.Object,
                _mapper
            );
        }

        [Fact]
        public void CreateSale_Return_Success()
        {
            var request = new SaleRequestBuilder().Build();

            var sale = _saleRepositoryMock.Setup(r => r.CreateSales(It.IsAny<SaleEntitie>()))
                .ReturnsAsync(new SaleEntitie()
                {
                    SaleId = 1,
                    Date = DateTime.Now,
                    Order = 111,
                    Status = StatusEnum.AguardandoPagamento,
                    Products = new List<ProductEntitie>()
                    {
                        new ProductEntitieBuilder()
                        .WithProductId(1)
                        .WithName("Tv 32")
                        .WithDescription("LED")
                        .WithPrice(1800.00)
                        .Build(),
                        new ProductEntitieBuilder().Build()
                    },
                    Salesperson = new SalespersonEntitieBuilder().Build()
                });

            var result = useCase.Execute(request);

            _saleRepositoryMock.Verify(r => r.CreateSales(It.IsAny<SaleEntitie>()), Times.Once());

            Assert.NotNull(result);

            var product1 = request.Products!.ToList()[0];
            Assert.Equal(product1.Name, result.Result.Products!.ToList()[0].Name);
            Assert.Equal(product1.Description, result.Result.Products!.ToList()[0].Description);
            Assert.Equal(product1.Price, result.Result.Products!.ToList()[0].Price);
            Assert.Equal(1, result.Result.Products!.ToList()[0].ProductId);

            var product2 = request.Products!.ToList()[1];
            Assert.Equal(product2.Name, result.Result.Products!.ToList()[1].Name);
            Assert.Equal(product2.Description, result.Result.Products!.ToList()[1].Description);
            Assert.Equal(product2.Price, result.Result.Products!.ToList()[1].Price);
            Assert.Equal(2, result.Result.Products!.ToList()[1].ProductId);

            var person = request.Salesperson;
            Assert.Equal(person.Name, result.Result.Salesperson.Name);
            Assert.Equal(person.EmailAddress, result.Result.Salesperson.EmailAddress);
            Assert.Equal("111.222.333-44", result.Result.Salesperson.DocumentNumber);
            Assert.Equal("(11) 2 3333-4444", result.Result.Salesperson.PhoneNumber);
            Assert.Equal(1, result.Result.Salesperson.PersonId);
        }

        [Fact]
        public async Task CreateSale_Return_ExceptionAsync()
        {
            var menssageError = ErrorsConstants.REPOSITORY_CREATE_SALE;
            var request = new SaleRequestBuilder().Build();

            var sale = _saleRepositoryMock.Setup(r => r.CreateSales(It.IsAny<SaleEntitie>()))
                .ThrowsAsync(new CustonException(menssageError));

            CustonException exception = await Assert.ThrowsAsync<CustonException>(async () =>
            {
                await useCase.Execute(request);
            });

            _saleRepositoryMock.Verify(r => r.CreateSales(It.IsAny<SaleEntitie>()), Times.Once);

            Assert.Equal(ErrorsConstants.REPOSITORY_CREATE_SALE, exception.Message);
        }
    }
}
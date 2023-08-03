using AutoMapper;
using FluentValidation;
using Moq;
using tech_test_payment.Borders.Mapper;
using tech_test_payment.Borders.Repositories;
using tech_test_payment.Borders.Validators;
using tech_test_payment.Helpers.Constants;
using tech_test_payment.Helpers.ExceptionHelper;
using tech_test_payment.UnitTests.Mocks.Entities;
using tech_test_payment.UseCases.UseCases;
using Xunit;

namespace tech_test_payment.UnitTests.UseCases
{
    public class GetSaleUseCaseTest
    {
        private readonly Mock<ISaleRepository> _saleRepositoryMock;
        private readonly IValidator<int> _validatorMock;
        private readonly IMapper _mapper;

        private readonly GetSalesUseCase useCase;

        public GetSaleUseCaseTest()
        {
            _saleRepositoryMock = new Mock<ISaleRepository>();

            MapperConfiguration mapperConfig = new(mc =>
            {
                mc.AddProfile(new SaleMapper());
            });

            _mapper = mapperConfig.CreateMapper();

            _validatorMock = new IdValidator();

            useCase = new GetSalesUseCase(
                _validatorMock,
                _saleRepositoryMock.Object,
                _mapper
            );
        }

        [Fact]
        public void GetSale_Return_Success()
        {
            var request = 12;

            var repositoryResponse = new SaleEntitieBuilder().WithSaleId(12).Build();

            var sale = _saleRepositoryMock.Setup(r => r.GetForSalesId(It.IsAny<int>()))
                .ReturnsAsync(repositoryResponse);

            var result = useCase.Execute(request);

            _saleRepositoryMock.Verify(r => r.GetForSalesId(It.IsAny<int>()), Times.Once());

            Assert.NotNull(result);
            Assert.Equal(request, result.Result.SaleId);
        }

        [Fact]
        public async Task GetSale_Return_ExceptionAsync()
        {
            var menssageError = ErrorsConstants.REPOSITORY_GET_SALE;
            var request = 12;

            var sale = _saleRepositoryMock.Setup(r => r.GetForSalesId(It.IsAny<int>()))
                .ThrowsAsync(new CustonException(menssageError));

            CustonException exception = await Assert.ThrowsAsync<CustonException>(async () =>
            {
                await useCase.Execute(request);
            });

            _saleRepositoryMock.Verify(r => r.GetForSalesId(It.IsAny<int>()), Times.Once);

            Assert.Equal(ErrorsConstants.REPOSITORY_GET_SALE, exception.Message);
        }
    }
}

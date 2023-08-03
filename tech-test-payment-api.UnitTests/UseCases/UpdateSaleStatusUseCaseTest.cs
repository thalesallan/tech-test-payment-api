using FluentValidation;
using Moq;
using tech_test_payment.Borders.Entities;
using tech_test_payment.Borders.Repositories;
using tech_test_payment.Borders.Validators;
using tech_test_payment.Helpers;
using tech_test_payment.Helpers.Constants;
using tech_test_payment.Helpers.Enums;
using tech_test_payment.Helpers.ExceptionHelper;
using tech_test_payment.UnitTests.Mocks.Entities;
using tech_test_payment.UseCases.UseCases;
using Xunit;

namespace tech_test_payment.UnitTests.UseCases
{
    public class UpdateSaleStatusUseCaseTest
    {
        private readonly Mock<ISaleRepository> _saleRepositoryMock;
        private readonly IValidator<StatusEnum> _validatorMock;

        private readonly UpdadeStatusUseCase useCase;

        public UpdateSaleStatusUseCaseTest()
        {
            _saleRepositoryMock = new Mock<ISaleRepository>();

            _validatorMock = new StatusEnumValidator();

            useCase = new UpdadeStatusUseCase(
                _validatorMock,
                _saleRepositoryMock.Object
            );
        }

        [Theory]
        [InlineData(12, StatusEnum.PagamentoAprovado, StatusEnum.AguardandoPagamento)]
        [InlineData(12, StatusEnum.Cancelada, StatusEnum.AguardandoPagamento)]
        [InlineData(12, StatusEnum.EnviadoParaTransportadora, StatusEnum.PagamentoAprovado)]
        [InlineData(12, StatusEnum.Cancelada, StatusEnum.PagamentoAprovado)]
        [InlineData(12, StatusEnum.Entregue, StatusEnum.EnviadoParaTransportadora)]
        public void UpdateSaleStatus_Return_OK(int saleId, StatusEnum statusRequest, StatusEnum actualStatus )
        {
            var repositoryResponse = new SaleEntitieBuilder().WithSaleId(12).Build();

            var ActualSale = new SaleEntitieBuilder().WithSaleId(12).WithStatus(actualStatus).Build();

            _saleRepositoryMock.Setup(r => r.GetForSalesId(It.IsAny<int>()))
                .ReturnsAsync(ActualSale);

            _saleRepositoryMock.Setup(r => r.UpdateStatus(It.IsAny<int>(), It.IsAny<SaleEntitie>()))
                .ReturnsAsync(true);

            var result = useCase.Execute(saleId, statusRequest);

            _saleRepositoryMock.Verify(r => r.GetForSalesId(It.IsAny<int>()), Times.Once());
            _saleRepositoryMock.Verify(r => r.UpdateStatus(It.IsAny<int>(), It.IsAny<SaleEntitie>()), Times.Once());

            Assert.True(result.Result);
            Assert.Null(result.Exception);
        }

        [Theory]
        [InlineData(12, StatusEnum.EnviadoParaTransportadora, StatusEnum.AguardandoPagamento)]
        [InlineData(12, StatusEnum.Entregue, StatusEnum.AguardandoPagamento)]
        [InlineData(12, StatusEnum.Entregue, StatusEnum.PagamentoAprovado)]       
        public async Task UpdateSaleStatus_Return_ValidateStatusAsync(int saleId, StatusEnum statusRequest, StatusEnum actualStatus)
        {
            var errors = string.Format(ErrorsConstants.STATUS_VALIDATION_ERROR, 
                GetStatusStringHelper.GetStringStatus(statusRequest),
                GetStatusStringHelper.GetStringStatus(actualStatus));

            var repositoryResponse = new SaleEntitieBuilder().WithSaleId(12).Build();

            var ActualSale = new SaleEntitieBuilder().WithSaleId(12).WithStatus(actualStatus).Build();

            _saleRepositoryMock.Setup(r => r.GetForSalesId(It.IsAny<int>()))
                .ReturnsAsync(ActualSale);

            _saleRepositoryMock.Setup(r => r.UpdateStatus(It.IsAny<int>(), It.IsAny<SaleEntitie>()))
                .ReturnsAsync(true);

            CustonException exception = await Assert.ThrowsAsync<CustonException>(async () =>
            {
                await useCase.Execute(saleId, statusRequest);
            });

            _saleRepositoryMock.Verify(r => r.GetForSalesId(It.IsAny<int>()), Times.Once());
            _saleRepositoryMock.Verify(r => r.UpdateStatus(It.IsAny<int>(), It.IsAny<SaleEntitie>()), Times.Never());

            Assert.Equal(errors, exception.Message);
        }


        [Theory]
        [InlineData(12, StatusEnum.AguardandoPagamento, StatusEnum.Cancelada)]
        [InlineData(12, StatusEnum.PagamentoAprovado, StatusEnum.Cancelada)]
        [InlineData(12, StatusEnum.EnviadoParaTransportadora, StatusEnum.Cancelada)]
        [InlineData(12, StatusEnum.Entregue, StatusEnum.Cancelada)]
        public async Task UpdateSaleStatus_Return_ValidateStatus_With_StatusCanceledAsync(int saleId, StatusEnum statusRequest, StatusEnum actualStatus)
        {
            var errors = string.Format(ErrorsConstants.STATUS_CANCELED_VALIDATION_ERROR,
                GetStatusStringHelper.GetStringStatus(statusRequest),
                GetStatusStringHelper.GetStringStatus(actualStatus));

            var repositoryResponse = new SaleEntitieBuilder().WithSaleId(12).Build();

            var ActualSale = new SaleEntitieBuilder().WithSaleId(12).WithStatus(actualStatus).Build();

            _saleRepositoryMock.Setup(r => r.GetForSalesId(It.IsAny<int>()))
                .ReturnsAsync(ActualSale);

            _saleRepositoryMock.Setup(r => r.UpdateStatus(It.IsAny<int>(), It.IsAny<SaleEntitie>()))
                .ReturnsAsync(true);

            CustonException exception = await Assert.ThrowsAsync<CustonException>(async () =>
            {
                await useCase.Execute(saleId, statusRequest);
            });

            _saleRepositoryMock.Verify(r => r.GetForSalesId(It.IsAny<int>()), Times.Once());
            _saleRepositoryMock.Verify(r => r.UpdateStatus(It.IsAny<int>(), It.IsAny<SaleEntitie>()), Times.Never());

            Assert.Equal(errors, exception.Message);
        }

        [Fact]
        public async Task UpdateSaleStatus_Return_ExceptionAsync()
        {
            int saleId = 12;
            StatusEnum status = StatusEnum.PagamentoAprovado;

            var ActualSale = new SaleEntitieBuilder().Build();

            var menssageError = ErrorsConstants.REPOSITORY_UPDATE_SALE_STATUS + " - " + ActualSale.Order.ToString();
            
            _saleRepositoryMock.Setup(r => r.GetForSalesId(It.IsAny<int>()))
                .ReturnsAsync(ActualSale);

            var sale = _saleRepositoryMock.Setup(r => r.UpdateStatus(It.IsAny<int>(), It.IsAny<SaleEntitie>()))
                .ThrowsAsync(new Exception(menssageError));

            Exception exception = await Assert.ThrowsAsync<Exception>(async () =>
            {
                await useCase.Execute(saleId, status);
            });

            _saleRepositoryMock.Verify(r => r.GetForSalesId(It.IsAny<int>()), Times.Once);

            Assert.Equal(menssageError, exception.Message);
        }
    }
}

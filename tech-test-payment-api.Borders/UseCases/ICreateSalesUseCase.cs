using tech_test_payment.Borders.Dtos.Request;
using tech_test_payment.Borders.Dtos.Response;

namespace tech_test_payment.Borders.UseCases
{
    public interface ICreateSalesUseCase
    {
        Task<SaleResponse> Execute(SaleRequest request);
    }
}

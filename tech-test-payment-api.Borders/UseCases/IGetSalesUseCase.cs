using tech_test_payment.Borders.Dtos.Response;

namespace tech_test_payment.Borders.UseCases
{
    public interface IGetSalesUseCase
    {
        Task<SaleResponse> Execute(int request);
    }
}

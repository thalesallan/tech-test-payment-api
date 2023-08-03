using tech_test_payment.Helpers.Enums;

namespace tech_test_payment.Borders.UseCases
{
    public interface IUpdateStatusUseCase
    {
        Task<bool> Execute(int saleId, StatusEnum status);
    }
}

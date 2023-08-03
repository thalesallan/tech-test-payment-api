using tech_test_payment.Borders.Entities;

namespace tech_test_payment.Borders.Repositories
{
    public interface ISaleRepository
    {
        Task<SaleEntitie> CreateSales(SaleEntitie entitie);
        Task<SaleEntitie> GetForSalesId(int salesId);
        Task<bool> UpdateStatus(int salesId, SaleEntitie entitie);
    }
}

using tech_test_payment.Borders.Entities;
using tech_test_payment.Borders.Repositories;
using tech_test_payment.Helpers.Constants;
using tech_test_payment.Helpers.ExceptionHelper;

namespace tech_test_payment.Repositories.Repositories
{
    public class SalesRepository : ISaleRepository
    {

        public SalesRepository() { }

        private readonly List<SaleEntitie> saleEntitieList = new();

        public async Task<SaleEntitie> CreateSales(SaleEntitie entitie)
        {
            try
            {
                await Task.Delay(2000);

                var newSales = new SaleEntitie
                {
                    SaleId = entitie.SaleId,
                    Products = entitie.Products,
                    Salesperson = entitie.Salesperson,
                    Status = entitie.Status,
                    Date = entitie.Date,
                    Order = entitie.Order
                };

                saleEntitieList.Add(newSales);

                return await Task.FromResult(newSales);
            }
            catch (CustonException ex)
            {
                Console.WriteLine(ex.Message);
                throw new CustonException(ErrorsConstants.REPOSITORY_CREATE_SALE, ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<SaleEntitie> GetForSalesId(int salesId)
        {
            try
            {
                await Task.Delay(2000);

                var result = saleEntitieList.Find(x => x.SaleId == salesId);

                SaleEntitie? saleEntitie = result ?? null;

                return saleEntitie ?? throw new CustonException(ErrorsConstants.REPOSITORY_GET_SALE);
            }
            catch (CustonException ex)
            {
                Console.WriteLine(ex.Message);
                throw new CustonException(ErrorsConstants.REPOSITORY_GET_SALE, ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<bool> UpdateStatus(int salesId, SaleEntitie saleEntitie)
        {
            try
            {
                await Task.Delay(2000);

                var beforeEntitie = saleEntitieList.Find(x => x.SaleId == salesId);

                saleEntitieList.RemoveAll(sale => sale.SaleId == salesId);

                var afterEntitie = new SaleEntitie
                {
                    SaleId = saleEntitie.SaleId,
                    Products = saleEntitie.Products,
                    Salesperson = saleEntitie.Salesperson,
                    Status = saleEntitie.Status,
                    Date = saleEntitie.Date,
                    Order = saleEntitie.Order
                };

                if (saleEntitieList.FindAll(x => x.SaleId == salesId).Equals(0))
                {
                    throw new CustonException(ErrorsConstants.REPOSITORY_UPDATE_SALE_STATUS + " - " + afterEntitie.Order.ToString());
                }

                saleEntitieList.Add(afterEntitie);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException.Message);
                throw;
            }
        }
    }
}

using tech_test_payment.Borders.Entities;
using tech_test_payment.Helpers.Enums;

namespace tech_test_payment.UnitTests.Mocks.Entities
{
    public class SaleEntitieBuilder
    {
        private SaleEntitie saleEntitie;

        public SaleEntitieBuilder()
        {
            saleEntitie = new SaleEntitie
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
            };
        }

        public SaleEntitieBuilder WithSaleId(int id)
        {
            saleEntitie = saleEntitie with { SaleId = id };
            return this;
        }

        public SaleEntitieBuilder WithStatus(StatusEnum status)
        {
            saleEntitie = saleEntitie with { Status = status };
            return this;
        }

        public SaleEntitieBuilder WithoutProduct()
        {
            saleEntitie = saleEntitie with { Products = new List<ProductEntitie>() { } };
            return this;
        }

        public SaleEntitieBuilder WithoutSalesperson()
        {
            saleEntitie = saleEntitie with { Salesperson = new SalespersonEntitie() { } };
            return this;
        }

        public SaleEntitie Build()
        {
            return saleEntitie;
        }
    }
}

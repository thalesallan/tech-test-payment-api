using tech_test_payment.Borders.Dtos;
using tech_test_payment.Borders.Dtos.Request;
using tech_test_payment.UnitTests.Mocks.Dto;

namespace tech_test_payment.UnitTests.Mocks
{
    public class SaleRequestBuilder
    {
        private SaleRequest saleRequest;

        public SaleRequestBuilder()
        {
            saleRequest = new SaleRequest
            {
                Products = new List<ProductDto>()
                {
                    new ProductDtoBuilder()
                        .WithName("Tv 32")
                        .WithDescription("LED")
                        .WithPrice(1800.00)
                        .Build(),
                    new ProductDtoBuilder().Build()
                },
                Salesperson = new SalespersonDtoBuilder().Build()
            };
        }

        public SaleRequestBuilder WithoutProduct()
        {
            saleRequest = saleRequest with { Products = new List<ProductDto>() { } };
            return this;
        }

        public SaleRequestBuilder WithoutSalesperson()
        {
            saleRequest = saleRequest with { Salesperson = new SalespersonDto() { } };
            return this;
        }

        public SaleRequest Build()
        {
            return saleRequest;
        }
    }
}

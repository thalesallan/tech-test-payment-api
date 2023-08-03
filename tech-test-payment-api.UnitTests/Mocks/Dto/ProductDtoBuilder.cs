using tech_test_payment.Borders.Dtos;

namespace tech_test_payment.UnitTests.Mocks.Dto
{
    public class ProductDtoBuilder
    {
        private ProductDto productDto;

        public ProductDtoBuilder()
        {
            productDto = new ProductDto
            {
                Name = "TV 55",
                Description = "Smart tv",
                Price = 3200.00
            };
        }

        public ProductDtoBuilder WithoutProduct()
        {
            productDto = productDto with { };
            return this;
        }
        
        public ProductDtoBuilder WithName(string name)
        {
            productDto = productDto with { Name = name};
            return this;
        }

        public ProductDtoBuilder WithDescription(string description)
        {
            productDto = productDto with { Description = description };
            return this;
        }

        public ProductDtoBuilder WithPrice(double price)
        {
            productDto = productDto with { Price = price };
            return this;
        }

        public ProductDto Build()
        {
            return productDto;
        }
    }
}

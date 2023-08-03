using tech_test_payment.Borders.Entities;

namespace tech_test_payment.UnitTests.Mocks.Entities
{
    public class ProductEntitieBuilder
    {
        private ProductEntitie productEntitie;

        public ProductEntitieBuilder() 
        {
            productEntitie = new ProductEntitie
            {
                ProductId = 2,
                Name = "TV 55",
                Description = "Smart tv",
                Price = 3200.00
            };
        }

        public ProductEntitieBuilder WithoutProduct()
        {
            productEntitie = productEntitie with { };
            return this;
        }

        public ProductEntitieBuilder WithProductId(int id)
        {
            productEntitie = productEntitie with { ProductId = id };
            return this;
        }

        public ProductEntitieBuilder WithName(string name)
        {
            productEntitie = productEntitie with { Name = name };
            return this;
        }

        public ProductEntitieBuilder WithDescription(string description)
        {
            productEntitie = productEntitie with { Description = description };
            return this;
        }

        public ProductEntitieBuilder WithPrice(double price)
        {
            productEntitie = productEntitie with { Price = price };
            return this;
        }

        public ProductEntitie Build()
        {
            return productEntitie;
        }
    }
}

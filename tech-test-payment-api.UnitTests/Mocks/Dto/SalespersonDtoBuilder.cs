using tech_test_payment.Borders.Dtos;

namespace tech_test_payment.UnitTests.Mocks.Dto
{
    public class SalespersonDtoBuilder
    {
        private SalespersonDto salespersonDto;

        public SalespersonDtoBuilder()
        {
            salespersonDto = new SalespersonDto
            {
                Name = "Vendedor Mock",
                DocumentNumber = "11122233344",
                EmailAddress = "email@mock.com.br",
                PhoneNumber = "11233334444"
            };
        }

        public SalespersonDtoBuilder WithoutSalesperson()
        {
            this.salespersonDto = salespersonDto with { };
            return this;
        }

        public SalespersonDto Build()
        {
            return salespersonDto;
        }
    }
}

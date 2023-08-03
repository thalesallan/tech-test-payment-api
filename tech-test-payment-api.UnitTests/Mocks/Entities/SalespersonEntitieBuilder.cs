using tech_test_payment.Borders.Entities;

namespace tech_test_payment.UnitTests.Mocks.Entities
{
    public class SalespersonEntitieBuilder
    {
        private SalespersonEntitie salespersonEntitie;

        public SalespersonEntitieBuilder()
        {
            salespersonEntitie = new SalespersonEntitie()
            {
                PersonId = 1,
                Name = "Vendedor Mock",
                DocumentNumber = "11122233344",
                EmailAddress = "email@mock.com.br",
                PhoneNumber = "11233334444"
            };
        }

        public SalespersonEntitieBuilder WithoutSalesperson()
        {
            this.salespersonEntitie = salespersonEntitie with { };
            return this;
        }

        public SalespersonEntitie Build()
        {
            return salespersonEntitie;
        }
    }
}

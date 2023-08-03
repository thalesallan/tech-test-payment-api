using tech_test_payment.Helpers.Enums;

namespace tech_test_payment.Borders.Entities
{
    public record SaleEntitie
    {
        public int SaleId { get; init; }
        public int Order { get; init; }
        public StatusEnum Status { get; init; }
        public DateTime Date { get; init; }
        public IEnumerable<ProductEntitie>? Products { get; init; }
        public SalespersonEntitie? Salesperson { get; init; }
    }
}

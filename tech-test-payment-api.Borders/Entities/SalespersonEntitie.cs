namespace tech_test_payment.Borders.Entities
{
    public record SalespersonEntitie
    {
        public int PersonId { get; init; }
        public string? Name { get; init; }
        public string? DocumentNumber { get; init; }
        public string? EmailAddress { get; init; }
        public string? PhoneNumber { get; init; }
    }
}

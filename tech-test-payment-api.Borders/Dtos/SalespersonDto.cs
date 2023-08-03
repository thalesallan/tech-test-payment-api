namespace tech_test_payment.Borders.Dtos
{
    public record SalespersonDto
    {
        public string? Name { get; init; }
        public string? DocumentNumber { get; init; }
        public string? EmailAddress { get; init; }
        public string? PhoneNumber { get; init; }
    }
}

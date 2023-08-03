namespace tech_test_payment.Borders.Dtos.Request
{
    public record SaleRequest
    {
        public IEnumerable<ProductDto>? Products { get; init; }
        public SalespersonDto? Salesperson { get; init; }
    }
}

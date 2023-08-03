namespace tech_test_payment.Borders.Dtos.Response
{
    public record SaleResponse
    {
        public int SaleId { get; init; }
        public int Order { get; init; }
        public string? Status { get; init; }
        public string? Date { get; init; }
        public IEnumerable<ProductResponse>? Products { get; init; }
        public PersonResponse? Salesperson { get; init; }
    }
}
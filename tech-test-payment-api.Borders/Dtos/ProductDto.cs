namespace tech_test_payment.Borders.Dtos
{
    public record ProductDto
    {
        public string? Name { get; init; }
        public string? Description { get; init; }
        public double Price { get; init; }
    }
}

namespace tech_test_payment.Borders.Entities
{
    public record ProductEntitie
    {
        public int ProductId { get; init; }
        public string? Name { get; init; }
        public string? Description { get; init; }
        public double Price { get; init; }
    }
}

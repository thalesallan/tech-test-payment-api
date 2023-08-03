namespace tech_test_payment.Borders.Dtos.Response
{
    public record PersonResponse : SalespersonDto
    {
        public int PersonId { get; init; }
    }
}

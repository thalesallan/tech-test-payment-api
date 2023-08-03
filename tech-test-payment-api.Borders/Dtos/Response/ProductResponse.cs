using tech_test_payment.Borders.Dtos;

namespace tech_test_payment.Borders.Dtos.Response
{
    public record ProductResponse : ProductDto
    {
        public int ProductId { get; init; }
    }
}

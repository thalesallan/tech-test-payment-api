using FluentValidation;
using tech_test_payment.Borders.Dtos.Request;
using tech_test_payment.Helpers.Constants;

namespace tech_test_payment.Borders.Validators
{
    public class SaleRequestValidator : AbstractValidator<SaleRequest>
    {
        public SaleRequestValidator()
        {
            RuleFor(sale => sale.Products)
                .NotEmpty()
                .WithMessage(ErrorsConstants.VALIDATION_PRODUCT)
                .NotNull()
                .WithMessage("Products cannot be null");

            When(sale => sale.Products != null, () =>
            {
                RuleForEach(payment => payment.Products)
                    .SetValidator(new ProductDtoValidator());
            });
            
            When(sale => sale.Salesperson != null, () =>
            {
                RuleFor(sale => sale.Salesperson)
                .SetValidator(new SalespersonDtoValidator()!);
            });

            RuleFor(sale => sale.Salesperson)
                .NotNull()
                .WithMessage(ErrorsConstants.VALIDATION_SALLER);
        }
    }
}

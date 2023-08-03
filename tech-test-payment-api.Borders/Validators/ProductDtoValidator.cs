using FluentValidation;
using tech_test_payment.Borders.Dtos;
using tech_test_payment.Helpers.Constants;

namespace tech_test_payment.Borders.Validators
{
    public class ProductDtoValidator : AbstractValidator<ProductDto>
    {
        public ProductDtoValidator()
        {
            RuleFor(product => product.Name)
                .NotEmpty()
                .WithMessage(ErrorsConstants.VALIDATION_NAME);
            RuleFor(product => product.Description)
                .NotEmpty()
                .WithMessage(ErrorsConstants.VALIDATION_DESCRIPTION);
            RuleFor(product => product.Price)
                .Must(price => price > 0)
                .WithMessage(ErrorsConstants.VALIDATION_PRICE);
        }
    }
}
using FluentValidation;
using tech_test_payment.Helpers.Constants;

namespace tech_test_payment.Borders.Validators
{
    public class IdValidator : AbstractValidator<int>
    {
        public IdValidator() 
        {
            RuleFor(id => id)
                .Must(id => id > 0)
                .WithMessage(ErrorsConstants.VALIDATION_ID);
        }
    }
}

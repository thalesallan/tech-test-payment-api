using FluentValidation;
using tech_test_payment.Helpers.Constants;
using tech_test_payment.Helpers.Enums;

namespace tech_test_payment.Borders.Validators
{
    public class StatusEnumValidator : AbstractValidator<StatusEnum>
    {
        public StatusEnumValidator()
        {
            RuleFor(Status => Status)
                .IsInEnum()
                .WithMessage(ErrorsConstants.VALIDATION_STATUS);
        }
    }
}
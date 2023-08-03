using FluentValidation;
using tech_test_payment.Borders.Dtos;
using tech_test_payment.Helpers.Constants;

namespace tech_test_payment.Borders.Validators
{
    public class SalespersonDtoValidator : AbstractValidator<SalespersonDto>
    {
        public SalespersonDtoValidator()
        {
            RuleFor(person => person.Name)
                .NotEmpty()
                .WithMessage(ErrorsConstants.VALIDATION_NAME);
            RuleFor(person => person.DocumentNumber)
                .NotEmpty()
                .WithMessage(ErrorsConstants.VALIDATION_IDENTIFICATION_NUMBER)
                .MinimumLength(11)
                .WithMessage(ErrorsConstants.VALIDATION_IDENTIFICATION_NUMBER_INVALID)
                .MaximumLength(11)
                .WithMessage(ErrorsConstants.VALIDATION_IDENTIFICATION_NUMBER_INVALID)
                .Matches("^[0-9]+$")
                .WithMessage(ErrorsConstants.VALIDATION_IDENTIFICATION_NUMBER_INVALID);
            RuleFor(person => person.EmailAddress)
                .NotEmpty()
                .WithMessage(ErrorsConstants.VALIDATION_EMAIL)
                .EmailAddress()
                .WithMessage(ErrorsConstants.VALIDATION_EMAIL_INVALID);
            RuleFor(person => person.PhoneNumber)
                .NotEmpty()
                .WithMessage(ErrorsConstants.VALIDATION_PHONE_NUMBER)
                .MinimumLength(11)
                .WithMessage(ErrorsConstants.VALIDATION_PHONE_NUMBER_INVALID);
        }
    }
}
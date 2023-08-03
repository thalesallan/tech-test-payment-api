using FluentValidation;
using FluentValidation.Results;
using tech_test_payment.Borders.Dtos;
using tech_test_payment.Borders.Dtos.Request;
using tech_test_payment.Borders.Validators;
using tech_test_payment.Helpers.Constants;
using Xunit;

namespace tech_test_payment.UnitTests.Validators
{
    public class ValidatorsTest
    {
        private readonly IValidator<SaleRequest> _validatorMock;

        public ValidatorsTest()
        {
            _validatorMock = new SaleRequestValidator();
        }

        [Fact]
        public void ProductValidator_Validate_Product_Empty()
        {
            var request = new SaleRequest
            {
                Products = new List<ProductDto>() { },
                Salesperson = new SalespersonDto
                {
                    Name = "Vendedor Mock",
                    DocumentNumber = "11122233344",
                    EmailAddress = "email@mock.com.br",
                    PhoneNumber = "11233334444"
                }
            };

            ValidationResult validationResult = _validatorMock.Validate(request);

            Assert.False(validationResult.IsValid);
            Assert.Equal(ErrorsConstants.VALIDATION_PRODUCT, validationResult.Errors[0].ErrorMessage);
        }

        [Theory]
        [InlineData("", "Smart tv", 1800.00)]
        [InlineData("TV 32", "", 1800.00)]
        [InlineData("TV 32", "Smart tv", 0.0)]
        public void ProductValidator_Validate_Product_Velues(string name, string description, int price)
        {
            var request = new SaleRequest
            {
                Products = new List<ProductDto>()
                {
                     new ProductDto
                    {
                        Name = name,
                        Description = description,
                        Price = price
                    },
                },
                Salesperson = new SalespersonDto
                {
                    Name = "Vendedor Mock",
                    DocumentNumber = "11122233344",
                    EmailAddress = "email@mock.com.br",
                    PhoneNumber = "11233334444"
                }
            };

            ValidationResult validationResult = _validatorMock.Validate(request);

            Assert.False(validationResult.IsValid);

            if (name.Equals(""))
            {
                Assert.Equal(ErrorsConstants.VALIDATION_NAME, validationResult.Errors[0].ErrorMessage);
            }
            else if (description.Equals(""))
            {
                Assert.Equal(ErrorsConstants.VALIDATION_DESCRIPTION, validationResult.Errors[0].ErrorMessage);
            }
            else
            {
                Assert.Equal(ErrorsConstants.VALIDATION_PRICE, validationResult.Errors[0].ErrorMessage);
            }
        }

        [Fact]
        public void PersonValidator_Validate_Person_Null()
        {
            var request = new SaleRequest
            {
                Products = new List<ProductDto>()
                {
                    new ProductDto
                    {
                        Name = "TV 32",
                        Description = "Smart tv",
                        Price = 1800.00
                    }
                },

            };

            ValidationResult validationResult = _validatorMock.Validate(request);

            Assert.False(validationResult.IsValid);
            Assert.Equal(ErrorsConstants.VALIDATION_SALLER, validationResult.Errors[0].ErrorMessage);
        }

        [Theory]
        [InlineData("", "11122233344", "email@mock.com.br", "11233334444")]
        [InlineData("Vendedor Mock", "", "email@mock.com.br", "11233334444")]
        [InlineData("Vendedor Mock", "aaaaaaaaaaa", "email@mock.com.br", "11233334444")]
        [InlineData("Vendedor Mock", "11122233344", "", "11233334444")]
        [InlineData("Vendedor Mock", "11122233344", "@mock.com.br", "11233334444")]
        [InlineData("Vendedor Mock", "11122233344", "email@mock.com.br", "")]
        [InlineData("Vendedor Mock", "11122233344", "email@mock.com.br", "112333344")]
        public void PersonValidator_Validate_Person_Values(string name, string identificationNumber, string email, string phoneNumber)
        {
            var request = new SaleRequest
            {
                Products = new List<ProductDto>()
                {
                    new ProductDto
                    {
                        Name = "TV 32",
                        Description = "Smart tv",
                        Price = 1800.00
                    }
                },
                Salesperson = new SalespersonDto
                {
                    Name = name,
                    DocumentNumber = identificationNumber,
                    EmailAddress = email,
                    PhoneNumber = phoneNumber
                }

            };

            ValidationResult validationResult = _validatorMock.Validate(request);

            Assert.False(validationResult.IsValid);

            if (name.Equals(""))
            {
                Assert.Equal(ErrorsConstants.VALIDATION_NAME, validationResult.Errors[0].ErrorMessage);
            }
            else if (identificationNumber.Equals(""))
            {
                Assert.Equal(ErrorsConstants.VALIDATION_IDENTIFICATION_NUMBER, validationResult.Errors[0].ErrorMessage);
            }
            else if (identificationNumber.Equals("aaaaaaaaaaa"))
            {
                Assert.Equal(ErrorsConstants.VALIDATION_IDENTIFICATION_NUMBER_INVALID, validationResult.Errors[0].ErrorMessage);
            }
            else if (email.Equals(""))
            {
                Assert.Equal(ErrorsConstants.VALIDATION_EMAIL, validationResult.Errors[0].ErrorMessage);
            }
            else if (email.Equals("@mock.com.br"))
            {
                Assert.Equal(ErrorsConstants.VALIDATION_EMAIL_INVALID, validationResult.Errors[0].ErrorMessage);
            }
            else if (phoneNumber.Equals(""))
            {
                Assert.Equal(ErrorsConstants.VALIDATION_PHONE_NUMBER, validationResult.Errors[0].ErrorMessage);
            }
            else
            {
                Assert.Equal(ErrorsConstants.VALIDATION_PHONE_NUMBER_INVALID, validationResult.Errors[0].ErrorMessage);
            }
        }
    }
}

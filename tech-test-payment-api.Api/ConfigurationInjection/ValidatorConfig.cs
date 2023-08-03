using FluentValidation;
using tech_test_payment.Borders.Dtos;
using tech_test_payment.Borders.Dtos.Request;
using tech_test_payment.Borders.Validators;
using tech_test_payment.Helpers.Enums;

namespace tech_test_payment.Api.ConfigurationInjection
{
    public class ValidatorConfig
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IValidator<SalespersonDto>, SalespersonDtoValidator>();
            services.AddTransient<IValidator<ProductDto>, ProductDtoValidator>();
            services.AddTransient<IValidator<SaleRequest>, SaleRequestValidator>();
            services.AddTransient<IValidator<StatusEnum>, StatusEnumValidator>();
            services.AddTransient<IValidator<int>, IdValidator>();
        }
    }
}

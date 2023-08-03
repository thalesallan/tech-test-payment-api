using tech_test_payment.Borders.UseCases;
using tech_test_payment.UseCases.UseCases;

namespace tech_test_payment.Api.ConfigurationInjection
{
    public class UseCaseConfig
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<ICreateSalesUseCase, CreateSalesUseCase>();
            services.AddScoped<IGetSalesUseCase, GetSalesUseCase>();
            services.AddScoped<IUpdateStatusUseCase, UpdadeStatusUseCase>();
        }
    }
}

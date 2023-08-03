using tech_test_payment.Borders.Mapper;

namespace tech_test_payment.Api.ConfigurationInjection
{
    public class AutoMapperConfig
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(SaleMapper));
        }
    }
}

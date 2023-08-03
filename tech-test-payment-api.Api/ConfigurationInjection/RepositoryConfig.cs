using tech_test_payment.Borders.Repositories;
using tech_test_payment.Repositories.Repositories;

namespace tech_test_payment.Api.ConfigurationInjection
{
    public class RepositoryConfig
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ISaleRepository, SalesRepository>();
        }
    }
}

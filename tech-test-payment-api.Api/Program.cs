using System.Text.Json.Serialization;
using tech_test_payment.Api.ConfigurationInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Dependency Injection
var services = builder.Services;
UseCaseConfig.ConfigureServices(services);
RepositoryConfig.ConfigureServices(services);
ValidatorConfig.ConfigureServices(services);
AutoMapperConfig.ConfigureServices(services);


// Configure SerializerOptions
builder.Services.AddControllers()
   .AddJsonOptions(options =>
   {
       options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
   });

// Register Swagger generator
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();

// Configure the swagger
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Tech test payment API v1");
});

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
   name: "default",
   pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

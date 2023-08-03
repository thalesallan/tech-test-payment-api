using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;
using tech_test_payment.Api.ConfigurationInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Dependecy Injection
var services = builder.Services;
UseCaseConfig.ConfigureServices(services);
RepositoryConfig.ConfigureServices(services);
ValidatorConfig.ConfigureServices(services);
AutoMapperConfig.ConfigureServices(services);

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Tech test payment api", Version = "v1" });
});

// Configure SerializerOptions
builder.Services.AddControllers()
    .AddJsonOptions(options => { 
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); 
    });

var app = builder.Build();

app.UseSwagger();

// Configure the swagger
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Tech test payment API v1");
});


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

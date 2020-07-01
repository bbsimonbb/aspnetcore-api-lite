using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Json;
using aspnetcore_api_lite.Services;

namespace aspnetcore_api_lite
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<CustomerService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/customers/{id}", async context =>
                {
                    var countryService = context.Request.HttpContext.RequestServices.GetRequiredService<CustomerService>();

                    var id = (string)context.Request.RouteValues["id"];
                    var country = countryService.Get(id);
                    var response = country != null ? JsonSerializer.Serialize(country) : "No country found";

                    await context.Response.WriteAsync(response);
                });

                endpoints.MapGet("/customers", async context =>
                {
                    var countryService = context.Request.HttpContext.RequestServices.GetRequiredService<CustomerService>();

                    var countries = countryService.Get();
                    var response = JsonSerializer.Serialize(countries);

                    await context.Response.WriteAsync(response);
                });
            });
        }
    }
}
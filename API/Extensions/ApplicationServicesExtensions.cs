using System.Linq;
using API.Errors;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped(typeof(IGenericRepository<>), (typeof(GenericRepository<>)));
            services.Configure<ApiBehaviorOptions>(options =>                       // Getting access to its options
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errors = actionContext.ModelState
                        .Where(e => e.Value.Errors.Count > 0)// checks to see if theres any errors by counting if number of error is greater than zero
                        .SelectMany(x => x.Value.Errors)// Selects all the error messages
                        .Select(x => x.ErrorMessage).ToArray();//populates them into an array

                    var errorResponse = new ApiValidationErrorResponse
                    {                                           // initialize the ApiValidationErrorResponse class to populate the Errors property inside ApiValidationErrorResponse class
                        Errors = errors
                    };
                    return new BadRequestObjectResult(errorResponse);
                };

            });
            return services;
        }
    }
}
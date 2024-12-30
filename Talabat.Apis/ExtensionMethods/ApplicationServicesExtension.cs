using Microsoft.AspNetCore.Mvc;
using Talabat.Apis.MappingProfiles;
using Talabat.Core.Interfaces;
using Talabat.Repositories.Interfaces.Contract;
using Talabat.Apis.ErrorsHandler;

namespace Talabat.Apis.ExtensionMethods
{
    public static class ApplicationServicesExtension 
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection Services)
        {
            Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            Services.AddAutoMapper(typeof(ProductProfile));
           Services.Configure<ApiBehaviorOptions>(options =>

                options.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var errors = actionContext.ModelState.Where(p => p.Value.Errors.Count() > 0)
                    .SelectMany(P => P.Value.Errors)
                    .Select(M => M.ErrorMessage).ToList();

                    var ApiValidationError = new ApiValidationError()
                    {
                        Errors = errors
                    };
                    return new BadRequestObjectResult(ApiValidationError);
                });

            return Services;
        }
    }
}

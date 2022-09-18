using Core.Utilities.Attributes;
using EC.Services.CategoryAPI.Validations.CategoryValidations;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;

namespace EC.Services.CategoryAPI.Extensions
{
    public static class ControllerExtensions
    {
        public static void AddControllerSettings(this IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(FluentValidationCustomValidationAttribute));
            });

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddValidatorsFromAssemblyContaining<CategoryUpdateDtoValidator>();
            services.AddValidatorsFromAssemblyContaining<CategoryAddDtoValidator>();
            services.AddValidatorsFromAssemblyContaining<CategoryDeleteDtoValidator>();

            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters(); // for client side
        }
    }
}

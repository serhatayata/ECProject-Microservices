using Core.Utilities.Attributes;
using EC.Services.CategoryAPI.Validations;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace EC.Services.CategoryAPI.Extensions
{
    public static class ControllerExtensions
    {
        public static void AddAuth(this IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(FluentValidationCustomValidationAttribute));
            })
            .AddFluentValidation(v =>
            {
                v.RegisterValidatorsFromAssemblyContaining<CategoryUpdateDtoValidator>();
            });

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
        }
    }
}

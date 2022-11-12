using Core.Utilities.Attributes;
using Core.Utilities.Validations;
using EC.Services.LangResourceAPI.Validations.LangResourceValidations;
using EC.Services.LangResourceAPI.Validations.LangValidations;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;

namespace EC.Services.LangResourceAPI.Extensions
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

            services.AddValidatorsFromAssemblyContaining<LangResourceAddDtoValidator>();
            services.AddValidatorsFromAssemblyContaining<LangResourceUpdateDtoValidator>();
            services.AddValidatorsFromAssemblyContaining<LangUpdateDtoValidator>();
            services.AddValidatorsFromAssemblyContaining<LangAddDtoValidator>();

            services.AddValidatorsFromAssemblyContaining<PagingDtoValidator>();

            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters(); // for client side
        }

    }
}

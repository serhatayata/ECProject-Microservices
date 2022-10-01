using Core.Utilities.Attributes;
using EC.Services.BasketAPI.Validations;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;

namespace EC.Services.BasketAPI.Extensions
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

            #region Basket
            services.AddValidatorsFromAssemblyContaining<BasketSaveOrUpdateDtoValidator>();
            services.AddValidatorsFromAssemblyContaining<BasketItemValidator>();
            #endregion

            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters(); // for client side

        }
    }
}

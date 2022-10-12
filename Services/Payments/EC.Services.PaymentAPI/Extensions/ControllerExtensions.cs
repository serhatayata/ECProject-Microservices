using Core.Utilities.Attributes;
using FluentValidation.AspNetCore;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using EC.Services.PaymentAPI.Validations.PaymentValidations;
using EC.Services.PaymentAPI.Validations.BasketValidations;

namespace EC.Services.PaymentAPI.Extensions
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

            services.AddValidatorsFromAssemblyContaining<PaymentAddDtoValidator>();
            services.AddValidatorsFromAssemblyContaining<BasketDtoValidator>();
            services.AddValidatorsFromAssemblyContaining<BasketItemDtoValidator>();

            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters(); // for client side
        }
    }
}

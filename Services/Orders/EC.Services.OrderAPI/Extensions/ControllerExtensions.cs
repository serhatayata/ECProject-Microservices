using Core.Utilities.Attributes;
using Core.Utilities.Validations;
using EC.Services.Order.Application.Validations.OrderItemValidations;
using EC.Services.Order.Application.Validations.OrderValidations;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;

namespace EC.Services.OrderAPI.Extensions
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

            //services.AddValidatorsFromAssemblyContaining<PaymentAddDtoValidator>();
            services.AddValidatorsFromAssemblyContaining<PagingDtoValidator>();
            services.AddValidatorsFromAssemblyContaining<OrderItemDtoValidator>();
            services.AddValidatorsFromAssemblyContaining<OrderCreateDtoValidator>();
            services.AddValidatorsFromAssemblyContaining<AddressDtoValidator>();

            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters(); // for client side
        }
    }
}

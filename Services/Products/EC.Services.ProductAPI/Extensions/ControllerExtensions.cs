using Core.Utilities.Attributes;
using EC.Services.ProductAPI.Validations.ProductValidations;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;

namespace EC.Services.ProductAPI.Extensions
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

            #region Product
            services.AddValidatorsFromAssemblyContaining<ProductAddDtoValidator>();
            services.AddValidatorsFromAssemblyContaining<ProductUpdateDtoValidator>();
            #endregion
            #region ProductVariant

            #endregion
            #region Stock

            #endregion
            #region Variant

            #endregion


        }
    }
}

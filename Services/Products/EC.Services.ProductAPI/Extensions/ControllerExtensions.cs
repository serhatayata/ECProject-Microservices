using Core.Utilities.Attributes;
using EC.Services.ProductAPI.Validations.ProductValidations;
using EC.Services.ProductAPI.Validations.ProductVariantValidations;
using EC.Services.ProductAPI.Validations.StockValidations;
using EC.Services.ProductAPI.Validations.VariantValidations;
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
            services.AddValidatorsFromAssemblyContaining<ProductVariantDtoValidator>();
            services.AddValidatorsFromAssemblyContaining<ProductVariantDeleteDtoValidator>();

            #endregion
            #region Stock
            services.AddValidatorsFromAssemblyContaining<StockAddDtoValidator>();
            services.AddValidatorsFromAssemblyContaining<StockUpdateDtoValidator>();

            #endregion
            #region Variant
            services.AddValidatorsFromAssemblyContaining<VariantAddDtoValidator>();
            services.AddValidatorsFromAssemblyContaining<VariantUpdateDtoValidator>();
            #endregion

            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters(); // for client side

        }
    }
}

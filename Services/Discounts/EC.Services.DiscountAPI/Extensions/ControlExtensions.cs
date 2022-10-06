using Core.Utilities.Attributes;
using FluentValidation.AspNetCore;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using EC.Services.DiscountAPI.Validations.CampaignValidations;
using EC.Services.DiscountAPI.Validations.DiscountValidations;

namespace EC.Services.DiscountAPI.Extensions
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

            #region Campaign
            services.AddValidatorsFromAssemblyContaining<CampaignAddDtoValidator>();
            services.AddValidatorsFromAssemblyContaining<CampaignAddProductDtoValidator>();
            services.AddValidatorsFromAssemblyContaining<CampaignUpdateDtoValidator>();
            #endregion
            #region Discount
            services.AddValidatorsFromAssemblyContaining<DiscountAddDtoValidator>();
            services.AddValidatorsFromAssemblyContaining<DiscountUpdateDtoValidator>();
            services.AddValidatorsFromAssemblyContaining<DiscountGetByIdDtoValidator>();
            services.AddValidatorsFromAssemblyContaining<DiscountGetByCodeDtoValidator>();

            #endregion


            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters(); // for client side
        }
    }
}

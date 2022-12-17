using Core.Utilities.Attributes;
using FluentValidation.AspNetCore;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using EC.Services.DiscountAPI.Validations.CampaignValidations;
using EC.Services.DiscountAPI.Validations.DiscountValidations;
using Core.Utilities.Validations;

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

            #region Base
            services.AddValidatorsFromAssemblyContaining<DeleteStringDtoValidator>();
            services.AddValidatorsFromAssemblyContaining<DeleteIntDtoValidator>();
            services.AddValidatorsFromAssemblyContaining<PagingDtoValidator>();

            #endregion
            #region Campaign
            services.AddValidatorsFromAssemblyContaining<CampaignAddDtoValidator>();
            services.AddValidatorsFromAssemblyContaining<CampaignProductIdValidator>();
            services.AddValidatorsFromAssemblyContaining<CampaignAddProductDtoValidator>();
            services.AddValidatorsFromAssemblyContaining<CampaignUpdateDtoValidator>();
            services.AddValidatorsFromAssemblyContaining<CampaignDeleteProductDtoValidator>();
            #endregion
            #region Discount
            services.AddValidatorsFromAssemblyContaining<DiscountAddDtoValidator>();
            services.AddValidatorsFromAssemblyContaining<DiscountUpdateDtoValidator>();
            services.AddValidatorsFromAssemblyContaining<DiscountGetByCodeDtoValidator>();

            #endregion


            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters(); // for client side
        }
    }
}

using Core.Utilities.Attributes;
using FluentValidation.AspNetCore;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using EC.Services.DiscountAPI.Validations.CampaignValidations;
using EC.Services.DiscountAPI.Validations.DiscountValidations;
using Core.Utilities.Validations;
using EC.Services.DiscountAPI.Validations.CampaignUserValidations;
using EC.Services.DiscountAPI.Validations.CampaignProduct;

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
            services.AddValidatorsFromAssemblyContaining<CampaignIdDtoValidator>();
            services.AddValidatorsFromAssemblyContaining<CampaignAddDtoValidator>();
            services.AddValidatorsFromAssemblyContaining<CampaignProductIdValidator>();
            services.AddValidatorsFromAssemblyContaining<CampaignUpdateDtoValidator>();
            services.AddValidatorsFromAssemblyContaining<CampaignGetWithStatusDtoValidator>();
            services.AddValidatorsFromAssemblyContaining<CampaignGetAllWithStatusDtoValidator>();
            #endregion
            #region Discount
            services.AddValidatorsFromAssemblyContaining<DiscountAddDtoValidator>();
            services.AddValidatorsFromAssemblyContaining<DiscountUpdateDtoValidator>();
            services.AddValidatorsFromAssemblyContaining<DiscountGetByCodeDtoValidator>();

            #endregion
            #region CampaignUser
            services.AddValidatorsFromAssemblyContaining<CampaignIdPagingDtoValidator>();
            services.AddValidatorsFromAssemblyContaining<CampaignUserAddDtoValidator>();
            services.AddValidatorsFromAssemblyContaining<CampaignUserDeleteDtoValidator>();
            services.AddValidatorsFromAssemblyContaining<CampaignUserIdDtoValidator>();
            services.AddValidatorsFromAssemblyContaining<CampaignCodeDtoValidator>();

            #endregion
            #region CampaignProduct
            services.AddValidatorsFromAssemblyContaining<CampaignAddProductDtoValidator>();
            services.AddValidatorsFromAssemblyContaining<CampaignDeleteProductDtoValidator>();

            #endregion

            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters(); // for client side
        }
    }
}

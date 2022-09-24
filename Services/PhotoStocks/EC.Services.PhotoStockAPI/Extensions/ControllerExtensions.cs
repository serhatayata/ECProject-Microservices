﻿using Core.Utilities.Attributes;
using EC.Services.PhotoStockAPI.Validations.PhotoValidations;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;

namespace EC.Services.PhotoStockAPI.Extensions
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

            services.AddValidatorsFromAssemblyContaining<PhotoAddDtoValidator>();
            services.AddValidatorsFromAssemblyContaining<PhotoDeleteByIdDtoValidator>();
            services.AddValidatorsFromAssemblyContaining<PhotoDeleteByTypeAndEntityIdDtoValidator>();
            services.AddValidatorsFromAssemblyContaining<PhotoDeleteByUrlDtoValidator>();

            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters(); // for client side
        }
    }
}

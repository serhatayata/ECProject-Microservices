using Core.Extensions;
using EC.Services.PaymentAPI.Data.Contexts;
using EC.Services.PaymentAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace EC.Services.PaymentAPI.Extensions
{
    public static class SeedDataExtensions
    {
        private static readonly DateTime datetime_now = DateTime.Now;

        public static void AddSeedData(this IServiceCollection services, ConfigurationManager configuration)
        {
            var serviceProvider = services.BuildServiceProvider();
            using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var context = scope.ServiceProvider.GetService<PaymentDbContext>();

            context.Database.Migrate();

            AddPayments(context);

        }

        #region AddCategories
        public static void AddPayments(PaymentDbContext context)
        {
            if (!context.Payments.Any())
            {
                Payment[] payments =
                {
                    new Payment()
                    {
                       AddressDetail="Address detail test 1",
                       CardName="Mehmet Kaya",
                       CardNumber="3333",
                       CDate=DateTime.Now,
                       CityName="Istanbul",
                       CountryName="Turkiye",
                       CountyName="Umraniye",
                       PaymentNo="asdfg12345asdf",
                       PhoneCountry="TR",
                       PhoneNumber="5555555555",
                       Status=(int)PaymentStatus.Waiting,
                       TotalPrice=234.20M,
                       ZipCode="34774"
                    },
                    new Payment()
                    {
                       AddressDetail="Address detail test 2",
                       CardName="Merve Tekin",
                       CardNumber="1111",
                       CDate=DateTime.Now,
                       CityName="Ankara",
                       CountryName="Turkiye",
                       CountyName="Kecioren",
                       PaymentNo="lkjhg54123rewq",
                       PhoneCountry="TR",
                       PhoneNumber="6666666666",
                       Status=(int)PaymentStatus.Waiting,
                       TotalPrice=654.23M,
                       ZipCode="34565"
                    }
                };

                context.Payments.AddRange(payments);
                context.SaveChanges();
            }
        }

        #endregion

    }

}

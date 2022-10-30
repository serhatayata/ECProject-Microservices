using Core.Extensions;
using EC.Services.Order.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace EC.Services.OrderAPI.Extensions
{
    public static class SeedDataExtensions
    {
        private static readonly DateTime datetime_now = DateTime.Now;
        private static int orderNoLength { get; set; }
        public static void AddSeedData(this IServiceCollection services, ConfigurationManager configuration)
        {
            var serviceProvider = services.BuildServiceProvider();
            using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var context = scope.ServiceProvider.GetService<OrderDbContext>();

            orderNoLength = configuration.GetValue<int>("OrderNoLength");

            context.Database.Migrate();

            AddOrders(context);

        }

        #region AddCategories
        public static void AddOrders(OrderDbContext context)
        {
            if (!context.Orders.Any())
            {
                Order.Domain.OrderAggregate.Order[] orders =
                {
                    new Order.Domain.OrderAggregate.Order()
                    {
                        UserId="123123asdasd",
                        AddressDetail="address detail 1",
                        CDate=DateTime.Now,
                        CityName="Istanbul",
                        CountryName="Turkiye",
                        CountyName="Umraniye",
                        OrderNo=RandomExtensions.RandomString(orderNoLength),
                        ZipCode="34777"
                    },
                    new Order.Domain.OrderAggregate.Order()
                    {
                        UserId="1232324asdaf",
                        AddressDetail="address detail 2",
                        CDate=DateTime.Now,
                        CityName="Ankara",
                        CountryName="Turkiye",
                        CountyName="Kecioren",
                        OrderNo=RandomExtensions.RandomString(orderNoLength),
                        ZipCode="12341"
                    },
                    new Order.Domain.OrderAggregate.Order()
                    {
                        UserId="asd54542334",
                        AddressDetail="address detail 3",
                        CDate=DateTime.Now,
                        CityName="Kocaeli",
                        CountryName="Turkiye",
                        CountyName="Gebze",
                        OrderNo=RandomExtensions.RandomString(orderNoLength),
                        ZipCode="12234"
                    },
                };

                context.Orders.AddRange(orders);
                context.SaveChanges();
            }

            if (!context.OrderItems.Any())
            {
                var allOrders = context.Orders.ToList();

                Order.Domain.OrderAggregate.OrderItem[] orderItems =
                {
                    new Order.Domain.OrderAggregate.OrderItem()
                    {
                        Price=500.00M,
                        ProductId="asdaf12314a",
                        Order=allOrders[0],
                        OrderId=allOrders[0].Id,
                        Quantity=1
                    },
                    new Order.Domain.OrderAggregate.OrderItem()
                    {
                        Price=300.00M,
                        ProductId="bddaf12314a",
                        Order=allOrders[1],
                        OrderId=allOrders[1].Id,
                        Quantity=1
                    },
                    new Order.Domain.OrderAggregate.OrderItem()
                    {
                        Price=200.00M,
                        ProductId="ccfaf12314a",
                        Order=allOrders[2],
                        OrderId=allOrders[2].Id,
                        Quantity=1
                    }
                };

                context.OrderItems.AddRange(orderItems);
                context.SaveChanges();
            }
        }

        #endregion


    }
}

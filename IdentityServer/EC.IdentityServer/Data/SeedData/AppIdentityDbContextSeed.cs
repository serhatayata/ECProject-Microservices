using EC.IdentityServer.Data.DbContext;
using EC.IdentityServer.Models.Identity;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace EC.IdentityServer.Data.SeedData
{
    public class AppIdentityDbContextSeed
    {
        public async static Task AddUserSettingsAsync(string connString)
        {
            var assembly = typeof(Program).Assembly.GetName().Name;

            var services = new ServiceCollection();

            services.AddLogging();
            services.AddDbContext<AppIdentityDbContext>(options => options.UseSqlServer(connString, b => b.MigrationsAssembly(assembly)));

            services.AddIdentity<AppUser, AppRole>(options =>
            {
                //this place might be changed...
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            })
            .AddEntityFrameworkStores<AppIdentityDbContext>()
            .AddDefaultTokenProviders();

            var serviceProvider = services.BuildServiceProvider();
            using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();

            var context = scope.ServiceProvider.GetService<AppIdentityDbContext>();
            context.Database.Migrate();

            var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
            var user_1 = await userMgr.FindByNameAsync("905374882316");
            if (user_1 == null)
            {
                user_1 = new AppUser
                {
                    Name = "Serhat",
                    Surname = "Ayata",
                    CreatedAt = DateTime.Now,
                    LastSeen = DateTime.Now,
                    UpdatedAt=DateTime.Now,
                    PhoneNumber="905374882316",
                    UserName = "905374882316",
                    Status=(int)UserStatus.NotValidated,
                    Email = "srht1@email.com",
                    EmailConfirmed = true,
                };
                var result = await userMgr.CreateAsync(user_1, "Password12*");
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                result = await userMgr.AddClaimsAsync(user_1, new Claim[]{
                            new Claim(JwtClaimTypes.Name, "Serhat Ayata"),
                            new Claim(JwtClaimTypes.GivenName, "Serhat"),
                            new Claim(JwtClaimTypes.FamilyName, "Serhat"),
                            new Claim(JwtClaimTypes.WebSite, "http://serhatayata.com"),
                        });

                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }
                //Log.Debug("Serhat Ayata user created");
            }
            else
            {
                //Log.Debug("Serhat Ayata user already exists");
            }

            var user_2 = await userMgr.FindByNameAsync("905555555555");
            if (user_2 == null)
            {
                user_2 = new AppUser
                {
                    Name = "Mehmet",
                    Surname = "Kaya",
                    CreatedAt = DateTime.Now,
                    LastSeen = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    PhoneNumber = "905555555555",
                    UserName = "905555555555",
                    Status = (int)UserStatus.NotValidated,
                    Email = "mkaya@email.com",
                    EmailConfirmed = true,
                };
                var result = await userMgr.CreateAsync(user_2, "Password12");
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                result = await userMgr.AddClaimsAsync(user_2, new Claim[]{
                            new Claim(JwtClaimTypes.Name, "Mehmet Kaya"),
                            new Claim(JwtClaimTypes.GivenName, "Mehmet"),
                            new Claim(JwtClaimTypes.FamilyName, "Kaya"),
                            new Claim(JwtClaimTypes.WebSite, "http://blabla.com"),
                            new Claim("location", "somewhere")
                        });
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }
                //Log.Debug("Mehmet Kaya user created");
            }
            else
            {
                //Log.Debug("Mehmet Kaya user already exists");
            }
        }

    }
}

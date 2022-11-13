using Core.CrossCuttingConcerns.Caching.Redis;
using EC.Services.LangResourceAPI.Data.Contexts;
using EC.Services.LangResourceAPI.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text.Json;

namespace EC.Services.LangResourceAPI.Extensions
{
    public static class SeedDataExtensions
    {
        public static void AddSeedData(this IServiceCollection services, ConfigurationManager configuration)
        {
            var serviceProvider = services.BuildServiceProvider();
            using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var context = scope.ServiceProvider.GetService<LangResourceDbContext>();
            var redisService = scope.ServiceProvider.GetService<IRedisCacheManager>();
            int redisDbId = configuration.GetValue<int>("LangResourceRedisDbId");

            context.Database.Migrate();

            AddLangs(context);
            AddLangResources(context);
            AddLangsToRedis(context,redisService,redisDbId);
            AddLangResourcesToRedis(context,redisService,redisDbId);
        }

        #region AddLangs
        public static void AddLangs(LangResourceDbContext context)
        {
            if (!context.Langs.Any())
            {
                Lang[] langs =
                {
                    new()
                    {
                        Name="Turkish",
                        Code="tr-TR"
                    },
                    new()
                    {
                        Name="English",
                        Code="tr-TR"
                    },
                    new()
                    {
                        Name="Spanish",
                        Code="es-ES"
                    }
                };

                context.Langs.AddRange(langs);
                context.SaveChanges();
            }
        }
        #endregion
        #region AddLangResources
        public static void AddLangResources(LangResourceDbContext context)
        {
            if (!context.LangResources.Any())
            {
                var allLangs = context.Langs.ToList();
                var lang1 = allLangs[0];
                var lang2 = allLangs[1];
                var lang3 = allLangs[2];

                LangResource[] langResources =
                {
                    #region login.phonenumber
                    new()
                    {
                       Tag="login.phonenumber",
                       Description="Telefon numarası",
                       MessageCode="0000",
                       LangId=lang1.Id,
                       Lang=lang1
                    },
                    new()
                    {
                       Tag="login.phonenumber",
                       Description="Phone number",
                       MessageCode="0000",
                       LangId=lang2.Id,
                       Lang=lang2
                    },
                    new()
                    {
                       Tag="login.phonenumber",
                       Description="Número de teléfono",
                       MessageCode="0000",
                       LangId=lang3.Id,
                       Lang=lang3
                    },
	                #endregion
                    #region login.password
                    new()
                    {
                       Tag="login.password",
                       Description="Şifre",
                       MessageCode="0000",
                       LangId=lang1.Id,
                       Lang=lang1
                    },
                    new()
                    {
                       Tag="login.password",
                       Description="Password",
                       MessageCode="0000",
                       LangId=lang2.Id,
                       Lang=lang2
                    },
                    new()
                    {
                       Tag="login.password",
                       Description="Clave",
                       MessageCode="0000",
                       LangId=lang3.Id,
                       Lang=lang3
                    },
	                #endregion
                    #region login.phonenumber.null
                    new()
                    {
                       Tag="login.phonenumber.null",
                       Description="Telefon numarası boş bırakılamaz",
                       MessageCode="1001",
                       LangId=lang1.Id,
                       Lang=lang1
                    },
                    new()
                    {
                       Tag="login.phonenumber.null",
                       Description="Phone number cannot be null",
                       MessageCode="1001",
                       LangId=lang2.Id,
                       Lang=lang2
                    },
                    new()
                    {
                       Tag="login.phonenumber.null",
                       Description="El número de teléfono no puede ser nulo",
                       MessageCode="1001",
                       LangId=lang3.Id,
                       Lang=lang3
                    },
	                #endregion
                    #region login.password.null
                    new()
                    {
                       Tag="login.password.null",
                       Description="Şifre boş bırakılamaz",
                       MessageCode="1002",
                       LangId=lang1.Id,
                       Lang=lang1
                    },
                    new()
                    {
                       Tag="login.password.null",
                       Description="Password cannot be null",
                       MessageCode="1002",
                       LangId=lang2.Id,
                       Lang=lang2
                    },
                    new()
                    {
                       Tag="login.password.null",
                       Description="La contraseña no puede ser nula",
                       MessageCode="1002",
                       LangId=lang3.Id,
                       Lang=lang3
                    }
	                #endregion
                };

                context.LangResources.AddRange(langResources);
                context.SaveChanges();
            }
        }
        #endregion
        #region AddLangsToRedis
        public static void AddLangsToRedis(LangResourceDbContext context,IRedisCacheManager redisCacheManager, int redisDbId)
        {
            if (String.IsNullOrEmpty(redisCacheManager.GetDatabase(db: redisDbId).StringGet("langs")))
            {
                return;
            }

            var allLangs = context.Langs.ToList();

            var status = redisCacheManager.GetDatabase(db: redisDbId).StringSet("langs", JsonSerializer.Serialize(allLangs));
        }
        #endregion
        #region AddLangResourcesToRedis
        public static void AddLangResourcesToRedis(LangResourceDbContext context, IRedisCacheManager redisCacheManager, int redisDbId)
        {
            if (String.IsNullOrEmpty(redisCacheManager.GetDatabase(db: redisDbId).StringGet("langResources")))
            {
                return;
            }

            var allLangResources = context.LangResources.ToList();

            var status = redisCacheManager.GetDatabase(db: redisDbId).StringSet("langResources", JsonSerializer.Serialize(allLangResources));
        }
        #endregion

    }
}

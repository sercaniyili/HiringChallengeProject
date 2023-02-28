using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HiringChallange.Application.Interfaces.Cache;
using HiringChallange.Application.Interfaces.MessageBrokers;
using HiringChallange.Application.Interfaces.Token;
using HiringChallange.Infrastructure.Services.Cache;
using HiringChallange.Infrastructure.Services.MessageBrokers;
using HiringChallange.Infrastructure.Services.Token;

namespace HiringChallange.Infrastructure
{
    public static class ServisRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            //Redis configuration
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = "localhost:6379";
            });
            

            //Add Ioc
            services.AddScoped<ITokenGenerator,TokenGenerator>();

            services.AddSingleton<IRedisDistrubutedCache, RedisDistrubutedCacheService>();

            services.AddTransient<IRabbitmqConnection, RabbitmqConnection>();
            services.AddTransient<IRabbitmqService, RabbitmqService>();



            //Auth configuration
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
             .AddJwtBearer(options =>
             {
             options.SaveToken = true;
             options.RequireHttpsMetadata = false;
             options.TokenValidationParameters = new TokenValidationParameters()
             {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidAudience = configuration["JWT:ValidAudience"],
            ValidIssuer = configuration["JWT:ValidIssuer"],
            ValidateLifetime = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
              };
              });


            return services;
        }


    }
}

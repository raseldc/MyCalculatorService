using Microsoft.Extensions.DependencyInjection;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCalculator.Model;
using WebCalculator.Service.services;
using WebCalculator.services;

namespace WebCalculator.Service
{
    public  static class IocConfig
    {
        public static void Register(IServiceCollection services)
        {
            services.AddSingleton<TokenService>();
            services.AddSingleton<CustomerService>();
            services.AddSingleton<ChargeService>();
            services.AddSingleton<IUserService,UserService>();
            services.AddSingleton<IStripeService,StripeService>();
            services.AddSingleton<IEmailService,EmailService>();
            services.AddSingleton<ApplicationDbContext>();
            services.AddSingleton<ICardInfoService, CardInfoService>();
        }
    }
}

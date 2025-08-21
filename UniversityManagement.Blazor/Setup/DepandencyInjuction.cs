using AutoMapper;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UniversityManagement.Blazor.Components.Account;
using UniversityManagementSystem.Application.Abstractions;
using UniversityManagementSystem.Application.Interfaces;
using UniversityManagementSystem.Application.Mapper;
using UniversityManagementSystem.Application.Services;
using UniversityManagementSystem.Domain.Common.Interfaces;
using UniversityManagementSystem.Domain.Entities;
using UniversityManagementSystem.Domain.Interfaces;
using UniversityManagementSystem.Infrastructure.Data;
using UniversityManagementSystem.Infrastructure.Data.Repositories;
using UniversityManagementSystem.Infrastructure.Persistence;

namespace UniversityManagement.Blazor.Setup
{
    public static class DepandencyInjuction
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Add services to the container.
            services.AddRazorComponents()
                .AddInteractiveServerComponents();

            services.AddCascadingAuthenticationState();
            services.AddScoped<IdentityUserAccessor>();
            services.AddScoped<IdentityRedirectManager>();
            services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

            //services.AddAuthentication(options =>
            //{
            //    options.DefaultScheme = IdentityConstants.ApplicationScheme;
            //    options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
            //})
            //    .AddIdentityCookies();

            var connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            services.AddDbContext<UniversityDbContext>(options =>
                options.UseSqlServer(connectionString));
            services.AddDatabaseDeveloperPageExceptionFilter();

            //services.AddIdentityCore<User>(options => options.SignIn.RequireConfirmedAccount = true)
            //    .AddEntityFrameworkStores<UniversityDbContext>()
            //    .AddSignInManager()
            //    .AddDefaultTokenProviders();

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<UniversityDbContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<IToastService, ToastService>();
            

            // الحصول على جميع الأنواع في assembly الخدمات
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            // للحصول على Assembly الخدمات تلقائيًا دون تحديد نوع معين
            var servicesAssembly = typeof(Injectable).Assembly;


            var serviceTypes = servicesAssembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof(Injectable)))
                .ToList();

            if (serviceTypes == null) return services;

            foreach (var serviceType in serviceTypes)
            {
                var interfaceType = serviceType.GetInterfaces()
                    .FirstOrDefault(i => i.Name == $"I{serviceType.Name}");

                if (interfaceType != null)
                {
                    services.AddScoped(interfaceType, serviceType);
                }
            }

           

            services.AddLogging();
            return services;
        }
    }
}

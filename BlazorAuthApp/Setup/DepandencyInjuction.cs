using BlazorAuthApp.Components.Account;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Reflection;
using UniversityManagementSystem.Application.Abstractions;
using UniversityManagementSystem.Application.Services;
using UniversityManagementSystem.Domain.Entities;
using UniversityManagementSystem.Domain.Interfaces;
using UniversityManagementSystem.Infrastructure.Data.Repositories;
using UniversityManagementSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using BlazorAuthApp.Mapper;
using UniversityManagementSystem.Domain.Common.Interfaces;
using UniversityManagementSystem.Infrastructure.Persistence;
using BlazorAuthApp.Provider;

namespace BlazorAuthApp.Setup
{
    public static class DepandencyInjuction
    {
        public static IServiceCollection AddServices(this IServiceCollection services,IConfiguration configuration)
        {
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

            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
            })
                .AddEntityFrameworkStores<UniversityDbContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<IdentityRedirectManager>();

            services.AddScoped<CustomAuthStateProvider>();
            services.AddScoped<AuthenticationStateProvider>(provider =>
                provider.GetRequiredService<CustomAuthStateProvider>());

            services.AddAuthorizationCore();

            services.AddSingleton<IEmailSender<User>, IdentityNoOpEmailSender>();


            // الحصول على جميع الأنواع في assembly الخدمات
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>)); 

            var serviceTypes = Assembly.GetAssembly(typeof(StudentServices))
                ?.GetTypes()
                .Where(t => t.IsClass &&
                           !t.IsAbstract &&
                           t.IsSubclassOf(typeof(Injectable)))
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

            var configuration1 = new MapperConfiguration(e =>
            e.AddProfiles(new List<Profile> {
                new UnivercityMappingProfile()
            }));
            services.AddAutoMapper(typeof(Profile).Assembly);
            var mapper = configuration1.CreateMapper();
            services.AddSingleton(mapper);
            services.AddLogging();

            return services;
        }
    }
}

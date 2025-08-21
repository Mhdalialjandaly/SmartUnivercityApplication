using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using UniversityManagement.Blazor.Components;
using UniversityManagement.Blazor.Hubs;
using UniversityManagement.Blazor.Services;
using UniversityManagement.Blazor.Setup;
using UniversityManagementSystem.Application.Mapper;
using UniversityManagementSystem.Domain.Entities;

namespace UniversityManagement.Blazor
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddServices(builder.Configuration);
            builder.Services.AddSignalR();
            builder.Services.AddHttpContextAccessor();

            // إنشاء configuration يدوياً
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<UnivercityMappingProfile>();
                cfg.AddProfile<CustomMappingProfile>();
            });

            // تسجيل IMapper كخدمة
            builder.Services.AddSingleton<IMapper>(mapperConfig.CreateMapper());

            builder.Services.AddTransient<IEmailSender<User>, EmailSender>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            // Add additional endpoints required by the Identity /Account Razor components.
            app.MapAdditionalIdentityEndpoints();
            app.MapHub<ChatHub>("/chathub");
            app.Run();
        }
    }
}

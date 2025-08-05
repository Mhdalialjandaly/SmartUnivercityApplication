using UniversityManagementSystem.Blazor.Components;
using UniversityManagementSystem.Blazor.SetUp;

namespace UniversityManagementSystem.Blazor
{
    public class Program
    {
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Services.AddServices(builder.Configuration);

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseMigrationsEndPoint();
			}
			else
			{
				app.UseExceptionHandler("/Error");
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			// أضف هذه السطور قبل UseAntiforgery
			app.UseRouting();
			app.UseAuthentication();
			app.UseAuthorization();

			app.UseAntiforgery();

			app.MapRazorComponents<App>()
				.AddInteractiveServerRenderMode();

			app.MapAdditionalIdentityEndpoints();

			app.Run();
		}
	}
}

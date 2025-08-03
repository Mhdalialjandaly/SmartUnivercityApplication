using BlazorAuthApp.Components;
using BlazorAuthApp.Setup;
namespace BlazorAuthApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            builder.Services.AddServices(builder.Configuration);

            var app = builder.Build();

            app.UseStaticFiles();
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

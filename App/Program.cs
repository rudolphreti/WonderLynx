using App.Services; 

namespace App
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddHttpClient("API", client =>
            {
                client.BaseAddress = new Uri("https://localhost:7265/");
                client.DefaultRequestVersion = new Version(1, 1); // Optional: Setzt HTTP/1.1 statt HTTP/2
            }).ConfigurePrimaryHttpMessageHandler(() =>
            {
                return new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; }
                };
            });
            builder.Services.AddTransient<ReferenceItemService>();

            builder.Services.AddHttpContextAccessor();
            builder.Services.AddControllersWithViews();

            var app = builder.Build();
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseHttpMethodOverride();
            app.UseRouting();
            app.UseAuthorization();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}

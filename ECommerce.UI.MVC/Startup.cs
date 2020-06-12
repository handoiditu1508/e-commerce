using ECommerce.UI.MVC.Infrastructure;
using ECommerce.UI.Shared.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace ECommerce.UI.MVC
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllersWithViews();
			services.AddMvc(option =>
			{
				//option.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
				//option.Filters.Add(new RequireHttpsAttribute());
			})
			.AddRazorOptions(option =>
			{
				option.ViewLocationFormats.Add("/Views/Shared/Partials/{0}.cshtml");
				option.ViewLocationFormats.Add("/Pages/Shared/Partials/{0}.cshtml");
				option.ViewLocationFormats.Add("/Views/Shared/ClientLibraries/{0}.cshtml");
				option.ViewLocationFormats.Add("/Pages/Shared/ClientLibraries/{0}.cshtml");
			});
			services.AddScoped(sp => DefaultDataTier.GetUnitOfWork(sp));
			services.AddScoped(sp => CookieCart.GetCart(sp));
			services.AddHttpContextAccessor();
			services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
			services.AddMemoryCache();
			services.AddSession();
			services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();
			services.AddHttpClient();
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			app.UseDeveloperExceptionPage();
			app.UseStatusCodePages();
			app.UseStaticFiles();
			app.UseSession();
			app.UseRouting();
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: null,
					pattern: "Cart",
					defaults: new { controller = "Cart", action = "Index" });

				endpoints.MapControllerRoute(
					name: null,
					pattern: "Cart/AddToCart/{sellerId}/{productTypeId}/{quantity:int?}",
					defaults: new { controller = "Cart", action = "AddToCart" });

				endpoints.MapControllerRoute(
					name: null,
					pattern: "Cart/RemoveFromCart/{sellerId}/{productTypeId}",
					defaults: new { controller = "Cart", action = "RemoveFromCart" });

				endpoints.MapControllerRoute(
					name: null,
					pattern: "AddToCart/{sellerId}/{productTypeId}/{quantity:int?}",
					defaults: new { controller = "Cart", action = "AddToCart" });

				endpoints.MapControllerRoute(
					name: null,
					pattern: "RemoveFromCart/{sellerId}/{productTypeId}",
					defaults: new { controller = "Cart", action = "RemoveFromCart" });

				endpoints.MapControllerRoute(
					name: null,
					pattern: "Products/",
					defaults: new { controller = "Product", action = "Index", page = 1 });

				endpoints.MapControllerRoute(
					name: "Default",
					pattern: $"{{controller={UrlHelperExtensions.DefaultController}}}/{{action={UrlHelperExtensions.DefaultAction}}}/{{id?}}");
			});
			//app.UseHttpsRedirection();
		}
	}
}
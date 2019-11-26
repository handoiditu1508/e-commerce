using ECommerce.UI.MVC.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace ECommerce.UI.MVC
{
	public class Startup
	{
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc(option=>
			{
				option.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
				//option.Filters.Add(new RequireHttpsAttribute());
			});
			services.AddScoped(sp => CookieCart.GetCart(sp));
			services.AddScoped(sp => DefaultDataTier.GetUnitOfWork(sp));
			services.AddHttpContextAccessor();
			services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
			services.AddMemoryCache();
			services.AddSession();
			services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			app.UseDeveloperExceptionPage();
			app.UseStatusCodePages();
			app.UseStaticFiles();
			app.UseSession();
			//app.UseHttpsRedirection();
			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: null,
					template: "Cart",
					defaults: new { controller = "Cart", action = "Index" });

				routes.MapRoute(
					name: null,
					template: "Cart/AddToCart/{sellerId}/{productTypeId}/{quantity:int?}",
					defaults: new { controller = "Cart", action = "AddToCart" });

				routes.MapRoute(
					name: null,
					template: "Cart/RemoveFromCart/{sellerId}/{productTypeId}",
					defaults: new { controller = "Cart", action = "RemoveFromCart" });

				routes.MapRoute(
					name: null,
					template: "AddToCart/{sellerId}/{productTypeId}/{quantity:int?}",
					defaults: new { controller = "Cart", action = "AddToCart" });

				routes.MapRoute(
					name: null,
					template: "RemoveFromCart/{sellerId}/{productTypeId}",
					defaults: new { controller = "Cart", action = "RemoveFromCart" });

				routes.MapRoute(
					name: null,
					template: "Products/{categoryId}/Page{page:int}",
					defaults: new { controller = "Product", action = "Index" });

				routes.MapRoute(
					name: null,
					template: "Products/Page{page:int}",
					defaults: new { controller = "Product", action = "Index", page = 1 });

				routes.MapRoute(
					name: null,
					template: "Products/{categoryId}",
					defaults: new { controller = "Product", action = "Index", page = 1 });

				routes.MapRoute(
					name: null,
					template: "Products/",
					defaults: new { controller = "Product", action = "Index", page = 1 });
				
				routes.MapRoute(
					name: "Default",
					template: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}
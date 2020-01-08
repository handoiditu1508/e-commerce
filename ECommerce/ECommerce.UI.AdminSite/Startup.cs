using ECommerce.UI.Shared.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace ECommerce.UI.AdminSite
{
	public class Startup
	{
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc().AddRazorOptions(option =>
			{
				option.ViewLocationFormats.Add("/Views/Shared/Partials/{0}.cshtml");
				option.ViewLocationFormats.Add("/Pages/Shared/Partials/{0}.cshtml");
				option.ViewLocationFormats.Add("/Views/Shared/ClientLibraries/{0}.cshtml");
				option.ViewLocationFormats.Add("/Pages/Shared/ClientLibraries/{0}.cshtml");
			});
			services.AddScoped(sp => DefaultDataTier.GetUnitOfWork(sp));
			services.AddHttpContextAccessor();
			services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
			services.AddMemoryCache();
			services.AddSession();
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			app.UseDeveloperExceptionPage();
			app.UseStatusCodePages();
			app.UseStaticFiles();
			app.UseSession();
			app.UseMvcWithDefaultRoute();
			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "Default",
					template: $"{{controller={UrlHelperExtensions.DefaultController}}}/{{action={UrlHelperExtensions.DefaultAction}}}/{{id?}}");
			});
		}
	}
}

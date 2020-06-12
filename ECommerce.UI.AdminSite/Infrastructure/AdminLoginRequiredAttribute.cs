using ECommerce.Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace ECommerce.UI.AdminSite.Infrastructure
{
	public class AdminLoginRequiredAttribute : TypeFilterAttribute
	{
		public AdminLoginRequiredAttribute() : base(typeof(AdminLoginRequiredFilter))
		{ }

		private class AdminLoginRequiredFilter : IAsyncAuthorizationFilter
		{
			private AdminLoginPersistence loginPersistence;

			public AdminLoginRequiredFilter(IHttpContextAccessor accessor, IUnitOfWork unitOfWork)
			{
				loginPersistence = new AdminLoginPersistence(accessor, unitOfWork);
			}

			public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
			{
				if ((await loginPersistence.PersistLoginAsync()) == null)
					context.Result = new RedirectToActionResult("Login", "Admin", new { });
			}
		}
	}
}
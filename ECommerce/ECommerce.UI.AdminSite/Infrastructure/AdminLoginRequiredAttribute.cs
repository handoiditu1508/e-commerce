using ECommerce.Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ECommerce.UI.AdminSite.Infrastructure
{
	public class AdminLoginRequiredAttribute : TypeFilterAttribute
	{
		public AdminLoginRequiredAttribute() : base(typeof(AdminLoginRequiredFilter))
		{ }

		private class AdminLoginRequiredFilter : IAuthorizationFilter
		{
			private AdminLoginPersistence loginPersistence;

			public AdminLoginRequiredFilter(IHttpContextAccessor accessor, IUnitOfWork unitOfWork)
			{
				loginPersistence = new AdminLoginPersistence(accessor, unitOfWork);
			}

			public void OnAuthorization(AuthorizationFilterContext context)
			{
				if (loginPersistence.PersistLogin() == null)
					context.Result = new RedirectToActionResult("Login", "Admin", new { });
			}
		}
	}
}
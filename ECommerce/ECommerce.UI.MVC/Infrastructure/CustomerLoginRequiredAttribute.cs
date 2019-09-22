using ECommerce.Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ECommerce.UI.MVC.Infrastructure
{
	public class CustomerLoginRequiredAttribute : TypeFilterAttribute
	{
		public CustomerLoginRequiredAttribute() : base(typeof(CustomerLoginRequiredFilter))
		{ }

		private class CustomerLoginRequiredFilter : IAuthorizationFilter
		{
			private CustomerLoginPersistence loginPersistence;

			public CustomerLoginRequiredFilter(IHttpContextAccessor accessor, IUnitOfWork unitOfWork)
			{
				loginPersistence = new CustomerLoginPersistence(accessor, unitOfWork);
			}

			public void OnAuthorization(AuthorizationFilterContext context)
			{
				if (loginPersistence.PersistLogin() == null)
					context.Result = new RedirectToActionResult("Login", "Customer", new { });
			}
		}
	}
}
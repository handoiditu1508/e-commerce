using ECommerce.Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace ECommerce.UI.MVC.Infrastructure
{
	public class CustomerLoginRequiredAttribute : TypeFilterAttribute
	{
		public CustomerLoginRequiredAttribute() : base(typeof(CustomerLoginRequiredFilter))
		{ }

		private class CustomerLoginRequiredFilter : IAsyncAuthorizationFilter
		{
			private CustomerLoginPersistence loginPersistence;

			public CustomerLoginRequiredFilter(IHttpContextAccessor accessor, IUnitOfWork unitOfWork)
			{
				loginPersistence = new CustomerLoginPersistence(accessor, unitOfWork);
			}

			public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
			{
				if ((await loginPersistence.PersistLoginAsync()) == null)
					context.Result = new RedirectToActionResult("Login", "Customer", new { });
			}
		}
	}
}
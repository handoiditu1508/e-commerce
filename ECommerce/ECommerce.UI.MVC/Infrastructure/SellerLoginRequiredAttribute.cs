using ECommerce.Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace ECommerce.UI.MVC.Infrastructure
{
	public class SellerLoginRequiredAttribute : TypeFilterAttribute
	{
		public SellerLoginRequiredAttribute() : base(typeof(SellerLoginRequiredFilter))
		{ }

		private class SellerLoginRequiredFilter : IAsyncAuthorizationFilter
		{
			private SellerLoginPersistence loginPersistence;

			public SellerLoginRequiredFilter(IHttpContextAccessor accessor, IUnitOfWork unitOfWork)
			{
				loginPersistence = new SellerLoginPersistence(accessor, unitOfWork);
			}

			public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
			{
				if (loginPersistence.PersistLogin() == null)
					context.Result = new RedirectToActionResult("Login", "Seller", new { });
			}
		}
	}
}
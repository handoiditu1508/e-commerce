using ECommerce.Infrastructure.UnitOfWork;
using ECommerce.UI.MVC.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ECommerce.UI.MVC.Components
{
	public class CustomerDropDownViewComponent : ViewComponent
	{
		private CustomerLoginPersistence loginPersistence;

		public CustomerDropDownViewComponent(IHttpContextAccessor accessor, IUnitOfWork unitOfWork)
		{
			loginPersistence = new CustomerLoginPersistence(accessor, unitOfWork);
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			return View(await loginPersistence.PersistLoginAsync());
		}
	}
}
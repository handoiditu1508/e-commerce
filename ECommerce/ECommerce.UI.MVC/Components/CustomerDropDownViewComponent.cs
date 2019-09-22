using ECommerce.Infrastructure.UnitOfWork;
using ECommerce.UI.MVC.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.UI.MVC.Components
{
	public class CustomerDropDownViewComponent : ViewComponent
	{
		private CustomerLoginPersistence loginPersistence;

		public CustomerDropDownViewComponent(IHttpContextAccessor accessor, IUnitOfWork unitOfWork)
		{
			loginPersistence = new CustomerLoginPersistence(accessor, unitOfWork);
		}

		public IViewComponentResult Invoke()
		{
			return View(loginPersistence.PersistLogin());
		}
	}
}
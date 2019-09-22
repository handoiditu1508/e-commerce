using ECommerce.Infrastructure.UnitOfWork;
using ECommerce.UI.MVC.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.UI.MVC.Components
{
	public class SellerDropDownViewComponent : ViewComponent
	{
		private SellerLoginPersistence loginPersistence;

		public SellerDropDownViewComponent(IHttpContextAccessor accessor, IUnitOfWork unitOfWork)
		{
			loginPersistence = new SellerLoginPersistence(accessor, unitOfWork);
		}

		public IViewComponentResult Invoke()
		{
			return View(loginPersistence.PersistLogin());
		}
	}
}
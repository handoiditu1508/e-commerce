using ECommerce.Infrastructure.UnitOfWork;
using ECommerce.UI.AdminSite.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ECommerce.UI.AdminSite.Components
{
	public class AdminDropDownViewComponent : ViewComponent
	{
		private AdminLoginPersistence loginPersistence;

		public AdminDropDownViewComponent(IHttpContextAccessor accessor, IUnitOfWork unitOfWork)
		{
			loginPersistence = new AdminLoginPersistence(accessor, unitOfWork);
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			return View(await loginPersistence.PersistLoginAsync());
		}
	}
}
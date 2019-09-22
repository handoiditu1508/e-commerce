using ECommerce.Application;
using ECommerce.Infrastructure.UnitOfWork;
using System;

namespace ECommerce.UI.AdminSite
{
	public static class DefaultDataTier
	{
		public static DataTier Get => DataTier.EntityFrameworkCore;

		public static IUnitOfWork GetUnitOfWork(IServiceProvider services)
			=> UnitOfWorkFactory.GetUnitOfWork(Get);
	}
}
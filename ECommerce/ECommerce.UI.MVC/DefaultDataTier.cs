using ECommerce.Application;
using ECommerce.Infrastructure.UnitOfWork;
using System;

namespace ECommerce.UI.MVC
{
	public static class DefaultDataTier
	{
		public static DataTier Get => DataTier.EntityFrameworkCore;

		public static IUnitOfWork GetUnitOfWork(IServiceProvider services)
			=> UnitOfWorkFactory.GetUnitOfWork(Get);
	}
}
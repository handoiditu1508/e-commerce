using ECommerce.Infrastructure.UnitOfWork;
using System;

namespace ECommerce.Application
{
	public static class UnitOfWorkFactory
	{
		public static IUnitOfWork GetUnitOfWork(DataTier dataTier)
		{
			switch (dataTier)
			{
				case DataTier.EntityFrameworkCore:
					return new Persistence.EF.UnitOfWork();
			}
			throw new NotImplementedException();
		}
	}

	public enum DataTier
	{
		EntityFrameworkCore
	}
}
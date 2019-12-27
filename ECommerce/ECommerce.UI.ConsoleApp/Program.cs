using ECommerce.Application;
using ECommerce.Infrastructure.UnitOfWork;
using ECommerce.Persistence.EF;
using System;
using System.Linq;

namespace ECommerce.UI.ConsoleApp
{
	class Program
	{
		static void Main(string[] args)
		{
			ApplicationDbContext context = new ApplicationDbContext();
			IUnitOfWork uow = new UnitOfWork();
			ECommerceService eCommerce = new ECommerceService(uow);

			var rand = new Random();
			Console.WriteLine(rand.Next());

			Console.ReadKey();
		}
	}
}
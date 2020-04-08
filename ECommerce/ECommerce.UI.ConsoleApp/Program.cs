using ECommerce.Application;
using ECommerce.Extensions;
using ECommerce.Infrastructure.UnitOfWork;
using ECommerce.Models.Entities.Sellers;
using ECommerce.Persistence.EF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.UI.ConsoleApp
{
	class Program
	{
		static void Main(string[] args)
		{
			MainAsync().Wait();
		}

		static async Task MainAsync()
		{
			ApplicationDbContext context = new ApplicationDbContext();
			IUnitOfWork uow = new UnitOfWork();
			ECommerceService eCommerce = new ECommerceService(uow);

			string[] splitedSearchString = new string[] { "asfdssfg", "fsdfsdf" };
			var x = context.ProductTypes.Where(p => p.Name.Contains("asd", CompareOptions.IgnoreNonSpace));

			Console.WriteLine(x.Count());

			Console.WriteLine("Done!");
			Console.ReadKey();
		}
	}
}
using Dapper;
using ECommerce.Application;
using ECommerce.Application.Extensions;
using ECommerce.Application.Extensions.UpdateModels;
using ECommerce.Models.SearchModels;
using ECommerce.Application.WorkingModels.Views;
using ECommerce.Infrastructure.UnitOfWork;
using ECommerce.Models.Entities;
using ECommerce.Models.Entities.Customers;
using ECommerce.Models.Entities.ProductTypes;
using ECommerce.Models.Entities.Sellers;
using ECommerce.Persistence.EF;
using ECommerce.UI.MVC.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ECommerce.UI.ConsoleApp
{
	class Program
	{
		static void Main(string[] args)
		{
			ApplicationDbContext context = new ApplicationDbContext();
			IUnitOfWork uow = new UnitOfWork();
			ECommerceService eCommerce = new ECommerceService(uow);

			var list = new int[] {1,2,3,4,5,6,7 };

			var x = list.Where(i => i == Test.GetNum() + 1);
			Console.WriteLine(x.Count());
			Console.WriteLine(Test.count);

			Console.WriteLine("Done.");
			Console.ReadKey();
		}
	}

	public class Test
	{
		public static int count = 0;

		public static int GetNum()
		{
			count++;
			return 5;
		}
	}
}
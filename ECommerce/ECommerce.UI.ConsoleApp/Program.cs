using ECommerce.Application;
using ECommerce.Application.Extensions;
using ECommerce.Application.Extensions.UpdateModels;
using ECommerce.Application.SearchModels;
using ECommerce.Application.Views;
using ECommerce.Infrastructure.UnitOfWork;
using ECommerce.Models.Entities;
using ECommerce.Models.Entities.ProductTypes;
using ECommerce.Models.Entities.Sellers;
using ECommerce.Persistence.EF;
using ECommerce.UI.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ECommerce.UI.ConsoleApp
{
	class Program
	{
		static void Main(string[] args)
		{
			//ApplicationDbContext context = new ApplicationDbContext();
			IUnitOfWork uow = new UnitOfWork();
			ECommerceService eCommerce = new ECommerceService(uow);

			var x = eCommerce.GetProductTypeUpdateRequestBy(1, 8);
			Console.WriteLine(x.Descriptions);

			Console.WriteLine("Done.");
			Console.ReadKey();
		}
	}
}
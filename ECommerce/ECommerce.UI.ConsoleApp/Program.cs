using Dapper;
using ECommerce.Application;
using ECommerce.Application.Extensions;
using ECommerce.Application.Extensions.UpdateModels;
using ECommerce.Application.SearchModels;
using ECommerce.Application.Views;
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

			HashSet<string> hashSet = new HashSet<string>();
			hashSet.Add("Trắng");
			hashSet.Add("Đen");
			hashSet.Add("Đỏ");
			foreach(string s in hashSet)
			{
				Console.WriteLine(s);
			}

			Console.WriteLine("Done.");
			Console.ReadKey();
		}
	}
}
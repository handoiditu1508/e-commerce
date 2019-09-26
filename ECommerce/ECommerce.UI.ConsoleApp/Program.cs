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
			/*//ApplicationDbContext context = new ApplicationDbContext();
			IUnitOfWork uow = new UnitOfWork();
			ECommerceService eCommerce = new ECommerceService(uow);*/

			string connectionString = @"Server=(localdb)\MSSQLLocalDB;Database=ECommerce;integrated security=true;Trusted_Connection=True;MultipleActiveResultSets=True";
			using (IDbConnection db=new SqlConnection(connectionString))
			{
				db.Open();
				var @params = new { Id = 1, FirstName="gold" };
				var result = db.Query<Customer>("SELECT * FROM Customer WHERE Id = @Id AND FirstName = @FirstName", @params).SingleOrDefault();
				Console.WriteLine(result?.Password);
			}

			Console.WriteLine("Done.");
			Console.ReadKey();
		}
	}
}
﻿using ECommerce.Application;
using ECommerce.Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.WebService.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		private ECommerceService eCommerce;

		public ProductController(IUnitOfWork unitOfWork)
		{
			eCommerce = new ECommerceService(unitOfWork);
		}

		[HttpGet(nameof(GetProductAttributesStates) + "/Seller/{sellerId}/ProductType/{productTypeId}")]
		public async Task<IEnumerable<IDictionary<string, string>>> GetProductAttributesStates(int sellerId, int productTypeId)
		{
			return await eCommerce.GetProductAttributesStatesAsync(sellerId, productTypeId);
		}

		[HttpGet(nameof(GetProductAttributes) + "/Seller/{sellerId}/ProductType/{productTypeId}")]
		public async Task<IDictionary<string, HashSet<string>>> GetProductAttributes(int sellerId, int productTypeId)
		{
			return await eCommerce.GetProductAttributesAsync(sellerId, productTypeId);
		}
	}
}

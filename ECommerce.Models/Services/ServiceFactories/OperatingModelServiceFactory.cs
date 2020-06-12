using ECommerce.Models.Entities.Sellers;
using ECommerce.Models.Repositories;
using ECommerce.Models.Services.OperatingModelServices;
using System;

namespace ECommerce.Models.Services.ServiceFactories
{
	public class OperatingModelServiceFactory
	{
		private ISellerRepository sellerRepository;

		public OperatingModelServiceFactory(ISellerRepository sellerRepository)
			=> this.sellerRepository = sellerRepository;

		public OperatingModelService GetService(OperatingModel model)
		{
			switch (model)
			{
				case OperatingModel.ODF: return new ODFService(sellerRepository);
			}
			throw new NotImplementedException();
		}
	}
}
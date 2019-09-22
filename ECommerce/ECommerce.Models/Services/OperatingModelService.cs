using ECommerce.Models.Entities.Sellers;
using ECommerce.Models.Repositories;
using System.Collections.Generic;

namespace ECommerce.Models.Services
{
	public abstract class OperatingModelService
	{
		protected ISellerRepository sellerRepository;
		public OperatingModel ServiceOperatingModel { get; set; }

		public OperatingModelService(OperatingModel serviceOperatingModel, ISellerRepository sellerRepository)
		{
			this.sellerRepository = sellerRepository;
			ServiceOperatingModel = serviceOperatingModel;
		}

		public abstract bool CanAdminAddProductQuantity(Product product, out ICollection<string> errors);

		public abstract bool CanAdminReduceProductQuantity(Product product, out ICollection<string> errors);

		public abstract bool CanSellerAddProductQuantity(Product product, out ICollection<string> errors);

		public abstract bool CanSellerReduceProductQuantity(Product product, out ICollection<string> errors);

		public abstract bool CanAdminConfirmsOrder();

		public abstract bool CanAdminRejectsOrder();

		public abstract bool CanAdminManagesOrder();

		public abstract bool CanSellerConfirmsOrder();

		public abstract bool CanSellerRejectsOrder();

		public abstract bool CanSellerManagesOrder();

		public abstract bool CanChangeToThisModel(Product product, out ICollection<string> errors);

		public abstract bool CanLeaveThisModel(Product product, out ICollection<string> errors);
	}
}
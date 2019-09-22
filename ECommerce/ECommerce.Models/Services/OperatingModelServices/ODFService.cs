using ECommerce.Models.Entities.Sellers;
using ECommerce.Models.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace ECommerce.Models.Services.OperatingModelServices
{
	public class ODFService : OperatingModelService
	{
		public ODFService(ISellerRepository sellerRepository)
			: base(OperatingModel.ODF, sellerRepository) { }

		public override bool CanAdminAddProductQuantity(Product product, out ICollection<string> errors)
		{
			errors = new List<string>();
			errors.Add($"Admin can not add quantity for product with {ServiceOperatingModel} model");
			return false;
		}

		public override bool CanSellerAddProductQuantity(Product product, out ICollection<string> errors)
		{
			errors = new List<string>();

			//check product existence
			if(product==null)
			{
				errors.Add("Could not found product");
				return false;
			}
			
			return !errors.Any();
		}

		public override bool CanAdminReduceProductQuantity(Product product, out ICollection<string> errors)
		{
			errors = new List<string>();
			errors.Add($"Admin can not reduce quantity for product with {ServiceOperatingModel} model");
			return false;
		}

		public override bool CanSellerReduceProductQuantity(Product product, out ICollection<string> errors)
		{
			errors = new List<string>();

			//check product existence
			if (product == null)
			{
				errors.Add("Could not found product");
				return false;
			}

			return !errors.Any();
		}

		public override bool CanAdminConfirmsOrder() => false;

		public override bool CanAdminRejectsOrder() => false;

		public override bool CanAdminManagesOrder() => false;

		public override bool CanSellerConfirmsOrder() => true;

		public override bool CanSellerRejectsOrder() => true;

		public override bool CanSellerManagesOrder() => true;

		public override bool CanChangeToThisModel(Product product, out ICollection<string> errors)
		{
			errors = new List<string>();

			//check same model
			if (product.Model == ServiceOperatingModel)
			{
				errors.Add($"Model {ServiceOperatingModel} already have registered");
			}

			//check product quantity
			if (product.Quantity != 0)
			{
				errors.Add($"Quantity must be zero to change to {ServiceOperatingModel} model");
			}

			return !errors.Any();
		}

		public override bool CanLeaveThisModel(Product product, out ICollection<string> errors)
		{
			errors = new List<string>();
			return true;
		}
	}
}
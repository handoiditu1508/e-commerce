using ECommerce.Models.Entities.Sellers;
using ECommerce.Models.Messages;
using ECommerce.Models.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Models.Services.OperatingModelServices
{
	public class ODFService : OperatingModelService
	{
		public ODFService(ISellerRepository sellerRepository)
			: base(OperatingModel.ODF, sellerRepository) { }

		public override async Task<BoolMessage> CanAdminAddProductQuantityAsync(Product product)
		{
			BoolMessage message = new BoolMessage();
			message.Errors.Add($"Admin can not add quantity for product with {ServiceOperatingModel} model");
			message.Result = false;
			return message;
		}

		public override async Task<BoolMessage> CanSellerAddProductQuantityAsync(Product product)
		{
			BoolMessage message = new BoolMessage();

			//check product existence
			if(product==null)
			{
				message.Errors.Add("Could not found product");
				message.Result = false;
				return message;
			}

			if (message.Errors.Any())
				message.Result = false;
			else message.Result = true;

			return message;
		}

		public override async Task<BoolMessage> CanAdminReduceProductQuantityAsync(Product product)
		{
			BoolMessage message = new BoolMessage();
			message.Errors.Add($"Admin can not reduce quantity for product with {ServiceOperatingModel} model");
			message.Result = false;
			return message;
		}

		public override async Task<BoolMessage> CanSellerReduceProductQuantityAsync(Product product)
		{
			BoolMessage message = new BoolMessage();

			//check product existence
			if (product == null)
			{
				message.Errors.Add("Could not found product");
				message.Result = false;
				return message;
			}

			if (message.Errors.Any())
				message.Result = false;
			else message.Result = true;

			return message;
		}

		public override async Task<BoolMessage> CanAdminConfirmsOrderAsync() => new BoolMessage(false);

		public override async Task<BoolMessage> CanAdminRejectsOrderAsync() => new BoolMessage(false);

		public override async Task<BoolMessage> CanAdminManagesOrderAsync() => new BoolMessage(false);

		public override async Task<BoolMessage> CanSellerConfirmsOrderAsync() => new BoolMessage(false);

		public override async Task<BoolMessage> CanSellerRejectsOrderAsync() => new BoolMessage(false);

		public override async Task<BoolMessage> CanSellerManagesOrderAsync() => new BoolMessage(false);

		public override async Task<BoolMessage> CanChangeToThisModelAsync(Product product)
		{
			BoolMessage message = new BoolMessage();

			//check same model
			if (product.Model == ServiceOperatingModel)
			{
				message.Errors.Add($"Model {ServiceOperatingModel} already have registered");
			}

			//check product quantity
			if (product.Quantity != 0)
			{
				message.Errors.Add($"Quantity must be zero to change to {ServiceOperatingModel} model");
			}

			if (message.Errors.Any())
				message.Result = false;
			else message.Result = true;

			return message;
		}

		public override async Task<BoolMessage> CanLeaveThisModelAsync(Product product) => new BoolMessage(true);
	}
}
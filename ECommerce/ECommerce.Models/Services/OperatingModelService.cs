using ECommerce.Models.Entities.Sellers;
using ECommerce.Models.Messages;
using ECommerce.Models.Repositories;
using System.Threading.Tasks;

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

		public abstract Task<BoolMessage> CanAdminAddProductQuantityAsync(Product product);

		public abstract Task<BoolMessage> CanAdminReduceProductQuantityAsync(Product product);

		public abstract Task<BoolMessage> CanSellerAddProductQuantityAsync(Product product);

		public abstract Task<BoolMessage> CanSellerReduceProductQuantityAsync(Product product);

		public abstract Task<BoolMessage> CanAdminConfirmsOrderAsync();

		public abstract Task<BoolMessage> CanAdminRejectsOrderAsync();

		public abstract Task<BoolMessage> CanAdminManagesOrderAsync();

		public abstract Task<BoolMessage> CanSellerConfirmsOrderAsync();

		public abstract Task<BoolMessage> CanSellerRejectsOrderAsync();

		public abstract Task<BoolMessage> CanSellerManagesOrderAsync();

		public abstract Task<BoolMessage> CanChangeToThisModelAsync(Product product);

		public abstract Task<BoolMessage> CanLeaveThisModelAsync(Product product);
	}
}
using ECommerce.Models.Entities.Customers;
using ECommerce.Models.Entities.ProductTypes;
using ECommerce.Models.Entities.Sellers;
using ECommerce.Models.Messages;
using ECommerce.Models.Repositories;
using ECommerce.Models.Services.ServiceFactories;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Models.Services
{
	public class OrderService
	{
		private ICustomerRepository customerRepository;
		private ISellerRepository sellerRepository;
		private OperatingModelServiceFactory modelServiceFactory;

		public OrderService(ICustomerRepository customerRepository, ISellerRepository sellerRepository, OperatingModelServiceFactory modelServiceFactory)
		{
			this.customerRepository = customerRepository;
			this.sellerRepository = sellerRepository;
			this.modelServiceFactory = modelServiceFactory;
		}

		public async Task<BoolMessage> OrderAsync(int customerId, Order order)
		{
			BoolMessage message = new BoolMessage();

			//check order null
			if (order == null)
			{
				message.Errors.Add("Order can not be empty");
				message.Result = false;
				return message;
			}

			//check quantity
			if (order.Quantity < 1)
				message.Errors.Add("Quantity can not lower than 1");

			//check customer existence
			Customer customer = await customerRepository.GetByAsync(customerId);
			if (customer == null)
				message.Errors.Add("Could not found customer");
			else if (!customer.Active)//check customer active or locked
				message.Errors.Add("Customer is locked");

			//check product existence
			Product product = await sellerRepository.GetProductByAsync(order.SellerId, order.ProductTypeId);
			if (product == null)
				message.Errors.Add("Could not found product");
			else
			{
				if (product.Status != ProductStatus.Active)//check product status
					message.Errors.Add("Product is unavailable at the moment");

				if (!product.Active)//check product active or locked
					message.Errors.Add("Product is locked");

				if (product.ProductType.Status != ProductTypeStatus.Active)
					message.Errors.Add("Product type is unavailable at the moment");
			}

			if (!message.Errors.Any())
			{
				customer.Order(order);
				message.Result = true;
				return message;
			}
			message.Result = false;
			return message;
		}

		public BoolMessage CancelOrder(Order order)
		{
			BoolMessage message = new BoolMessage();

			//check order null
			if (order == null)
			{
				message.Errors.Add("Order can not be empty");
				message.Result = false;
				return message;
			}

			//check order status
			if (order.Status == OrderStatus.Shipped)
			{
				message.Errors.Add("Can not cancel a shipped order");
			}

			if (!message.Errors.Any())
			{
				order.Customer.CancelOrder(order);
				message.Result = true;
				return message;
			}
			message.Result = false;
			return message;
		}

		public async Task<BoolMessage> ConfirmOrderByAdminAsync(Order order)
		{
			BoolMessage message = new BoolMessage();

			//check order null
			if (order == null)
			{
				message.Errors.Add("Order can not be empty");
				message.Result = false;
				return message;
			}

			//check product operating model
			OperatingModelService modelService = modelServiceFactory
				.GetService((await sellerRepository.GetProductByAsync(order.SellerId, order.ProductTypeId)).Model);

			if (!(await modelService.CanAdminConfirmsOrderAsync()).Result)
			{
				message.Errors.Add("You don't have right to confirm this order");
			}

			//check order status
			if (order.Status != OrderStatus.Confirming)
			{
				message.Errors.Add("Can only confirm an order while firming it");
			}

			if (!message.Errors.Any())
			{
				order.Status = OrderStatus.Preparing;
				message.Result = true;
				return message;
			}
			message.Result = false;
			return message;
		}

		public async Task<BoolMessage> ConfirmOrderBySellerAsync(Order order)
		{
			BoolMessage message = new BoolMessage();

			//check order null
			if (order == null)
			{
				message.Errors.Add("Order can not be empty");
				message.Result = false;
				return message;
			}

			//check product operating model
			OperatingModelService modelService = modelServiceFactory
				.GetService((await sellerRepository.GetProductByAsync(order.SellerId, order.ProductTypeId)).Model);

			if (!(await modelService.CanSellerConfirmsOrderAsync()).Result)
			{
				message.Errors.Add("You don't have right to confirm this order");
			}

			//check order status
			if (order.Status != OrderStatus.Confirming)
			{
				message.Errors.Add("Can only confirm an order while firming it");
			}

			if (!message.Errors.Any())
			{
				order.Status = OrderStatus.Preparing;
				message.Result = true;
				return message;
			}
			message.Result = false;
			return message;
		}

		public async Task<BoolMessage> RejectOrderByAdminAsync(Order order)
		{
			BoolMessage message = new BoolMessage();

			//check order null
			if (order == null)
			{
				message.Errors.Add("Order can not be empty");
				message.Result = false;
				return message;
			}

			//check product operating model
			OperatingModelService modelService = modelServiceFactory
				.GetService((await sellerRepository.GetProductByAsync(order.SellerId, order.ProductTypeId)).Model);

			if (!(await modelService.CanAdminRejectsOrderAsync()).Result)
			{
				message.Errors.Add("You don't have right to reject this order");
			}

			//check order status
			if (order.Status != OrderStatus.Confirming)
			{
				message.Errors.Add("Can only reject an order while firming it");
			}

			if (!message.Errors.Any())
			{
				order.Seller.RejectOrder(order);
				message.Result = true;
				return message;
			}
			message.Result = false;
			return message;
		}

		public async Task<BoolMessage> RejectOrderBySellerAsync(Order order)
		{
			BoolMessage message = new BoolMessage();

			//check order null
			if (order == null)
			{
				message.Errors.Add("Order can not be empty");
				message.Result = false;
				return message;
			}

			//check product operating model
			OperatingModelService modelService = modelServiceFactory
				.GetService((await sellerRepository.GetProductByAsync(order.SellerId, order.ProductTypeId)).Model);

			if (!(await modelService.CanSellerRejectsOrderAsync()).Result)
			{
				message.Errors.Add("You don't have right to reject this order");
			}

			//check order status
			if (order.Status != OrderStatus.Confirming)
			{
				message.Errors.Add("Can only reject an order while firming it");
			}

			if (!message.Errors.Any())
			{
				order.Seller.RejectOrder(order);
				message.Result = true;
				return message;
			}
			message.Result = false;
			return message;
		}

		public async Task<BoolMessage> ChangeOrderStatusByAdminAsync(Order order, OrderStatus status)
		{
			BoolMessage message = new BoolMessage();

			//check order null
			if (order == null)
			{
				message.Errors.Add("Order can not be empty");
				message.Result = false;
				return message;
			}

			//check product operating model
			OperatingModelService modelService = modelServiceFactory
				.GetService((await sellerRepository.GetProductByAsync(order.SellerId, order.ProductTypeId)).Model);

			if (!(await modelService.CanAdminManagesOrderAsync()).Result)
			{
				message.Errors.Add("You don't have right to manage this order");
			}

			//check order status
			if (order.Status == OrderStatus.Confirming)
			{
				message.Errors.Add("Can not change an order which are waiting for confirmation");
			}

			if (!message.Errors.Any())
			{
				order.Status = status;
				message.Result = true;
				return message;
			}
			message.Result = false;
			return message;
		}

		public async Task<BoolMessage> ChangeOrderStatusBySellerAsync(Order order, OrderStatus status)
		{
			BoolMessage message = new BoolMessage();

			//check order null
			if (order == null)
			{
				message.Errors.Add("Order can not be empty");
				message.Result = false;
				return message;
			}

			//check product operating model
			OperatingModelService modelService = modelServiceFactory
				.GetService((await sellerRepository.GetProductByAsync(order.SellerId, order.ProductTypeId)).Model);

			if (!(await modelService.CanSellerManagesOrderAsync()).Result)
			{
				message.Errors.Add("You don't have right to manage this order");
			}

			//check order status
			if (order.Status == OrderStatus.Confirming)
			{
				message.Errors.Add("Can not change an order which are waiting for confirmation");
			}

			if (!message.Errors.Any())
			{
				order.Status = status;
				message.Result = true;
				return message;
			}
			message.Result = false;
			return message;
		}
	}
}
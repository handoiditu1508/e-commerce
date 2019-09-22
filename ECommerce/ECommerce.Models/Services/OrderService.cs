using ECommerce.Models.Entities.Customers;
using ECommerce.Models.Entities.ProductTypes;
using ECommerce.Models.Entities.Sellers;
using ECommerce.Models.Repositories;
using ECommerce.Models.Services.ServiceFactories;
using System.Collections.Generic;
using System.Linq;

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

		public bool TryOrder(int customerId, Order order, out ICollection<string> errors)
		{
			errors = new List<string>();

			//check order null
			if (order == null)
			{
				errors.Add("Order can not be empty");
				return false;
			}

			//check quantity
			if (order.Quantity < 1)
				errors.Add("Quantity can not lower than 1");

			//check customer existence
			Customer customer = customerRepository.GetBy(customerId);
			if (customer == null)
				errors.Add("Could not found customer");
			else if (!customer.Active)//check customer active or locked
				errors.Add("Customer is locked");

			//check product existence
			Product product = sellerRepository.GetProductBy(order.SellerId, order.ProductTypeId);
			if (product == null)
				errors.Add("Could not found product");
			else
			{
				if (product.Status != ProductStatus.Active)//check product status
					errors.Add("Product is unavailable at the moment");

				if (!product.Active)//check product active or locked
					errors.Add("Product is locked");

				if (product.ProductType.Status != ProductTypeStatus.Active)
					errors.Add("Product type is unavailable at the moment");
			}

			if (!errors.Any())
			{
				customer.Order(order);
				return true;
			}
			return false;
		}

		public bool TryCancelOrder(Order order, out ICollection<string> errors)
		{
			errors = new List<string>();

			//check order null
			if (order == null)
			{
				errors.Add("Order can not be empty");
				return false;
			}

			//check order status
			if (order.Status==OrderStatus.Shipped)
			{
				errors.Add("Can not cancel a shipped order");
			}

			if (!errors.Any())
			{
				order.Customer.CancelOrder(order);
				return true;
			}
			return false;
		}

		public bool TryConfirmOrderByAdmin(Order order, out ICollection<string> errors)
		{
			errors = new List<string>();

			//check order null
			if (order == null)
			{
				errors.Add("Order can not be empty");
				return false;
			}

			//check product operating model
			OperatingModelService modelService = modelServiceFactory
				.GetService(sellerRepository.GetProductBy(order.SellerId, order.ProductTypeId).Model);

			if (!modelService.CanAdminConfirmsOrder())
			{
				errors.Add("You don't have right to confirm this order");
			}

			//check order status
			if (order.Status != OrderStatus.Confirming)
			{
				errors.Add("Can only confirm an order while firming it");
			}

			if (!errors.Any())
			{
				order.Status = OrderStatus.Preparing;
				return true;
			}
			return false;
		}

		public bool TryConfirmOrderBySeller(Order order, out ICollection<string> errors)
		{
			errors = new List<string>();

			//check order null
			if (order == null)
			{
				errors.Add("Order can not be empty");
				return false;
			}

			//check product operating model
			OperatingModelService modelService = modelServiceFactory
				.GetService(sellerRepository.GetProductBy(order.SellerId, order.ProductTypeId).Model);

			if (!modelService.CanSellerConfirmsOrder())
			{
				errors.Add("You don't have right to confirm this order");
			}

			//check order status
			if (order.Status != OrderStatus.Confirming)
			{
				errors.Add("Can only confirm an order while firming it");
			}

			if (!errors.Any())
			{
				order.Status = OrderStatus.Preparing;
				return true;
			}
			return false;
		}

		public bool TryRejectOrderByAdmin(Order order, out ICollection<string> errors)
		{
			errors = new List<string>();

			//check order null
			if (order == null)
			{
				errors.Add("Order can not be empty");
				return false;
			}

			//check product operating model
			OperatingModelService modelService = modelServiceFactory
				.GetService(sellerRepository.GetProductBy(order.SellerId, order.ProductTypeId).Model);

			if(!modelService.CanAdminRejectsOrder())
			{
				errors.Add("You don't have right to reject this order");
			}

			//check order status
			if (order.Status != OrderStatus.Confirming)
			{
				errors.Add("Can only reject an order while firming it");
			}

			if (!errors.Any())
			{
				order.Seller.RejectOrder(order);
				return true;
			}
			return false;
		}

		public bool TryRejectOrderBySeller(Order order, out ICollection<string> errors)
		{
			errors = new List<string>();

			//check order null
			if (order == null)
			{
				errors.Add("Order can not be empty");
				return false;
			}

			//check product operating model
			OperatingModelService modelService = modelServiceFactory
				.GetService(sellerRepository.GetProductBy(order.SellerId, order.ProductTypeId).Model);

			if (!modelService.CanSellerRejectsOrder())
			{
				errors.Add("You don't have right to reject this order");
			}

			//check order status
			if (order.Status != OrderStatus.Confirming)
			{
				errors.Add("Can only reject an order while firming it");
			}

			if (!errors.Any())
			{
				order.Seller.RejectOrder(order);
				return true;
			}
			return false;
		}

		public bool TryChangeOrderStatusByAdmin(Order order, OrderStatus status, out ICollection<string> errors)
		{
			errors = new List<string>();

			//check order null
			if (order == null)
			{
				errors.Add("Order can not be empty");
				return false;
			}

			//check product operating model
			OperatingModelService modelService = modelServiceFactory
				.GetService(sellerRepository.GetProductBy(order.SellerId, order.ProductTypeId).Model);

			if (!modelService.CanAdminManagesOrder())
			{
				errors.Add("You don't have right to manage this order");
			}

			//check order status
			if (order.Status == OrderStatus.Confirming)
			{
				errors.Add("Can not change an order which are waiting for confirmation");
			}

			if (!errors.Any())
			{
				order.Status = status;
				return true;
			}
			return false;
		}

		public bool TryChangeOrderStatusBySeller(Order order, OrderStatus status, out ICollection<string> errors)
		{
			errors = new List<string>();

			//check order null
			if (order == null)
			{
				errors.Add("Order can not be empty");
				return false;
			}

			//check product operating model
			OperatingModelService modelService = modelServiceFactory
				.GetService(sellerRepository.GetProductBy(order.SellerId, order.ProductTypeId).Model);

			if (!modelService.CanSellerManagesOrder())
			{
				errors.Add("You don't have right to manage this order");
			}

			//check order status
			if (order.Status == OrderStatus.Confirming)
			{
				errors.Add("Can not change an order which are waiting for confirmation");
			}

			if (!errors.Any())
			{
				order.Status = status;
				return true;
			}
			return false;
		}
	}
}
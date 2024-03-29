﻿using ECommerce.Application.Extensions;
using ECommerce.Application.Extensions.AddModels;
using ECommerce.Application.Extensions.UpdateModels;
using ECommerce.Application.Services;
using ECommerce.Application.WorkingModels.AddModels;
using ECommerce.Application.WorkingModels.UpdateModels;
using ECommerce.Application.WorkingModels.Views;
using ECommerce.Extensions;
using ECommerce.Infrastructure.UnitOfWork;
using ECommerce.Models.Entities.Admins;
using ECommerce.Models.Entities.Categories;
using ECommerce.Models.Entities.Customers;
using ECommerce.Models.Entities.ProductTypes;
using ECommerce.Models.Entities.Sellers;
using ECommerce.Models.Entities.Users;
using ECommerce.Models.Messages;
using ECommerce.Models.Repositories;
using ECommerce.Models.SearchModels;
using ECommerce.Models.Services;
using ECommerce.Models.Services.ServiceFactories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Application
{
	public class ECommerceService
	{
		private IUnitOfWork unitOfWork;
		private IAdminRepository adminRepository => unitOfWork.GetAdminRepository();
		private ICategoryRepository categoryRepository => unitOfWork.GetCategoryRepository();
		private ICustomerRepository customerRepository => unitOfWork.GetCustomerRepository();
		private IProductTypeRepository productTypeRepository => unitOfWork.GetProductTypeRepository();
		private ISellerRepository sellerRepository => unitOfWork.GetSellerRepository();
		private IUserRepository userRepository => unitOfWork.GetUserRepository();

		private OrderService orderService;
		private RegisterProductService registerProductService;
		private UpdateProductTypeService updateProductTypeService;
		private OperatingModelServiceFactory operatingModelServiceFactory;

		public ECommerceService(IUnitOfWork unitOfWork)
		{
			this.unitOfWork = unitOfWork;
		}

		private OperatingModelServiceFactory OperatingModelServiceFactory
		{
			get
			{
				if (operatingModelServiceFactory == null)
					operatingModelServiceFactory = new OperatingModelServiceFactory(sellerRepository);
				return operatingModelServiceFactory;
			}
		}

		private OrderService OrderService
		{
			get
			{
				if (orderService == null)
					orderService = new OrderService(customerRepository, sellerRepository, OperatingModelServiceFactory);
				return orderService;
			}
		}

		private RegisterProductService RegisterProductService
		{
			get
			{
				if (registerProductService == null)
					registerProductService = new RegisterProductService(sellerRepository, productTypeRepository, OperatingModelServiceFactory);
				return registerProductService;
			}
		}

		private UpdateProductTypeService UpdateProductTypeService
		{
			get
			{
				if (updateProductTypeService == null)
					updateProductTypeService = new UpdateProductTypeService(sellerRepository, productTypeRepository, categoryRepository);
				return updateProductTypeService;
			}
		}

		#region Admin
		private BoolMessage ValidateAdmin(Admin admin)
		{
			BoolMessage message = new BoolMessage();

			if (message.Errors.Any())
				message.Result = false;
			else message.Result = true;

			return message;
		}

		public async Task<Message<AdminView>> AddAdminAsync(AdminAddModel addModel)
		{
			var message = new Message<AdminView>(null);
			Admin admin = addModel.ConvertToEntity();

			var validationMessage = ValidateUser(admin.User, true, true);
			if(validationMessage.Result)
			{
				validationMessage = ValidateAdmin(admin);
				if (validationMessage.Result)
				{
					await adminRepository.AddAsync(admin);
					await unitOfWork.CommitAsync();
					message.Result = admin.ConvertToView();
					return message;
				}
			}
			
			message.Errors = validationMessage.Errors;
			return message;
		}

		public async Task<AdminView> GetAdminByAsync(int adminId)
			=> (await adminRepository.GetByAsync(adminId))?.ConvertToView() ?? null;

		public async Task<AdminUpdateModel> GetAdminUpdateModelByAsync(int adminId)
			=> (await adminRepository.GetByAsync(adminId))?.ConvertToUpdateModel() ?? null;

		public AdminUpdateModel GetAdminUpdateModelBy(string adminEmail)
			=> adminRepository.GetBy(adminEmail)?.ConvertToUpdateModel() ?? null;

		public AdminView GetAdminBy(string adminEmail)
			=> adminRepository.GetBy(adminEmail)?.ConvertToView() ?? null;

		public IEnumerable<AdminView> GetAdminsBy(AdminSearchModel searchModel, int? startIndex, short? length)
		{
			IEnumerable<Admin> admins = adminRepository
			.GetBy(searchModel);

			if (startIndex != null && startIndex > -1)
				admins = admins.Skip((int)startIndex);
			if (length != null && length > -1)
				admins = admins.Take((int)length);

			return admins.ConvertToViews();
		}

		public int CountAdminsBy(AdminSearchModel searchModel)
			=> adminRepository
				.GetBy(searchModel)
				.Count();

		public async Task<Message> UpdateAdminAsync(int adminId, AdminUpdateModel updateModel)
		{
			Message message = new Message();
			Admin admin = updateModel.ConvertToEntity();

			var validationMessage = ValidateUser(admin.User, false, false);
			if (validationMessage.Result)
			{
				validationMessage = ValidateAdmin(admin);
				if (validationMessage.Result)
				{
					if ((await adminRepository.GetByAsync(adminId)) != null)
					{
						await userRepository.UpdateAsync(adminId, admin.User);
						await adminRepository.UpdateAsync(adminId, admin);
						await unitOfWork.CommitAsync();
					}
					else message.Errors.Add("Could not found admin");
				}
				else message.Errors = validationMessage.Errors;
			}
			else message.Errors = validationMessage.Errors;

			return message;
		}

		public async Task DeleteAdminAsync(int adminId)
		{
			await adminRepository.DeleteAsync(adminId);
			await unitOfWork.CommitAsync();
		}

		public async Task DeleteAdminAsync(Admin admin)
		{
			adminRepository.Delete(admin);
			await unitOfWork.CommitAsync();
		}
		#endregion

		#region Category
		private BoolMessage ValidateCategory(Category category)
		{
			BoolMessage message = new BoolMessage();

			if (category == null)
			{
				message.Errors.Add("Can not validate an empty instance");
				message.Result = false;
				return message;
			}

			if (string.IsNullOrWhiteSpace(category.Name))
			{
				message.Errors.Add("Name can not be empty");
			}
			else
			{
				category.Name = category.Name
					.Trim()
					.RemoveMultipleSpaces();
			}

			if (message.Errors.Any())
				message.Result = false;
			else message.Result = true;

			return message;
		}

		public async Task<Message<CategoryView>> AddCategoryAsync(CategoryAddModel addModel, int? parentId)
		{
			var message = new Message<CategoryView>();
			Category category = addModel.ConvertToEntity();

			var validationMessage = ValidateCategory(category);
			if (!validationMessage.Result)
			{
				message.Errors = validationMessage.Errors;
				return message;
			}

			if (parentId != null)
			{
				Category parentCategory = await categoryRepository.GetByAsync((int)parentId);
				if (parentCategory == null)
				{
					message.Errors.Add("Parent was not found");
					return message;
				}

				if (parentCategory.ChildCategories.Select(c => c.Name).Contains(category.Name))
				{
					message.Errors.Add($"Parent already has a child named \"{category.Name}\"");
					return message;
				}
			}
			else if (categoryRepository.GetRoots().Select(c => c.Name).Contains(category.Name))
			{
				message.Errors.Add("Root has already existed");
				return message;
			}

			category.ParentId = parentId;

			await categoryRepository.AddAsync(category);
			await unitOfWork.CommitAsync();
			message.Result = category.ConvertToView();
			return message;
		}

		public IEnumerable<CategoryView> GetAllRootCategories()
			=> categoryRepository.GetRoots().ConvertToViews();

		public async Task<CategoryView> GetCategoryByAsync(int categoryId)
			=> (await categoryRepository.GetByAsync(categoryId))?.ConvertToView() ?? null;

		public async Task<CategoryUpdateModel> GetCategoryUpdateModelByAsync(int categoryId)
			=> (await categoryRepository.GetByAsync(categoryId))?.ConvertToUpdateModel() ?? null;

		public async Task<CategoryView> GetCategoryByProductTypeIdAsync(int productTypeId)
			=> (await productTypeRepository.GetByAsync(productTypeId))?.Category.ConvertToView() ?? null;

		public async Task<CategoryView> GetParentCategoryAsync(int categoryId)
		{
			Category category = (await categoryRepository.GetByAsync(categoryId));
			return category?.ParentCategory?.ConvertToView() ?? null;
		}

		public CategoryView GetLastCategory() => categoryRepository.GetAll().LastOrDefault().ConvertToView();

		public async Task<IEnumerable<CategoryView>> GetChildCategoriesAsync(int categoryId)
		{
			Category category = await categoryRepository.GetByAsync(categoryId);
			return category?.ChildCategories.ConvertToViews() ?? null;
		}

		public async Task<Message> UpdateCategoryAsync(int categoryId, CategoryUpdateModel updateModel)
		{
			Message message = new Message();
			Category category = updateModel.ConvertToEntity();

			var validationMessage = ValidateCategory(category);
			if (validationMessage.Result)
			{
				if (categoryRepository.GetByAsync(categoryId) != null)
				{
					await categoryRepository.UpdateAsync(categoryId, category);
					await unitOfWork.CommitAsync();
				}
				else message.Errors.Add("Could not found category");
			}
			else
			{
				message.Errors = validationMessage.Errors;
			}

			return message;
		}

		public async Task DeleteCategoryAsync(int categoryId)
		{
			await categoryRepository.DeleteAsync(categoryId);
			await unitOfWork.CommitAsync();
		}

		public void DeleteCategory(Category category)
		{
			categoryRepository.Delete(category);
			categoryRepository.CommitAsync();
		}
		#endregion

		#region Customer
		private BoolMessage ValidateCustomer(Customer customer)
		{
			BoolMessage message = new BoolMessage();

			if (message.Errors.Any())
				message.Result = false;
			else message.Result = true;

			return message;
		}

		public async Task<Message<CustomerView>> AddCustomerAsync(CustomerAddModel addModel)
		{
			var message = new Message<CustomerView>();
			Customer customer = addModel.ConvertToEntity();

			var validationMessage = ValidateUser(customer.User, true, true);
			if (validationMessage.Result)
			{
				validationMessage = ValidateCustomer(customer);
				if (validationMessage.Result)
				{
					await customerRepository.AddAsync(customer);
					await unitOfWork.CommitAsync();
					message.Result = customer.ConvertToView();
					return message;
				}
			}

			message.Errors = validationMessage.Errors;
			return message;
		}

		public async Task<CustomerView> GetCustomerByAsync(int customerId)
			=> (await customerRepository.GetByAsync(customerId))?.ConvertToView() ?? null;

		public CustomerView GetCustomerBy(string customerEmail)
			=> customerRepository.GetBy(customerEmail)?.ConvertToView() ?? null;

		public async Task<CustomerUpdateModel> GetCustomerCustomerUpdateModelByAsync(int customerId)
			=> (await customerRepository.GetByAsync(customerId))?.ConvertToUpdateModel() ?? null;

		public CustomerUpdateModel GetCustomerCustomerUpdateModelBy(string customerEmail)
			=> customerRepository.GetBy(customerEmail)?.ConvertToUpdateModel() ?? null;

		public async Task<CustomerView> GetCustomerByOrderIdAsync(int orderId)
			=> (await customerRepository.GetOrderByAsync(orderId))?.Customer.ConvertToView() ?? null;

		public IEnumerable<CustomerView> GetCustomersBy(CustomerSearchModel searchModel, int? startIndex, short? length)
		{
			IEnumerable<Customer> customers = customerRepository.GetBy(searchModel);

			if (startIndex != null && startIndex > -1)
				customers = customers.Skip((int)startIndex);
			if (length != null && length > -1)
				customers = customers.Take((int)length);

			return customers.ConvertToViews();
		}

		public int CountCustomersBy(CustomerSearchModel searchModel)
			=> customerRepository
				.GetBy(searchModel)
				.Count();

		public async Task<Message> UpdateCustomerAsync(int customerId, CustomerUpdateModel updateModel)
		{
			Message message = new Message();
			Customer customer = updateModel.ConvertToEntity();

			var validationMessage = ValidateUser(customer.User, false, false);
			if (validationMessage.Result)
			{
				validationMessage = ValidateCustomer(customer);
				if (validationMessage.Result)
				{
					if ((await customerRepository.GetByAsync(customerId)) != null)
					{
						await userRepository.UpdateAsync(customerId, customer.User);
						await customerRepository.UpdateAsync(customerId, customer);
						await unitOfWork.CommitAsync();
					}
					else message.Errors.Add("Could not found customer");
				}
				else message.Errors = validationMessage.Errors;
			}
			else message.Errors = validationMessage.Errors;

			return message;
		}

		public async Task<Message> UpdateCustomerActiveAsync(int customerId, bool active)
		{
			Message message = new Message();
			Customer customer = await customerRepository.GetByAsync(customerId);
			if (customer != null)
			{
				customer.Active = active;
				await unitOfWork.CommitAsync();
			}
			else message.Errors.Add("Could not found customer");

			return message;
		}

		public async Task DeleteCustomerAsync(int customerId)
		{
			await customerRepository.DeleteAsync(customerId);
			await unitOfWork.CommitAsync();
		}

		public async Task DeleteCustomerAsync(Customer customer)
		{
			customerRepository.Delete(customer);
			await unitOfWork.CommitAsync();
		}
		#endregion

		#region Order
		public async Task<Message<OrderView>> AddOrderAsync(int customerId, OrderAddModel addModel)
		{
			var message = new Message<OrderView>();
			Order order = addModel.ConvertToEntity();
			var orderMessage = await OrderService.OrderAsync(customerId, order);
			if (orderMessage.Result)
			{
				await unitOfWork.CommitAsync();
				message.Result = order.ConvertToView();
				return message;
			}
			else message.Errors = orderMessage.Errors;
			return message;
		}

		public async Task<Message> CancelOrderByAdminAsync(int orderId)
		{
			Message message = new Message();

			Order order = await customerRepository.GetOrderByAsync(orderId);

			var orderMessage = await OrderService.CancelOrderByAdminAsync(order);

			if (orderMessage.Result)
				await unitOfWork.CommitAsync();
			else message.Errors = orderMessage.Errors;

			return message;
		}

		public async Task<Message> CancelOrderBySellerAsync(int orderId)
		{
			Message message = new Message();

			Order order = await customerRepository.GetOrderByAsync(orderId);

			var orderMessage = await OrderService.CancelOrderBySellerAsync(order);

			if (orderMessage.Result)
				await unitOfWork.CommitAsync();
			else message.Errors = orderMessage.Errors;

			return message;
		}

		public async Task<Message<OrderStatus>> ChangeOrderStatusByAdminAsync(int orderId, OrderStatus status)
		{
			var message = new Message<OrderStatus>();

			Order order = await customerRepository.GetOrderByAsync(orderId);
			if (order == null)
			{
				message.Errors.Add("Could not found order");
				return message;
			}
			else message.Result = order.Status;

			var orderMessage = await OrderService.ChangeOrderStatusByAdminAsync(order, status);

			if (orderMessage.Result)
			{
				await unitOfWork.CommitAsync();
				message.Result = status;
			}
			else message.Errors = orderMessage.Errors;

			return message;
		}

		public async Task<Message<OrderStatus>> ChangeOrderStatusBySellerAsync(int orderId, OrderStatus status)
		{
			var message = new Message<OrderStatus>();

			Order order = await customerRepository.GetOrderByAsync(orderId);
			if (order == null)
			{
				message.Errors.Add("Could not found order");
				return message;
			}
			else message.Result = order.Status;

			var orderMessage = await OrderService.ChangeOrderStatusBySellerAsync(order, status);

			if (orderMessage.Result)
			{
				await unitOfWork.CommitAsync();
				message.Result = status;
			}
			else message.Errors = orderMessage.Errors;

			return message;
		}

		public async Task<OrderView> GetOrderByAsync(int orderId)
			=> (await customerRepository.GetOrderByAsync(orderId))?.ConvertToView() ?? null;

		public IEnumerable<OrderView> GetOrdersByCustomerId(OrderSearchModel searchModel, int? startIndex, short? length)
		{
			IEnumerable<Order> orders = customerRepository.GetOrdersBy(searchModel);

			if (startIndex != null && startIndex > -1)
				orders = orders.Skip((int)startIndex);
			if (length != null && length > -1)
				orders = orders.Take((int)length);

			return orders.ConvertToViews();
		}

		public IEnumerable<OrderView> GetOrdersBySellerId(OrderSearchModel searchModel, int? startIndex, short? length)
		{
			IEnumerable<Order> orders = sellerRepository.GetOrdersBy(searchModel);

			if (startIndex != null && startIndex > -1)
				orders = orders.Skip((int)startIndex);
			if (length != null && length > -1)
				orders = orders.Take((int)length);

			return orders.ConvertToViews();
		}

		public IEnumerable<OrderView> GetOrdersByProductTypeId(OrderSearchModel searchModel, int? startIndex, short? length)
		{
			IEnumerable<Order> orders = productTypeRepository.GetOrdersBy(searchModel);

			if (startIndex != null && startIndex > -1)
				orders = orders.Skip((int)startIndex);
			if (length != null && length > -1)
				orders = orders.Take((int)length);

			return orders.ConvertToViews();
		}

		public IEnumerable<OrderView> GetOrders(OrderSearchModel searchModel, int? startIndex, short? length)
		{
			IEnumerable<Order> orders = customerRepository.GetAllOrdersBy(searchModel);

			if (startIndex != null && startIndex > -1)
				orders = orders.Skip((int)startIndex);
			if (length != null && length > -1)
				orders = orders.Take((int)length);

			return orders.ConvertToViews();
		}

		public int CountOrdersByCustomerId(OrderSearchModel searchModel)
			=> customerRepository
				.GetOrdersBy(searchModel)
				.Count();

		public int CountOrdersBySellerId(OrderSearchModel searchModel)
			=> sellerRepository.GetOrdersBy(searchModel).Count();

		public int CountOrdersByProductTypeId(OrderSearchModel searchModel)
			=> productTypeRepository.GetOrdersBy(searchModel).Count();

		public int CountOrders(OrderSearchModel searchModel)
			=> customerRepository.GetAllOrdersBy(searchModel).Count();
		#endregion

		#region Product Type
		private async Task<BoolMessage> ValidateProductTypeAsync(ProductType productType)
		{
			BoolMessage message = new BoolMessage();

			if (productType == null)
			{
				message.Errors.Add("Can not validate an empty instance");
				message.Result = false;
				return message;
			}

			if (string.IsNullOrWhiteSpace(productType.Name))
			{
				message.Errors.Add("Name can not be empty");
			}
			else productType.Name = productType.Name.Trim().RemoveMultipleSpaces();

			Category category = await categoryRepository.GetByAsync(productType.CategoryId);
			if (category == null)
			{
				message.Errors.Add("Category can not be empty");
			}
			else if (category.ChildCategories.Any())
			{
				message.Errors.Add("Category must has no child");
			}

			if (message.Errors.Any())
				message.Result = false;
			else message.Result = true;

			return message;
		}

		public async Task<Message<ProductTypeView>> AddProductTypeAsync(ProductTypeAddModel addModel)
		{
			var message = new Message<ProductTypeView>();
			ProductType productType = addModel.ConvertToEntity();
			var validationMessage = await ValidateProductTypeAsync(productType);

			if (validationMessage.Result)
			{
				await productTypeRepository.AddAsync(productType);
				await unitOfWork.CommitAsync();
				message.Result = productType.ConvertToView();
				return message; ;
			}
			else message.Errors = validationMessage.Errors;
			return message;
		}

		public async Task<ProductTypeView> GetProductTypeByAsync(int productTypeId)
			=> (await productTypeRepository.GetByAsync(productTypeId))?.ConvertToView() ?? null;

		public async Task<ProductTypeUpdateModel> GetProductTypeUpdateModelByAsync(int productTypeId)
			=> (await productTypeRepository.GetByAsync(productTypeId))?.ConvertToUpdateModel() ?? null;

		public async Task<IEnumerable<ProductTypeView>> GetProductTypesByAsync(ProductTypeSearchModel searchModel, int? startIndex, short? length)
		{
			IEnumerable<ProductType> productTypes = await productTypeRepository.GetByAsync(searchModel);

			if (startIndex != null && startIndex > -1)
				productTypes = productTypes.Skip((int)startIndex);
			if (length != null && length > -1)
				productTypes = productTypes.Take((int)length);

			return productTypes.ConvertToViews();
		}

		public async Task<int> CountProductTypesByAsync(ProductTypeSearchModel searchModel)
		{
			IEnumerable<ProductType> productTypes = await productTypeRepository.GetByAsync(searchModel);

			if (searchModel.DateModified != null)
				productTypes = productTypes
					.Where(p => p.DateModified.Date == searchModel.DateModified?.Date);

			if (searchModel.HasActiveProduct != null)
				productTypes = productTypes
					.Where(pt => pt.Products
						.Any(p => p.Active == searchModel.HasActiveProduct));

			if (searchModel.ProductStatus != null)
				productTypes = productTypes
					.Where(pt => pt.Products
						.Any(p => p.Status == searchModel.ProductStatus));

			return productTypes.Count();
		}

		public async Task<Message> UpdateProductTypeAsync(int productTypeId, ProductTypeUpdateModel updateModel)
		{
			Message message = new Message();
			ProductType productType = updateModel.ConvertToEntity();
			var validationMessage = await ValidateProductTypeAsync(productType);

			if (validationMessage.Result)
			{
				if ((await productTypeRepository.GetByAsync(productTypeId)) != null)
				{
					await productTypeRepository.UpdateAsync(productTypeId, productType);
					await unitOfWork.CommitAsync();
				}
				else message.Errors.Add("Could not found product type");
			}
			else message.Errors = validationMessage.Errors;

			return message;
		}

		public async Task<Message> UpdateProductTypeStatusAsync(int productTypeId, ProductTypeStatus status)
		{
			Message message = new Message();
			ProductType productType = await productTypeRepository.GetByAsync(productTypeId);
			if (productType != null)
			{
				(await productTypeRepository.GetByAsync(productTypeId)).Status = status;
				await unitOfWork.CommitAsync();
			}
			else message.Errors.Add("Could not found product type");

			return message;
		}

		public async Task<Message> DeleteProductTypeAsync(int productTypeId)
		{
			Message message = new Message();

			ProductType productType = await productTypeRepository.GetByAsync(productTypeId);
			if (productType != null)
			{
				message.Errors.Add("Could not found product type");
			}
			else
			{
				if (productType.Products.Any())
					message.Errors.Add("Can not delete a product type that has been registered");

				if (productTypeRepository.GetOrdersBy(new OrderSearchModel { ProductTypeId = productTypeId }).Any())
					message.Errors.Add("Can not delete a product type that has been ordered");

				if (!message.Errors.Any())
				{
					productTypeRepository.Delete(productType);
					await unitOfWork.CommitAsync();
				}
			}

			return message;
		}

		public async Task<Message> DeleteProductTypeAsync(ProductType productType)
		{
			Message message = new Message();

			if (productType != null)
			{
				message.Errors.Add("Could not found product type");
			}
			else
			{
				if (productType.Products.Any())
					message.Errors.Add("Can not delete a product type that has been registered");

				if (productType.Orders.Any())
					message.Errors.Add("Can not delete a product type that has been ordered");

				if (!message.Errors.Any())
				{
					productTypeRepository.Delete(productType);
					await unitOfWork.CommitAsync();
				}
			}

			return message;
		}
		#endregion

		#region Product Type Update Request
		public async Task<IEnumerable<ProductTypeUpdateRequestView>> GetProductTypeUpdateRequestsAsync(ProductTypeUpdateRequestSearchModel searchModel, int? startIndex, short? length)
		{
			IEnumerable<ProductTypeUpdateRequest> updateRequests = await productTypeRepository.GetAllUpdateRequestsByAsync(searchModel);

			if (startIndex != null && startIndex > -1)
				updateRequests = updateRequests.Skip((int)startIndex);
			if (length != null && length > -1)
				updateRequests = updateRequests.Take((int)length);

			return updateRequests.ConvertToViews();
		}

		public async Task<int> CountProductTypeUpdateRequestsAsync(ProductTypeUpdateRequestSearchModel searchModel) => (await productTypeRepository.GetAllUpdateRequestsByAsync(searchModel)).Count();

		public async Task<IEnumerable<ProductTypeUpdateRequestView>> GetProductTypeUpdateRequestsByProductTypeIdAsync(ProductTypeUpdateRequestSearchModel searchModel, int? startIndex, short? length)
		{
			IEnumerable<ProductTypeUpdateRequest> updateRequests = await productTypeRepository.GetUpdateRequestsByAsync(searchModel);

			if (startIndex != null && startIndex > -1)
				updateRequests = updateRequests.Skip((int)startIndex);
			if (length != null && length > -1)
				updateRequests = updateRequests.Take((int)length);

			return updateRequests.ConvertToViews();
		}

		public async Task<int> CountProductTypeUpdateRequestsByProductTypeIdAsync(ProductTypeUpdateRequestSearchModel searchModel)
			=> (await productTypeRepository.GetUpdateRequestsByAsync(searchModel)).Count();

		public async Task<IEnumerable<ProductTypeUpdateRequestView>> GetProductTypeUpdateRequestsBySellerIdAsync(ProductTypeUpdateRequestSearchModel searchModel, int? startIndex, short? length)
		{
			IEnumerable<ProductTypeUpdateRequest> updateRequests = await sellerRepository.GetProductTypeUpdateRequestsAsync(searchModel);

			if (startIndex != null && startIndex > -1)
				updateRequests = updateRequests.Skip((int)startIndex);
			if (length != null && length > -1)
				updateRequests = updateRequests.Take((int)length);

			return updateRequests.ConvertToViews();
		}

		public async Task<int> CountProductTypeUpdateRequestsBySellerIdAsync(ProductTypeUpdateRequestSearchModel searchModel)
			=> (await sellerRepository.GetProductTypeUpdateRequestsAsync(searchModel)).Count();

		public async Task<ProductTypeUpdateRequestView> GetProductTypeUpdateRequestByAsync(int sellerId, int productTypeId)
		{
			return (await productTypeRepository.GetUpdateRequestByAsync(sellerId, productTypeId))?.ConvertToView();
		}

		public async Task<Message<ProductTypeUpdateRequestView>> RequestAnUpdateForProductTypeAsync(int sellerId, ProductTypeUpdateRequestAddModel addModel)
		{
			var message = new Message<ProductTypeUpdateRequestView>();
			ProductTypeUpdateRequest updateRequest = addModel.ConvertToEntity();
			var updateRequestMessage = await UpdateProductTypeService.RequestAnUpdateAsync(sellerId, updateRequest);
			if (updateRequestMessage.Result)
			{
				await unitOfWork.CommitAsync();
				message.Result = updateRequest.ConvertToView();
				return message;
			}
			else message.Errors = updateRequestMessage.Errors;
			return message;
		}

		public async Task<Message> DeclineAnUpdateForProductTypeAsync(int sellerId, int productTypeId)
		{
			var message = new Message();
			var updateRequestMessage = await UpdateProductTypeService.DeclineAnUpdateAsync(sellerId, productTypeId);
			if (updateRequestMessage.Result)
			{
				await unitOfWork.CommitAsync();
				return message;
			}
			else message.Errors = updateRequestMessage.Errors;
			return message;
		}

		public async Task<Message> ApplyAnUpdateForProductTypeAsync(int sellerId, int productTypeId)
		{
			var message = new Message();
			var updateRequestMessage = await UpdateProductTypeService.ApplyAnUpdateAsync(sellerId, productTypeId);
			if (updateRequestMessage.Result)
			{
				await unitOfWork.CommitAsync();
				return message;
			}
			else message.Errors = updateRequestMessage.Errors;
			return message;
		}
		#endregion

		#region Product
		public async Task<Message<ProductView>> RegisterProductAsync(int sellerId, ProductAddModel addModel)
		{
			var message = new Message<ProductView>();

			Product product = addModel.ConvertToEntity();
			var productMessage = await RegisterProductService.RegisterAsync(sellerId, product);
			if (productMessage.Result)
			{
				await unitOfWork.CommitAsync();
				message.Result = product.ConvertToView();
				return message;
			}
			else message.Errors = productMessage.Errors;

			return message;
		}

		public async Task<Message> UnregisterProductAsync(int sellerId, int productTypeId)
		{
			Message message = new Message();
			var productMessage = await RegisterProductService.UnregisterAsync(sellerId, productTypeId);

			if (productMessage.Result)
				await unitOfWork.CommitAsync();
			else message.Errors = productMessage.Errors;

			return message;
		}

		public async Task<Message> ChangeProductOperatingModelAsync(int sellerId, int productTypeId, OperatingModel model)
		{
			Message message = new Message();
			var productMessage = await RegisterProductService.ChangeOperatingModelAsync(sellerId, productTypeId, model);

			if (productMessage.Result)
				await unitOfWork.CommitAsync();
			else message.Errors = productMessage.Errors;

			return message;
		}

		public ProductView GetProductBy(int sellerId, int productTypeId)
			=> sellerRepository.GetProductBy(sellerId, productTypeId)?.ConvertToView() ?? null;

		public async Task<ProductView> GetProductByAsync(int sellerId, int productTypeId)
			=> (await sellerRepository.GetProductByAsync(sellerId, productTypeId))?.ConvertToView() ?? null;

		public async Task<ProductUpdateModel> GetProductUpdateModelByAsync(int sellerId, int productTypeId)
			=> (await sellerRepository.GetProductByAsync(sellerId, productTypeId))?.ConvertToUpdateModel() ?? null;

		public async Task<ProductView> GetProductByOrderIdAsync(int orderId)
		{
			Order order = await customerRepository.GetOrderByAsync(orderId);
			return order != null ?
				(await sellerRepository.GetProductByAsync(order.SellerId, order.ProductTypeId))?
					.ConvertToView() ??
					null :
				null;
		}

		public ProductView GetRepresentativeProduct(int productTypeId)
		{
			var products = productTypeRepository.GetProductsBy(new ProductSearchModel { ProductTypeId = productTypeId });
			return products.LastOrDefault(p => p.Active && p.Status == ProductStatus.Active)?
				.ConvertToView() ??
				products.LastOrDefault()?
					.ConvertToView() ??
					null;
		}

		public async Task<IEnumerable<ProductView>> GetProductsBySellerIdAsync(ProductSearchModel searchModel, int? startIndex, short? length)
		{
			IEnumerable<Product> products = await sellerRepository.GetProductsByAsync(searchModel);

			if (startIndex != null && startIndex > -1)
				products = products.Skip((int)startIndex);
			if (length != null && length > -1)
				products = products.Take((int)length);

			return products.ConvertToViews();
		}

		public async Task<IEnumerable<ProductView>> GetProductsDistinctAsync(ProductSearchModel searchModel, int? startIndex, short? length)
		{
			IEnumerable<Product> products = await productTypeRepository.GetProductsDistinctAsync(searchModel);

			if (startIndex != null && startIndex > -1)
				products = products.Skip((int)startIndex);
			if (length != null && length > -1)
				products = products.Take((int)length);

			return products.ConvertToViews();
		}

		public async Task<IEnumerable<ProductView>> GetProductsAsync(ProductSearchModel searchModel, int? startIndex, short? length)
		{
			IEnumerable<Product> products = await sellerRepository.GetAllProductsByAsync(searchModel);

			if (startIndex != null && startIndex > -1)
				products = products.Skip((int)startIndex);
			if (length != null && length > -1)
				products = products.Take((int)length);

			return products.ConvertToViews();
		}

		public IEnumerable<ProductView> GetProductsByProductTypeId(ProductSearchModel searchModel, int? startIndex, short? length)
		{
			IEnumerable<Product> products = productTypeRepository.GetProductsBy(searchModel);

			short mq = (short)(searchModel.MinimumQuantity - 1);
			if (searchModel.MinimumQuantity != null)
				products = products.Where(p => p.Quantity > mq);

			if (startIndex != null && startIndex > -1)
				products = products.Skip((int)startIndex);
			if (length != null && length > -1)
				products = products.Take((int)length);

			return products.ConvertToViews();
		}

		public async Task<int> CountProductsBySellerIdAsync(ProductSearchModel searchModel)
			=> (await sellerRepository
				.GetProductsByAsync(searchModel))
				.Count();

		public int CountProductsByProductTypeId(ProductSearchModel searchModel)
			=> productTypeRepository.GetProductsBy(searchModel).Count();

		public async Task<int> CountProductsDistinctAsync(ProductSearchModel searchModel)
			=> (await productTypeRepository
				.GetProductsDistinctAsync(searchModel))
				.Count();

		public async Task<int> CountProductsAsync(ProductSearchModel searchModel)
			=> (await sellerRepository.GetAllProductsByAsync(searchModel)).Count();

		public async Task<IEnumerable<string>> GetProductImagesAsync(int sellerId, int productTypeId)
			=> (await sellerRepository
				.GetProductByAsync(sellerId, productTypeId))?
				.Images ?? null;

		public IDictionary<string, HashSet<string>> GetProductAttributes(int sellerId, int productTypeId)
			=> sellerRepository.GetProductAttributes(sellerId, productTypeId).ToFormalForm();

		public async Task<IEnumerable<IDictionary<string, string>>> GetProductAttributesStatesAsync(int sellerId, int productTypeId)
			=> (await sellerRepository
				.GetProductByAsync(sellerId, productTypeId))?
				.AttributesStates ?? null;

		public async Task<Message> UpdateProductAttributesAsync(int sellerId, int productTypeId, IDictionary<string, HashSet<string>> attributes)
		{
			Message message = new Message();

			Product product = await sellerRepository.GetProductByAsync(sellerId, productTypeId);
			if (product == null)
			{
				message.Errors.Add("Could not found product");
				return message;
			}

			if (attributes != null && attributes.Any())
			{
				var attributesToRemove = new List<string>();
				//check product attributes
				foreach (var attribute in attributes)
				{
					//check attribute name empty
					if (string.IsNullOrWhiteSpace(attribute.Key))
					{
						message.Errors.Add("Attribute name can not be empty");
						break;
					}

					//check attribute values count == 0
					if (attribute.Value == null || !attribute.Value.Any())
					{
						attributesToRemove.Add(attribute.Key);
						continue;
					}

					//check attribute value empty
					attribute.Value.RemoveWhere(v => v.IsNullOrWhiteSpace());
					//remove attribute if has no value
					if (!attribute.Value.Any())
					{
						attributesToRemove.Add(attribute.Key);
						continue;
					}
				}
				//remove attribute if has no value
				foreach (var attribute in attributesToRemove)
				{
					attributes.Remove(attribute);
				}
			}

			if (!message.Errors.Any())
			{
				product.Attributes = attributes;
				await unitOfWork.CommitAsync();
			}

			return message;
		}

		public async Task<Message> UpdateProductAttributesStatesAsync(int sellerId, int productTypeId, IEnumerable<IDictionary<string, string>> attributesStates)
		{
			Message message = new Message();

			Product product = await sellerRepository.GetProductByAsync(sellerId, productTypeId);

			if (product == null)
			{
				message.Errors.Add("Product not found");
				return message;
			}

			var attributes = product.Attributes;

			//check attributes contains attributes states or not
			var attributesStates2 = attributesStates.ToList();
			for (int i = attributesStates2.Count() - 1; i > -1; i--)
			{
				//length
				if (attributesStates2[i].Count != attributes.Count)
				{
					message.Errors.Add("Attributes states are not match with attributes");
					return message;
				}

				foreach (var attribute in attributes)
				{
					//key
					if (!attributesStates2[i].ContainsKey(attribute.Key))
					{
						message.Errors.Add("Attributes states are not match with attributes");
						return message;
					}
				}

				foreach (var state in attributesStates2[i])
				{
					//value
					if (!attributes[state.Key].Contains(state.Value))
					{
						message.Errors.Add("Attributes states are not match with attributes");
						return message;
					}
				}

				//check attributes states uniqueness
				for (int j = i + 1; j < attributesStates2.Count(); j++)
				{
					bool match = true;
					foreach (var state in attributesStates2[i])
					{
						if (state.Value != attributesStates2[j][state.Key])
						{
							match = false;
							break;
						}
					}

					if (match)
					{
						attributesStates2.Remove(attributesStates2[i]);
						break;
					}
				}
			}

			if (!message.Errors.Any())
			{
				product.AttributesStates = attributesStates2;
				await unitOfWork.CommitAsync();
			}

			return message;
		}

		public async Task<Message> AddProductAttributesStateAsync(int sellerId, int productTypeId, IDictionary<string, string> attributesState)
		{
			Message message = new Message();

			Product product = await sellerRepository.GetProductByAsync(sellerId, productTypeId);

			if (product == null)
			{
				message.Errors.Add("Product not found");
				return message;
			}

			var attributes = product.Attributes;

			//check attributes contains attributes states or not
			//length
			if (attributesState.Count != attributes.Count)
			{
				message.Errors.Add("Attributes states are not match with attributes");
				return message;
			}

			foreach (var attribute in attributes)
			{
				//key
				if (!attributesState.ContainsKey(attribute.Key))
				{
					message.Errors.Add("Attributes states are not match with attributes");
					return message;
				}
			}

			foreach (var state in attributesState)
			{
				//value
				if (!attributes[state.Key].Contains(state.Value))
				{
					message.Errors.Add("Attributes states are not match with attributes");
					return message;
				}
			}

			var attributesStates = product.AttributesStates.ToList();

			//check attributes states uniqueness
			for (short i = 0; i < attributesStates.Count; i++)
			{
				bool match = true;
				foreach (var state in attributesState)
				{
					if (state.Value != attributesStates[i][state.Key])
					{
						match = false;
						break;
					}
				}

				if (match)
				{
					message.Errors.Add("Duplicated");
					return message;
				}
			}
			attributesStates.Add(attributesState);
			product.AttributesStates = attributesStates;
			await unitOfWork.CommitAsync();

			return message;
		}

		public async Task DeleteProductAttributesStateAsync(int sellerId, int productTypeId, IDictionary<string, string> attributesState)
		{
			Product product = await sellerRepository.GetProductByAsync(sellerId, productTypeId);

			if (product == null)
			{
				return;
			}

			var attributes = product.Attributes;

			//length
			if (attributesState.Count != attributes.Count)
			{
				return;
			}

			foreach (var attribute in attributes)
			{
				//key
				if (!attributesState.ContainsKey(attribute.Key))
				{
					return;
				}
			}

			var attributesStates = product.AttributesStates.ToList();

			for (short i = 0; i < attributesStates.Count(); i++)
			{
				bool match = true;
				foreach (var state in attributesState)
				{
					if (state.Value != attributesStates[i][state.Key])
					{
						match = false;
						break;
					}
				}

				if (match)
				{
					attributesStates.RemoveAt(i);
					product.AttributesStates = attributesStates;
					await unitOfWork.CommitAsync();
					return;
				}
			}
		}

		public async Task DeleteProductAttributesStateAsync(int sellerId, int productTypeId, short index)
		{
			Product product = await sellerRepository.GetProductByAsync(sellerId, productTypeId);

			if (product == null)
			{
				return;
			}

			var attributesStates = product.AttributesStates.ToList();

			if (index > -1 && index < attributesStates.Count)
			{
				attributesStates.RemoveAt(index);
				product.AttributesStates = attributesStates;
				await unitOfWork.CommitAsync();
			}
		}

		public async Task<Message> UpdateProductAsync(int sellerId, int productTypeId, ProductUpdateModel updateModel)
		{
			Message message = new Message();

			Product product = await sellerRepository.GetProductByAsync(sellerId, productTypeId);
			if (product == null)
			{
				message.Errors.Add("Could not found product");
				return message;
			}

			//check product price
			if (updateModel.Price < 1)
				message.Errors.Add("Price can not lower than 1");

			if (!message.Errors.Any())
			{
				await sellerRepository.UpdateProductAsync(sellerId, productTypeId, updateModel.ConvertToEntity());
				await unitOfWork.CommitAsync();
			}

			return message;
		}

		public async Task<Message> UpdateProductActiveAsync(int sellerId, int productTypeId, bool active)
		{
			Message message = new Message();

			Product product = await sellerRepository.GetProductByAsync(sellerId, productTypeId);
			if (product != null)
			{
				product.Active = active;
				await unitOfWork.CommitAsync();
			}
			else message.Errors.Add("Could not found product");

			return message;
		}

		public async Task<Message> UpdateProductStatusAsync(int sellerId, int productTypeId, ProductStatus status)
		{
			Message message = new Message();

			Product product = await sellerRepository.GetProductByAsync(sellerId, productTypeId);
			if (product != null)
			{
				product.Status = status;
				await unitOfWork.CommitAsync();
			}
			else message.Errors.Add("Could not found product");

			return message;
		}

		public async Task<Message<short>> AddProductQuantityThroughAdminAsync(int sellerId, int productTypeId, short additionalNumbers)
		{
			var message = new Message<short>();

			Product product = await sellerRepository.GetProductByAsync(sellerId, productTypeId);
			if (product == null)
			{
				message.Errors.Add("Could not found product");
				return message;
			}

			OperatingModelService operatingModelService = OperatingModelServiceFactory.GetService(product.Model);

			var operatingModelMessage = await operatingModelService.CanAdminAddProductQuantityAsync(product);
			if (operatingModelMessage.Result)
			{
				product.Quantity += additionalNumbers;
				await unitOfWork.CommitAsync();
			}
			else message.Errors = operatingModelMessage.Errors;

			message.Result = product.Quantity;
			return message;
		}

		public async Task<Message<short>> ReduceProductQuantityThroughAdminAsync(int sellerId, int productTypeId, short reducingNumbers)
		{
			var message = new Message<short>();

			Product product = await sellerRepository.GetProductByAsync(sellerId, productTypeId);
			if (product == null)
			{
				message.Errors.Add("Could not found product");
				return message;
			}

			OperatingModelService operatingModelService = OperatingModelServiceFactory.GetService(product.Model);

			var operatingModelMessage = await operatingModelService.CanAdminAddProductQuantityAsync(product);
			if (operatingModelMessage.Result)
			{
				if (reducingNumbers <= product.Quantity)
				{
					product.Quantity -= reducingNumbers;
					await unitOfWork.CommitAsync();
				}
				else message.Errors.Add("Reducing numbers must greater than quantity");
			}
			else message.Errors = operatingModelMessage.Errors;

			message.Result = product.Quantity;
			return message;
		}

		public async Task<Message<short>> AddProductQuantityThroughSellerAsync(int sellerId, int productTypeId, short additionalNumbers)
		{
			var message = new Message<short>();

			Product product = await sellerRepository.GetProductByAsync(sellerId, productTypeId);

			if (product == null)
			{
				message.Errors.Add("Could not found product");
				return message;
			}

			OperatingModelService operatingModelService = OperatingModelServiceFactory.GetService(product.Model);

			var operatingModelMessage = await operatingModelService.CanSellerAddProductQuantityAsync(product);
			if (operatingModelMessage.Result)
			{
				product.Quantity += additionalNumbers;
				await unitOfWork.CommitAsync();
			}
			else message.Errors = operatingModelMessage.Errors;

			message.Result = product.Quantity;
			return message;
		}

		public async Task<Message<short>> ReduceProductQuantityThroughSellerAsync(int sellerId, int productTypeId, short reducingNumbers)
		{
			var message = new Message<short>();

			Product product = await sellerRepository.GetProductByAsync(sellerId, productTypeId);

			if (product == null)
			{
				message.Errors.Add("Could not found product");
				return message;
			}

			OperatingModelService operatingModelService = OperatingModelServiceFactory.GetService(product.Model);

			var operatingModelMessage = await operatingModelService.CanSellerReduceProductQuantityAsync(product);
			if (operatingModelMessage.Result)
			{
				if (reducingNumbers <= product.Quantity)
				{
					product.Quantity -= reducingNumbers;
					await unitOfWork.CommitAsync();
				}
				else message.Errors.Add("Reducing numbers must greater than quantity");
			}
			else message.Errors = operatingModelMessage.Errors;

			message.Result = product.Quantity;
			return message;
		}

		public async Task<Message> ChangeProductModelAsync(int sellerId, int productTypeId, OperatingModel model)
		{
			Message message = new Message();

			var productMessage = await registerProductService.ChangeOperatingModelAsync(sellerId, productTypeId, model);
			if (productMessage.Result)
				await unitOfWork.CommitAsync();
			else message.Errors = productMessage.Errors;

			return message;
		}
		#endregion

		#region Seller
		private BoolMessage ValidateSeller(Seller seller)
		{
			BoolMessage message = new BoolMessage();

			if (seller == null)
			{
				message.Errors.Add("Can not validate an empty instance");
				message.Result = false;
				return message;
			}

			if (string.IsNullOrWhiteSpace(seller.StoreName))
			{
				message.Errors.Add("Store name can not be empty");
			}
			else seller.StoreName = seller.StoreName.Trim().Capitalize().RemoveMultipleSpaces();

			if (string.IsNullOrWhiteSpace(seller.PhoneNumber))
			{
				message.Errors.Add("Phone number can not be empty");
			}
			else
			{
				seller.PhoneNumber = seller.PhoneNumber.Trim();
				if (!PhoneNumberValidationService.IsValidPhoneNumber(seller.PhoneNumber))
				{
					message.Errors.Add("Invalid phone number");
				}
			}

			if (message.Errors.Any())
				message.Result = false;
			else message.Result = true;

			return message;
		}

		public async Task<Message<SellerView>> AddSellerAsync(SellerAddModel addModel)
		{
			var message = new Message<SellerView>();
			Seller seller = addModel.ConvertToEntity();

			var validationMessage = ValidateUser(seller.User, true, true);
			if (validationMessage.Result)
			{
				validationMessage = ValidateSeller(seller);
				if (validationMessage.Result)
				{
					await sellerRepository.AddAsync(seller);
					await unitOfWork.CommitAsync();
					message.Result = seller.ConvertToView();
					return message;
				}
			}

			message.Errors = validationMessage.Errors;
			return message;
		}

		public async Task<SellerView> GetSellerByAsync(int sellerId) => (await sellerRepository.GetByAsync(sellerId))?.ConvertToView() ?? null;

		public SellerView GetSellerBy(string sellerEmail) => sellerRepository.GetBy(sellerEmail)?.ConvertToView() ?? null;

		public async Task<SellerUpdateModel> GetSellerUpdateModelByAsync(int sellerId) => (await sellerRepository.GetByAsync(sellerId))?.ConvertToUpdateModel() ?? null;

		public SellerUpdateModel GetSellerUpdateModelBy(string sellerEmail) => sellerRepository.GetBy(sellerEmail)?.ConvertToUpdateModel() ?? null;

		public IEnumerable<SellerView> GetSellersBy(SellerSearchModel searchModel, int? startIndex, short? length)
		{
			IEnumerable<Seller> sellers = sellerRepository.GetBy(searchModel);

			if (startIndex != null && startIndex > -1)
				sellers = sellers.Skip((int)startIndex);
			if (length != null && length > -1)
				sellers = sellers.Take((int)length);

			return sellers.ConvertToViews();
		}

		public int CountSellersBy(SellerSearchModel searchModel)
			=> sellerRepository
				.GetBy(searchModel)
				.Count();

		public async Task<Message> UpdateSellerAsync(int sellerId, SellerUpdateModel updateModel)
		{
			Message message = new Message();
			Seller seller = updateModel.ConvertToEntity();

			var validationMessage = ValidateUser(seller.User, false, false);
			if (validationMessage.Result)
			{
				validationMessage = ValidateSeller(seller);
				if (validationMessage.Result)
				{
					if ((await sellerRepository.GetByAsync(sellerId)) != null)
					{
						await userRepository.UpdateAsync(sellerId, seller.User);
						await sellerRepository.UpdateAsync(sellerId, seller);
						await unitOfWork.CommitAsync();
					}
					else message.Errors.Add("Could not found seller");
				}
				else message.Errors = validationMessage.Errors;
			}
			else message.Errors = validationMessage.Errors;

			return message;
		}

		public async Task<Message> UpdateSellerStatusAsync(int sellerId, SellerStatus status)
		{
			Message message = new Message();
			Seller seller = await sellerRepository.GetByAsync(sellerId);
			if (seller != null)
			{
				(await sellerRepository.GetByAsync(sellerId)).Status = status;
				await unitOfWork.CommitAsync();
			}
			else message.Errors.Add("Could not found seller");

			return message;
		}

		public async Task DeleteSellerAsync(int sellerId)
		{
			if ((await sellerRepository.GetByAsync(sellerId)) != null)
			{
				await sellerRepository.DeleteAsync(sellerId);
				await unitOfWork.CommitAsync();
			}
		}

		public async Task DeleteSellerAsync(Seller seller)
		{
			sellerRepository.Delete(seller);
			await unitOfWork.CommitAsync();
		}
		#endregion

		#region Comment
		public async Task<Message<CommentView>> SaveCommentAsync(int sellerId, int productTypeId, CommentAddModel addModel)
		{
			var message = new Message<CommentView>();

			if(addModel.Subject.IsNullOrWhiteSpace())
			{
				message.Errors.Add("Subject is required");
			}
			if (addModel.Content.IsNullOrWhiteSpace())
			{
				message.Errors.Add("Content is required");
			}
			if(message.Errors.Any())
			{
				return message;
			}

			Comment comment = addModel.ConvertToEntity();

			Product product = await sellerRepository.GetProductByAsync(sellerId, productTypeId);
			if (addModel.Images == null)
				comment.Images = product.Images;

			if (product != null)
			{
				product.SaveComment(comment);
				await unitOfWork.CommitAsync();
				comment = await sellerRepository.GetCommentByAsync(sellerId, productTypeId, addModel.CustomerId);
				message.Result = comment.ConvertToView();
				return message;
			}
			else
			{
				message.Errors.Add("Can not found product");
				return message;
			}
		}

		public async Task<CommentView> GetCommentByAsync(int sellerId, int productTypeId, int customerId)
		=> (await sellerRepository.GetCommentByAsync(sellerId, productTypeId, customerId))?.ConvertToView() ?? null;

		public IEnumerable<CommentView> GetCommentsByProductIds(CommentSearchModel searchModel, int? startIndex, short? length)
		{
			var comments = sellerRepository.GetCommentsByProductIds(searchModel);

			if (startIndex != null && startIndex > -1)
				comments = comments.Skip((int)startIndex);
			if (length != null && length > -1)
				comments = comments.Take((int)length);

			return comments.ConvertToViews();
		}

		public IEnumerable<CommentView> GetCommentsByCustomerIds(CommentSearchModel searchModel, int? startIndex, short? length)
		{
			var comments = customerRepository.GetCommentsBy(searchModel);

			if (startIndex != null && startIndex > -1)
				comments = comments.Skip((int)startIndex);
			if (length != null && length > -1)
				comments = comments.Take((int)length);

			return comments.ConvertToViews();
		}

		public IEnumerable<CommentView> GetAllComments(CommentSearchModel searchModel, int? startIndex, short? length)
		{
			var comments = sellerRepository.GetAllComments(searchModel);

			if (startIndex != null && startIndex > -1)
				comments = comments.Skip((int)startIndex);
			if (length != null && length > -1)
				comments = comments.Take((int)length);

			return comments.ConvertToViews();
		}

		public int CountCommentsByProductIds(CommentSearchModel searchModel)
			=> sellerRepository.GetCommentsByProductIds(searchModel).Count();

		public int CountCommentsByCustomerId(CommentSearchModel searchModel)
			=> customerRepository.GetCommentsBy(searchModel).Count();

		public int CountAllComments(CommentSearchModel searchModel)
			=> sellerRepository.GetAllComments(searchModel).Count();

		public float AverageNumberOfRatings(int sellerId, int productTypeId)
		{
			IEnumerable<RatingStars> ratings = sellerRepository
				.GetCommentsByProductIds(new CommentSearchModel { SellerId = sellerId, ProductTypeId = productTypeId })
				.Select(c => c.Stars);//select all rating

			if (!ratings.Any())
				return 0f;

			float sumRatings = ratings.Sum(s => (short)s);//sum all rating
			float result = sumRatings / ratings.Count();//calculates average number of ratings

			return (float)(Math.Round(result * 2, MidpointRounding.AwayFromZero) / 2);//round result to X.5 or X.0
		}

		public async Task DeleteCommentAsync(int sellerId, int productTypeId, int customerId)
		{
			Product product = await sellerRepository.GetProductByAsync(sellerId, productTypeId);

			if (product != null)
			{
				product.DeleteComment(customerId);
				await unitOfWork.CommitAsync();
			}
		}
		#endregion

		#region User
		private BoolMessage ValidateUser(User user, bool checkEmail, bool checkPassword)
		{
			BoolMessage message = new BoolMessage();

			if (user == null)
			{
				message.Errors.Add("Can not validate an empty instance");
				message.Result = false;
				return message;
			}

			if (user.Name == null)
			{
				message.Errors.Add("Name can not be empty");
			}
			else if (string.IsNullOrWhiteSpace(user.Name.FirstName) || string.IsNullOrWhiteSpace(user.Name.LastName))
			{
				message.Errors.Add("First name and last name can not be empty");
			}
			else
			{
				user.Name.FirstName = user.Name.FirstName
				.Trim().Capitalize().RemoveMultipleSpaces();
				user.Name.LastName = user.Name.LastName
					.Trim().Capitalize().RemoveMultipleSpaces();
				if (user.Name.MiddleName != null)
					user.Name.MiddleName = user.Name.MiddleName
					.Trim().Capitalize().RemoveMultipleSpaces();
			}

			if (checkEmail)
			{
				if (string.IsNullOrWhiteSpace(user.Email))
				{
					message.Errors.Add("Email can not be empty");
				}
				else if (!EmailValidationService.IsValidEmail(user.Email))
				{
					message.Errors.Add("Invalid email address");
				}
				else if (adminRepository.GetBy(user.Email) != null)
				{
					message.Errors.Add("Email already in use");
				}
			}

			if (checkPassword)
			{
				if (string.IsNullOrEmpty(user.Password))
				{
					message.Errors.Add("Password can not be empty");
				}
				else user.Password = EncryptionService.Encrypt(user.Password);
			}

			if (message.Errors.Any())
				message.Result = false;
			else message.Result = true;

			return message;
		}

		public async Task<Message<UserView>> AddUserAsync(UserAddModel addModel)
		{
			var message = new Message<UserView>();
			User user = addModel.ConvertToEntity();
			var validationMessage = ValidateUser(user, true, true);
			if (validationMessage.Result)
			{
				await userRepository.AddAsync(user);
				await unitOfWork.CommitAsync();
				message.Result = user.ConvertToView();
				return message;
			}
			message.Errors = validationMessage.Errors;
			return message;
		}

		public async Task<UserView> GetUserByAsync(int userId)
			=> (await userRepository.GetByAsync(userId))?.ConvertToView() ?? null;

		public UserView GetUserBy(string userEmail)
			=> userRepository.GetBy(userEmail)?.ConvertToView() ?? null;

		public IEnumerable<UserView> GetUsersBy(UserSearchModel searchModel, int? startIndex, short? length)
		{
			IEnumerable<User> users = userRepository.GetBy(searchModel);

			if (startIndex != null && startIndex > -1)
				users = users.Skip((int)startIndex);
			if (length != null && length > -1)
				users = users.Take((int)length);

			return users.ConvertToViews();
		}

		public int CountUsersBy(UserSearchModel searchModel)
			=> userRepository
				.GetBy(searchModel)
				.Count();

		public async Task<string> GetUserEncryptedPasswordAsync(int id)
			=> (await userRepository.GetByAsync(id))?.Password ?? null;

		public async Task<Message> UpdateUserAsync(int userId, UserUpdateModel updateModel)
		{
			Message message = new Message();
			User user = updateModel.ConvertToEntity();

			var validationMessage = ValidateUser(user, false, false);
			if (validationMessage.Result)
			{
				if ((await userRepository.GetByAsync(userId)) != null)
				{
					await userRepository.UpdateAsync(userId, user);
					await unitOfWork.CommitAsync();
				}
				else message.Errors.Add("Could not found user");
			}
			else message.Errors = validationMessage.Errors;

			return message;
		}

		public async Task<Message> UpdateUserPasswordAsync(int userId, string password)
		{
			Message message = new Message();
			if (!string.IsNullOrEmpty(password))
			{
				User user = await userRepository.GetByAsync(userId);
				if (user != null)
				{
					user.Password = EncryptionService.Encrypt(password);
					await unitOfWork.CommitAsync();
				}
				else message.Errors.Add("Could not found user");
			}
			else message.Errors.Add("Password can not be empty");

			return message;
		}

		public async Task<Message> UpdateUserEmailAsync(int userId, string email)
		{
			Message message = new Message();
			if (string.IsNullOrWhiteSpace(email))
			{
				message.Errors.Add("Email can not be empty");
			}
			else if (!EmailValidationService.IsValidEmail(email))
			{
				message.Errors.Add("Invalid email address");
			}
			else
			{
				User user = await userRepository.GetByAsync(userId);
				if (user != null)
				{
					if (userRepository.GetBy(email) == null)
					{
						user.Email = email;
						await unitOfWork.CommitAsync();
					}
					else message.Errors.Add("Email already in use");
				}
				else message.Errors.Add("Could not found user");
			}

			return message;
		}

		public async Task<Message> UpdateUserActiveAsync(int userId, bool active)
		{
			Message message = new Message();
			User user = await userRepository.GetByAsync(userId);
			if (user != null)
			{
				user.Active = active;
				await unitOfWork.CommitAsync();
			}
			else message.Errors.Add("Could not found user");

			return message;
		}

		public async Task DeleteUserAsync(int userId)
		{
			await userRepository.DeleteAsync(userId);
			await unitOfWork.CommitAsync();
		}

		public async Task DeleteUserAsync(User user)
		{
			userRepository.Delete(user);
			await unitOfWork.CommitAsync();
		}
		#endregion
	}
}
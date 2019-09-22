using ECommerce.Application.AddModels;
using ECommerce.Application.Extensions;
using ECommerce.Application.Extensions.AddModels;
using ECommerce.Application.Extensions.UpdateModels;
using ECommerce.Application.SearchModels;
using ECommerce.Application.Services;
using ECommerce.Application.UpdateModels;
using ECommerce.Application.Views;
using ECommerce.Extensions;
using ECommerce.Infrastructure.UnitOfWork;
using ECommerce.Models.Entities;
using ECommerce.Models.Entities.Admins;
using ECommerce.Models.Entities.Categories;
using ECommerce.Models.Entities.Customers;
using ECommerce.Models.Entities.ProductTypes;
using ECommerce.Models.Entities.Sellers;
using ECommerce.Models.Repositories;
using ECommerce.Models.Services;
using ECommerce.Models.Services.ServiceFactories;
using System;
using System.Collections.Generic;
using System.Linq;

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
		private bool ValidateAdmin(Admin admin, bool checkEmail, bool checkPassword, out ICollection<string> errors)
		{
			errors = new List<string>();

			if (admin == null)
			{
				errors.Add("Can not validate an empty instance");
				return false;
			}

			if (admin.Name == null)
			{
				errors.Add("Name can not be empty");
			}
			else if (string.IsNullOrWhiteSpace(admin.Name.FirstName) || string.IsNullOrWhiteSpace(admin.Name.LastName))
			{
				errors.Add("First name and last name can not be empty");
			}
			else
			{
				admin.Name.FirstName = admin.Name.FirstName
				.Trim().Capitalize().RemoveMultipleSpaces();
				admin.Name.LastName = admin.Name.LastName
					.Trim().Capitalize().RemoveMultipleSpaces();
				if (admin.Name.MiddleName != null)
					admin.Name.MiddleName = admin.Name.MiddleName
					.Trim().Capitalize().RemoveMultipleSpaces();
			}

			if (checkEmail)
			{
				if (string.IsNullOrWhiteSpace(admin.Email))
				{
					errors.Add("Email can not be empty");
				}
				else if (!EmailValidationService.IsValidEmail(admin.Email))
				{
					errors.Add("Invalid email address");
				}
				else if (adminRepository.GetBy(admin.Email) != null)
				{
					errors.Add("Email already in use");
				}
			}

			if (checkPassword)
			{
				if (string.IsNullOrEmpty(admin.Password))
				{
					errors.Add("Password can not be empty");
				}
				else admin.Password = EncryptionService.Encrypt(admin.Password);
			}

			return !errors.Any();
		}

		public void AddAdmin(AdminAddModel addModel, out ICollection<string> errors)
		{
			Admin admin = addModel.ConvertToEntity();
			if (ValidateAdmin(admin, true, true, out errors))
			{
				adminRepository.Add(admin);
				unitOfWork.Commit();
			}
		}

		public AdminView GetAdminBy(int adminId)
			=> adminRepository.GetBy(adminId)?.ConvertToView() ?? null;

		public AdminUpdateModel GetAdminUpdateModelBy(int adminId)
			=> adminRepository.GetBy(adminId)?.ConvertToUpdateModel() ?? null;

		public AdminUpdateModel GetAdminUpdateModelBy(string adminEmail)
			=> adminRepository.GetBy(adminEmail)?.ConvertToUpdateModel() ?? null;

		public AdminView GetAdminBy(string adminEmail)
			=> adminRepository.GetBy(adminEmail)?.ConvertToView() ?? null;

		public string GetAdminEncryptedPassword(int id)
			=> adminRepository.GetBy(id)?.Password ?? null;

		public IEnumerable<AdminView> GetAdminsBy(AdminSearchModel searchModel, int? startIndex, short? length)
		{
			IEnumerable<Admin> admins = adminRepository
			.GetBy(new FullName(searchModel.FirstName, searchModel.MiddleName, searchModel.LastName));

			if (startIndex != null && startIndex > -1)
				admins = admins.Skip((int)startIndex);
			if (length != null && length > -1)
				admins = admins.Take((int)length);

			return admins.ConvertToViews();
		}

		public int CountAdminsBy(AdminSearchModel searchModel)
			=> adminRepository
				.GetBy(new FullName(searchModel.FirstName, searchModel.MiddleName, searchModel.LastName))
				.Count();

		public void UpdateAdmin(int adminId, AdminUpdateModel updateModel, out ICollection<string> errors)
		{
			Admin admin = updateModel.ConvertToEntity();

			if (ValidateAdmin(admin, false, false, out errors))
			{
				if (adminRepository.GetBy(adminId) != null)
				{
					adminRepository.Update(adminId, admin);
					unitOfWork.Commit();
				}
				else errors.Add("Could not found admin");
			}
		}

		public void UpdateAdminPassword(int adminId, string password, out ICollection<string> errors)
		{
			errors = new List<string>();
			if (!string.IsNullOrEmpty(password))
			{
				Admin admin = adminRepository.GetBy(adminId);
				if (admin != null)
				{
					admin.Password = password;
					unitOfWork.Commit();
				}
				else errors.Add("Could not found admin");
			}
			else errors.Add("Password can not be empty");
		}

		public void UpdateAdminEmail(int adminId, string email, out ICollection<string> errors)
		{
			errors = new List<string>();
			if (string.IsNullOrWhiteSpace(email))
			{
				errors.Add("Email can not be empty");
			}
			else if (!EmailValidationService.IsValidEmail(email))
			{
				errors.Add("Invalid email address");
			}
			else
			{
				Admin admin = adminRepository.GetBy(adminId);
				if (admin != null)
				{
					if (adminRepository.GetBy(email) == null)
					{
						admin.Email = email;
						unitOfWork.Commit();
					}
					else errors.Add("Email already in use");
				}
				else errors.Add("Could not found admin");
			}
		}

		public void DeleteAdmin(int adminId)
		{
			adminRepository.Delete(adminId);
			unitOfWork.Commit();
		}

		public void DeleteAdmin(Admin admin)
		{
			adminRepository.Delete(admin);
			unitOfWork.Commit();
		}
		#endregion

		#region Category
		private bool ValidateCategory(Category category, out ICollection<string> errors)
		{
			errors = new List<string>();

			if (category == null)
			{
				errors.Add("Can not validate an empty instance");
				return false;
			}

			if (string.IsNullOrWhiteSpace(category.Name))
			{
				errors.Add("Name can not be empty");
			}
			else
			{
				category.Name = category.Name
					.Trim()
					.RemoveMultipleSpaces();
			}

			return !errors.Any();
		}

		public void AddCategory(CategoryAddModel addModel, int? parentId, out ICollection<string> errors)
		{
			Category category = addModel.ConvertToEntity();

			if (!ValidateCategory(category, out errors))
				return;

			if (parentId != null)
			{
				Category parentCategory = categoryRepository.GetBy((int)parentId);
				if (parentCategory == null)
				{
					errors.Add("Parent was not found");
					return;
				}

				if (parentCategory.ChildCategories.Select(c => c.Name).Contains(category.Name))
				{
					errors.Add($"Parent already has a child named \"{category.Name}\"");
					return;
				}
			}
			else if (categoryRepository.GetRoots().Select(c => c.Name).Contains(category.Name))
			{
				errors.Add("Root has already existed");
				return;
			}

			category.ParentId = parentId;

			categoryRepository.Add(category);
			unitOfWork.Commit();
		}

		public IEnumerable<CategoryView> GetAllRootCategories()
			=> categoryRepository.GetRoots().ConvertToViews();

		public CategoryView GetCategoryBy(int categoryId)
			=> categoryRepository.GetBy(categoryId)?.ConvertToView() ?? null;

		public CategoryUpdateModel GetCategoryUpdateModelBy(int categoryId)
			=> categoryRepository.GetBy(categoryId)?.ConvertToUpdateModel() ?? null;

		public CategoryView GetCategoryByProductTypeId(int productTypeId)
			=> productTypeRepository.GetBy(productTypeId)?.Category.ConvertToView() ?? null;

		public CategoryView GetParentCategory(int categoryId)
		{
			Category category = categoryRepository.GetBy(categoryId);
			return category?.ParentCategory?.ConvertToView() ?? null;
		}

		public IEnumerable<CategoryView> GetChildCategories(int categoryId)
		{
			Category category = categoryRepository.GetBy(categoryId);
			return category?.ChildCategories.ConvertToViews() ?? null;
		}

		public void UpdateCategory(int categoryId, CategoryUpdateModel updateModel, out ICollection<string> errors)
		{
			Category category = updateModel.ConvertToEntity();

			if (ValidateCategory(category, out errors))
			{
				if (categoryRepository.GetBy(categoryId) != null)
				{
					categoryRepository.Update(categoryId, category);
					unitOfWork.Commit();
				}
				else errors.Add("Could not found category");
			}
		}

		public void DeleteCategory(int categoryId)
		{
			categoryRepository.Delete(categoryId);
			unitOfWork.Commit();
		}

		public void DeleteCategory(Category category)
		{
			categoryRepository.Delete(category);
			categoryRepository.Commit();
		}
		#endregion

		#region Customer
		private bool ValidateCustomer(Customer customer, bool checkEmail, bool checkPassword, out ICollection<string> errors)
		{
			errors = new List<string>();

			if (customer == null)
			{
				errors.Add("Can not validate an empty instance");
				return false;
			}

			if (customer.Name == null)
			{
				errors.Add("Name can not be empty");
			}
			else if (string.IsNullOrWhiteSpace(customer.Name.FirstName) || string.IsNullOrWhiteSpace(customer.Name.LastName))
			{
				errors.Add("First name and last name can not be empty");
			}
			else
			{
				customer.Name.FirstName = customer.Name.FirstName
				.Trim().Capitalize().RemoveMultipleSpaces();
				customer.Name.LastName = customer.Name.LastName
					.Trim().Capitalize().RemoveMultipleSpaces();
				if (customer.Name.MiddleName != null)
					customer.Name.MiddleName = customer.Name.MiddleName
					.Trim().Capitalize().RemoveMultipleSpaces();
			}

			if (checkEmail)
			{
				if (string.IsNullOrWhiteSpace(customer.Email))
				{
					errors.Add("Email can not be empty");
				}
				else if (!EmailValidationService.IsValidEmail(customer.Email))
				{
					errors.Add("Invalid email address");
				}
				else if (adminRepository.GetBy(customer.Email) != null)
				{
					errors.Add("Email already in use");
				}
			}

			if (checkPassword)
			{
				if (string.IsNullOrEmpty(customer.Password))
				{
					errors.Add("Password can not be empty");
				}
				else customer.Password = EncryptionService.Encrypt(customer.Password);
			}

			return !errors.Any();
		}

		public void AddCustomer(CustomerAddModel addModel, out ICollection<string> errors)
		{
			Customer customer = addModel.ConvertToEntity();
			if (ValidateCustomer(customer, true, true, out errors))
			{
				customerRepository.Add(customer);
				unitOfWork.Commit();
			}
		}

		public CustomerView GetCustomerBy(int customerId)
			=> customerRepository.GetBy(customerId)?.ConvertToView() ?? null;

		public CustomerView GetCustomerBy(string customerEmail)
			=> customerRepository.GetBy(customerEmail)?.ConvertToView() ?? null;

		public CustomerUpdateModel GetCustomerCustomerUpdateModelBy(int customerId)
			=> customerRepository.GetBy(customerId)?.ConvertToUpdateModel() ?? null;

		public CustomerUpdateModel GetCustomerCustomerUpdateModelBy(string customerEmail)
			=> customerRepository.GetBy(customerEmail)?.ConvertToUpdateModel() ?? null;

		public CustomerView GetCustomerByOrderId(int orderId)
			=> customerRepository.GetOrderBy(orderId)?.Customer.ConvertToView() ?? null;

		public IEnumerable<CustomerView> GetCustomersBy(CustomerSearchModel searchModel, int? startIndex, short? length)
		{
			IEnumerable<Customer> customers = customerRepository
				.GetBy(searchModel.Email,
					new FullName(searchModel.FirstName, searchModel.MiddleName, searchModel.LastName),
					searchModel.Active);

			if (startIndex != null && startIndex > -1)
				customers = customers.Skip((int)startIndex);
			if (length != null && length > -1)
				customers = customers.Take((int)length);

			return customers.ConvertToViews();
		}

		public int CountCustomersBy(CustomerSearchModel searchModel)
			=> customerRepository
				.GetBy(searchModel.Email,
					new FullName(searchModel.FirstName, searchModel.MiddleName, searchModel.LastName),
					searchModel.Active)
				.Count();

		public string GetCustomerEncryptedPassword(int id)
			=> customerRepository.GetBy(id)?.Password ?? null;

		public void UpdateCustomer(int customerId, CustomerUpdateModel updateModel, out ICollection<string> errors)
		{
			Customer customer = updateModel.ConvertToEntity();

			if (ValidateCustomer(customer, false, false, out errors))
			{
				if (customerRepository.GetBy(customerId) != null)
				{
					customerRepository.Update(customerId, customer);
					unitOfWork.Commit();
				}
				else errors.Add("Could not found customer");
			}
		}

		public void UpdateCustomerPassword(int customerId, string password, out ICollection<string> errors)
		{
			errors = new List<string>();
			if (!string.IsNullOrEmpty(password))
			{
				Customer customer = customerRepository.GetBy(customerId);
				if (customer != null)
				{
					customer.Password = password;
					unitOfWork.Commit();
				}
				else errors.Add("Could not found customer");
			}
			else errors.Add("Password can not be empty");
		}

		public void UpdateCustomerActive(int customerId, bool active, out ICollection<string> errors)
		{
			errors = new List<string>();
			Customer customer = customerRepository.GetBy(customerId);
			if (customer != null)
			{
				customer.Active = active;
				unitOfWork.Commit();
			}
			else errors.Add("Could not found customer");
		}

		public void UpdateCustomerEmail(int customerId, string email, out ICollection<string> errors)
		{
			errors = new List<string>();
			if (string.IsNullOrWhiteSpace(email))
			{
				errors.Add("Email can not be empty");
			}
			else if (!EmailValidationService.IsValidEmail(email))
			{
				errors.Add("Invalid email address");
			}
			else
			{
				Customer customer = customerRepository.GetBy(customerId);
				if (customer != null)
				{
					if (customerRepository.GetBy(email) == null)
					{
						customer.Email = email;
						unitOfWork.Commit();
					}
					else errors.Add("Email already in use");
				}
				else errors.Add("Could not found customer");
			}
		}

		public void DeleteCustomer(int customerId)
		{
			customerRepository.Delete(customerId);
			unitOfWork.Commit();
		}

		public void DeleteCustomer(Customer customer)
		{
			customerRepository.Delete(customer);
			unitOfWork.Commit();
		}
		#endregion

		#region Order
		public void AddOrder(int customerId, OrderAddModel addModel, out ICollection<string> errors)
		{
			Order order = addModel.ConvertToEntity();
			if (OrderService.TryOrder(customerId, order, out errors))
				unitOfWork.Commit();
		}

		public void CancelOrder(Order order, out ICollection<string> errors)
		{
			if (OrderService.TryCancelOrder(order, out errors))
				unitOfWork.Commit();
		}

		public void ConfirmOrderThroughAdmin(Order order, out ICollection<string> errors)
		{
			if (OrderService.TryConfirmOrderByAdmin(order, out errors))
				unitOfWork.Commit();
		}

		public void ConfirmOrderThroughSeller(Order order, out ICollection<string> errors)
		{
			if (OrderService.TryConfirmOrderBySeller(order, out errors))
				unitOfWork.Commit();
		}

		public void RejectOrderThroughAdmin(Order order, out ICollection<string> errors)
		{
			if (OrderService.TryRejectOrderByAdmin(order, out errors))
				unitOfWork.Commit();
		}

		public void RejectOrderThroughSeller(Order order, out ICollection<string> errors)
		{
			if (OrderService.TryRejectOrderBySeller(order, out errors))
				unitOfWork.Commit();
		}

		public void ChangeOrderStatusThroughAdmin(Order order, OrderStatus status, out ICollection<string> errors)
		{
			if (OrderService.TryChangeOrderStatusByAdmin(order, status, out errors))
				unitOfWork.Commit();
		}

		public void ChangeOrderStatusThroughSeller(Order order, OrderStatus status, out ICollection<string> errors)
		{
			if (OrderService.TryChangeOrderStatusBySeller(order, status, out errors))
				unitOfWork.Commit();
		}

		public OrderView GetOrderBy(int orderId)
			=> customerRepository.GetOrderBy(orderId)?.ConvertToView() ?? null;

		public IEnumerable<OrderView> GetOrdersByCustomerId(OrderSearchModel searchModel, int? startIndex, short? length)
		{
			IEnumerable<Order> orders = customerRepository
				.GetOrdersBy((int)searchModel.CustomerId, searchModel.Quantity, searchModel.TotalValue, searchModel.TotalValueIndication);

			if (startIndex != null && startIndex > -1)
				orders = orders.Skip((int)startIndex);
			if (length != null && length > -1)
				orders = orders.Take((int)length);

			return orders.ConvertToViews();
		}

		public IEnumerable<OrderView> GetOrdersBySellerId(OrderSearchModel searchModel, int? startIndex, short? length)
		{
			IEnumerable<Order> orders = sellerRepository
				.GetOrdersBy((int)searchModel.SellerId, searchModel.Quantity, searchModel.TotalValue, searchModel.TotalValueIndication);

			if (startIndex != null && startIndex > -1)
				orders = orders.Skip((int)startIndex);
			if (length != null && length > -1)
				orders = orders.Take((int)length);

			return orders.ConvertToViews();
		}

		public IEnumerable<OrderView> GetOrdersByProductTypeId(OrderSearchModel searchModel, int? startIndex, short? length)
		{
			IEnumerable<Order> orders = productTypeRepository
				.GetOrdersBy((int)searchModel.ProductTypeId, searchModel.Quantity, searchModel.TotalValue, searchModel.TotalValueIndication);

			if (startIndex != null && startIndex > -1)
				orders = orders.Skip((int)startIndex);
			if (length != null && length > -1)
				orders = orders.Take((int)length);

			return orders.ConvertToViews();
		}

		public int CountOrdersByCustomerId(OrderSearchModel searchModel)
			=> customerRepository
				.GetOrdersBy((int)searchModel.CustomerId, searchModel.Quantity, searchModel.TotalValue, searchModel.TotalValueIndication)
				.Count();

		public int CountOrdersBySellerId(OrderSearchModel searchModel)
			=> sellerRepository
				.GetOrdersBy((int)searchModel.SellerId, searchModel.Quantity, searchModel.TotalValue, searchModel.TotalValueIndication)
				.Count();

		public int CountOrdersByProductTypeId(OrderSearchModel searchModel)
			=> productTypeRepository
				.GetOrdersBy((int)searchModel.ProductTypeId, searchModel.Quantity, searchModel.TotalValue, searchModel.TotalValueIndication)
				.Count();
		#endregion

		#region Product Type
		private bool ValidateProductType(ProductType productType, out ICollection<string> errors)
		{
			errors = new List<string>();

			if (productType == null)
			{
				errors.Add("Can not validate an empty instance");
				return false;
			}

			if (string.IsNullOrWhiteSpace(productType.Name))
			{
				errors.Add("Name can not be empty");
			}
			else productType.Name = productType.Name.Trim().RemoveMultipleSpaces();

			Category category = categoryRepository.GetBy(productType.CategoryId);
			if (category == null)
			{
				errors.Add("Category can not be empty");
			}
			else if (category.ChildCategories.Any())
			{
				errors.Add("Category must has no child");
			}

			return !errors.Any();
		}

		public void AddProductType(ProductTypeAddModel addModel, out ICollection<string> errors)
		{
			ProductType productType = addModel.ConvertToEntity();
			if (ValidateProductType(productType, out errors))
			{
				productTypeRepository.Add(productType);
				unitOfWork.Commit();
			}
		}

		public ProductTypeView GetProductTypeBy(int productTypeId)
			=> productTypeRepository.GetBy(productTypeId)?.ConvertToView() ?? null;

		public ProductTypeUpdateModel GetProductTypeUpdateModelBy(int productTypeId)
			=> productTypeRepository.GetBy(productTypeId)?.ConvertToUpdateModel() ?? null;

		public IEnumerable<ProductTypeView> GetProductTypesBy(ProductTypeSearchModel searchModel, int? startIndex, short? length)
		{
			IEnumerable<ProductType> productTypes = productTypeRepository.GetBy(searchModel.SearchString, searchModel.DateTimeModified, searchModel.CategoryId, searchModel.Status);

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

			if (startIndex != null && startIndex > -1)
				productTypes = productTypes.Skip((int)startIndex);
			if (length != null && length > -1)
				productTypes = productTypes.Take((int)length);

			return productTypes.ConvertToViews();
		}

		public int CountProductTypesBy(ProductTypeSearchModel searchModel)
		{
			IEnumerable<ProductType> productTypes = productTypeRepository.GetBy(searchModel.SearchString, searchModel.DateTimeModified, searchModel.CategoryId, searchModel.Status);

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

		public void UpdateProductType(int productTypeId, ProductTypeUpdateModel updateModel, out ICollection<string> errors)
		{
			ProductType productType = updateModel.ConvertToEntity();

			if (ValidateProductType(productType, out errors))
			{
				if (productTypeRepository.GetBy(productTypeId) != null)
				{
					productTypeRepository.Update(productTypeId, productType);
					unitOfWork.Commit();
				}
				else errors.Add("Could not found product type");
			}
		}

		public void UpdateProductTypeStatus(int productTypeId, ProductTypeStatus status, out ICollection<string> errors)
		{
			errors = new List<string>();
			ProductType seller = productTypeRepository.GetBy(productTypeId);
			if (seller != null)
			{
				productTypeRepository.GetBy(productTypeId).Status = status;
				unitOfWork.Commit();
			}
			else errors.Add("Could not found product type");
		}

		public void DeleteProductType(int productTypeId, out ICollection<string> errors)
		{
			errors = new List<string>();

			ProductType productType = productTypeRepository.GetBy(productTypeId);
			if (productType != null)
			{
				errors.Add("Could not found product type");
			}
			else
			{
				if (productType.Products.Any())
					errors.Add("Can not delete a product type that has been registered");

				if (productTypeRepository.GetOrdersBy(productTypeId, null, null, null).Any())
					errors.Add("Can not delete a product type that has been ordered");

				if (!errors.Any())
				{
					productTypeRepository.Delete(productType);
					unitOfWork.Commit();
				}
			}
		}

		public void DeleteProductType(ProductType productType, out ICollection<string> errors)
		{
			errors = new List<string>();

			if (productType != null)
			{
				errors.Add("Could not found product type");
			}
			else
			{
				if (productType.Products.Any())
					errors.Add("Can not delete a product type that has been registered");

				if (productType.Orders.Any())
					errors.Add("Can not delete a product type that has been ordered");

				if (!errors.Any())
				{
					productTypeRepository.Delete(productType);
					unitOfWork.Commit();
				}
			}
		}
		#endregion

		#region Product Type Update Request
		public IEnumerable<ProductTypeUpdateRequestView> GetProductTypeUpdateRequests(int? startIndex, short? length)
		{
			IEnumerable<ProductTypeUpdateRequest> updateRequests = productTypeRepository.GetUpdateRequests();

			if (startIndex != null && startIndex > -1)
				updateRequests = updateRequests.Skip((int)startIndex);
			if (length != null && length > -1)
				updateRequests = updateRequests.Take((int)length);

			return updateRequests.ConvertToViews();
		}

		public int CountProductTypeUpdateRequests() => productTypeRepository.GetUpdateRequests().Count();

		public IEnumerable<ProductTypeUpdateRequestView> GetProductTypeUpdateRequestsByProductTypeId(int productTypeId, int? startIndex, short? length)
		{
			IEnumerable<ProductTypeUpdateRequest> updateRequests = productTypeRepository.GetUpdateRequests(productTypeId);

			if (startIndex != null && startIndex > -1)
				updateRequests = updateRequests.Skip((int)startIndex);
			if (length != null && length > -1)
				updateRequests = updateRequests.Take((int)length);

			return updateRequests.ConvertToViews();
		}

		public int CountProductTypeUpdateRequestsByProductTypeId(int productTypeId) => productTypeRepository.GetUpdateRequests(productTypeId).Count();

		public IEnumerable<ProductTypeUpdateRequestView> GetProductTypeUpdateRequestsBySellerId(int sellerId, int? startIndex, short? length)
		{
			IEnumerable<ProductTypeUpdateRequest> updateRequests = sellerRepository.GetProductTypeUpdateRequests(sellerId);

			if (startIndex != null && startIndex > -1)
				updateRequests = updateRequests.Skip((int)startIndex);
			if (length != null && length > -1)
				updateRequests = updateRequests.Take((int)length);

			return updateRequests.ConvertToViews();
		}

		public int CountProductTypeUpdateRequestsBySellerId(int sellerId) => sellerRepository.GetProductTypeUpdateRequests(sellerId).Count();

		public ProductTypeUpdateRequestView GetProductTypeUpdateRequestBy(int sellerId, int productTypeId)
		{
			return productTypeRepository.GetUpdateRequest(sellerId, productTypeId)?.ConvertToView();
		}

		public void RequestAnUpdateForProductType(int sellerId, ProductTypeUpdateRequestAddModel addModel, out ICollection<string> errors)
		{
			if (UpdateProductTypeService.TryRequestAnUpdate(sellerId, addModel.ConvertToEntity(), out errors))
				unitOfWork.Commit();
		}

		public void DeclineAnUpdateForProductType(int sellerId, int productTypeId, out ICollection<string> errors)
		{
			if (UpdateProductTypeService.TryDeclineAnUpdate(sellerId, productTypeId, out errors))
				unitOfWork.Commit();
		}

		public void ApplyAnUpdateForProductType(int sellerId, int productTypeId, out ICollection<string> errors)
		{
			if (UpdateProductTypeService.TryApplyAnUpdate(sellerId, productTypeId, out errors))
				unitOfWork.Commit();
		}
		#endregion

		#region Product
		public void RegisterProduct(int sellerId, ProductAddModel addModel, out ICollection<string> errors)
		{
			errors = new List<string>();

			if (addModel.RepresentativeImage != null)
			{
				if (!ImageValidationService.IsValid(addModel.RepresentativeImage.Data))
				{
					errors.Add("Representative image is invalid");
				}
				if (!ImageValidationService.IsSizeValid(addModel.RepresentativeImage.Data))
				{
					errors.Add($"Representative image size can not larger than {ImageValidationService.AllowedSize} bytes {addModel.RepresentativeImage.Data.Length}");
				}
			}
			else errors.Add("Representative image is required");

			if (addModel.Images != null)
			{
				short count = 1;
				foreach (FileContent image in addModel.Images)
				{
					if (!ImageValidationService.IsValid(image.Data))
					{
						errors.Add("Image is invalid");
						break;
					}
					if (!ImageValidationService.IsSizeValid(image.Data))
					{
						errors.Add($"Size can not larger than {ImageValidationService.AllowedSize} bytes {image.Data.Length}");
						break;
					}
					count++;
				}
			}

			if (errors.Any())
				return;

			Product product = addModel.ConvertToEntity();
			if (RegisterProductService.TryRegister(sellerId, product, out errors))
				unitOfWork.Commit();
		}

		public void UnregisterProduct(int sellerId, int productTypeId, out ICollection<string> errors)
		{
			if (RegisterProductService.TryUnregister(sellerId, productTypeId, out errors))
				unitOfWork.Commit();
		}

		public void ChangeProductOperatingModel(int sellerId, int productTypeId, OperatingModel model, out ICollection<string> errors)
		{
			if (RegisterProductService.TryChangeOperatingModel(sellerId, productTypeId, model, out errors))
				unitOfWork.Commit();
		}

		public ProductView GetProductBy(int sellerId, int productTypeId)
			=> sellerRepository.GetProductBy(sellerId, productTypeId)?.ConvertToView() ?? null;

		public ProductUpdateModel GetProductUpdateModelBy(int sellerId, int productTypeId)
			=> sellerRepository.GetProductBy(sellerId, productTypeId)?.ConvertToUpdateModel() ?? null;

		public ProductView GetProductByOrderId(int orderId)
		{
			Order order = customerRepository.GetOrderBy(orderId);
			return order != null ?
				sellerRepository.GetProductBy(order.SellerId, order.ProductTypeId)?
					.ConvertToView() ?? 
					null :
				null;
		}

		public ProductView GetRepresentativeProduct(int productTypeId)
		{
			var products=productTypeRepository.GetProductsBy(productTypeId, null, null, null, null);
			return products.LastOrDefault(p => p.Active && p.Status == ProductStatus.Active)?
				.ConvertToView() ??
				products.LastOrDefault()?
					.ConvertToView() ??
					null;
		}

		public IEnumerable<ProductView> GetProductsBySellerId(ProductSearchModel searchModel, int? startIndex, short? length)
		{
			IEnumerable<Product> products = sellerRepository
				.GetProductsBy((int)searchModel.SellerId, searchModel.SearchString, searchModel.CategoryId,
					searchModel.Price, searchModel.PriceIndication, searchModel.Status, searchModel.Active,
					searchModel.ProductTypeStatus);

			if (startIndex != null && startIndex > -1)
				products = products.Skip((int)startIndex);
			if (length != null && length > -1)
				products = products.Take((int)length);

			return products.ConvertToViews();
		}

		public IEnumerable<ProductView> GetProductsByProductTypeId(ProductSearchModel searchModel, int? startIndex, short? length)
		{
			IEnumerable<Product> products = productTypeRepository
				.GetProductsBy((int)searchModel.ProductTypeId, searchModel.Price, searchModel.PriceIndication,
					searchModel.Status, searchModel.Active);

			if (startIndex != null && startIndex > -1)
				products = products.Skip((int)startIndex);
			if (length != null && length > -1)
				products = products.Take((int)length);

			return products.ConvertToViews();
		}

		public int CountProductsBySellerId(ProductSearchModel searchModel)
			=> sellerRepository
				.GetProductsBy((int)searchModel.SellerId, searchModel.SearchString, searchModel.CategoryId,
					searchModel.Price, searchModel.PriceIndication, searchModel.Status, searchModel.Active,
					searchModel.ProductTypeStatus)
				.Count();

		public int CountProductsByProductTypeId(ProductSearchModel searchModel)
			=> productTypeRepository
				.GetProductsBy((int)searchModel.ProductTypeId, searchModel.Price, searchModel.PriceIndication,
					searchModel.Status, searchModel.Active)
				.Count();

		public IEnumerable<FileContent> GetProductImages(int sellerId, int productTypeId)
			=> sellerRepository
				.GetProductBy(sellerId, productTypeId)
				.Images
				.Select(i => new FileContent(i.Content.Data, i.Content.MimeType));

		public void UpdateProduct(int sellerId, int productTypeId, ProductUpdateModel updateModel, out ICollection<string> errors)
		{
			errors = new List<string>();

			if (updateModel.RepresentativeImage != null)
			{
				if (!ImageValidationService.IsValid(updateModel.RepresentativeImage.Data))
				{
					errors.Add("Representative image is invalid");
				}
				if (!ImageValidationService.IsSizeValid(updateModel.RepresentativeImage.Data))
				{
					errors.Add($"Representative image size can not larger than {ImageValidationService.AllowedSize} bytes {updateModel.RepresentativeImage.Data.Length}");
				}
			}
			else errors.Add("Representative image is required");

			if (updateModel.Images != null)
			{
				foreach (FileContent image in updateModel.Images)
				{
					if (!ImageValidationService.IsValid(image.Data))
					{
						errors.Add("Image is invalid");
						break;
					}
					if (!ImageValidationService.IsSizeValid(image.Data))
					{
						errors.Add($"Size can not larger than {ImageValidationService.AllowedSize} bytes");
						break;
					}
				}
			}

			if (sellerRepository.GetProductBy(sellerId, productTypeId) == null)
			{
				errors.Add("Could not found product");
			}

			if (updateModel.Attributes != null && updateModel.Attributes.Any())
			{
				//check product attributes
				foreach(var attribute in updateModel.Attributes)
				{
					//check attribute name empty
					if (string.IsNullOrWhiteSpace(attribute.Key))
					{
						errors.Add("Attribute name can not be empty");
						break;
					}

					//check attribute values count == 0
					if (attribute.Value == null || !attribute.Value.Any())
						break;

					//check attribute value empty
					foreach (string value in attribute.Value)
					{
						if (string.IsNullOrWhiteSpace(value))
						{
							errors.Add("Attribute value can not be empty");
							goto flag;
						}
					}
				}
			flag:;
			}

			//check product price
			if (updateModel.Price < 1)
				errors.Add("Price can not lower than 1");

			if (!errors.Any())
			{
				sellerRepository.UpdateProduct(sellerId, productTypeId, updateModel.ConvertToEntity());
				unitOfWork.Commit();
			}
		}

		public void UpdateProductActive(int sellerId, int productTypeId, bool active, out ICollection<string> errors)
		{
			errors = new List<string>();

			Product product = sellerRepository.GetProductBy(sellerId, productTypeId);
			if (product != null)
			{
				product.Active = active;
				unitOfWork.Commit();
			}
			else errors.Add("Could not found product");
		}

		public void UpdateProductStatus(int sellerId, int productTypeId, ProductStatus status, out ICollection<string> errors)
		{
			errors = new List<string>();

			Product product = sellerRepository.GetProductBy(sellerId, productTypeId);
			if (product != null)
			{
				product.Status = status;
				unitOfWork.Commit();
			}
			else errors.Add("Could not found product");
		}

		public void AddProductQuantityThroughAdmin(int sellerId, int productTypeId, short additionalNumbers, out ICollection<string> errors)
		{
			errors = new List<string>();

			Product product = sellerRepository.GetProductBy(sellerId, productTypeId);

			if (product == null)
			{
				errors.Add("Could not found product");
				return;
			}

			OperatingModelService operatingModelService = OperatingModelServiceFactory.GetService(product.Model);

			if (operatingModelService.CanAdminAddProductQuantity(product, out errors))
				unitOfWork.Commit();
		}

		public void ReduceProductQuantityThroughAdmin(int sellerId, int productTypeId, short reducingNumbers, out ICollection<string> errors)
		{
			errors = new List<string>();

			Product product = sellerRepository.GetProductBy(sellerId, productTypeId);

			if (product == null)
			{
				errors.Add("Could not found product");
				return;
			}

			OperatingModelService operatingModelService = OperatingModelServiceFactory.GetService(product.Model);

			if (operatingModelService.CanAdminReduceProductQuantity(product, out errors))
				unitOfWork.Commit();
		}

		public void AddProductQuantityThroughSeller(int sellerId, int productTypeId, short additionalNumbers, out ICollection<string> errors)
		{
			errors = new List<string>();

			Product product = sellerRepository.GetProductBy(sellerId, productTypeId);

			if (product == null)
			{
				errors.Add("Could not found product");
				return;
			}

			OperatingModelService operatingModelService = OperatingModelServiceFactory.GetService(product.Model);

			if (operatingModelService.CanSellerAddProductQuantity(product, out errors))
				unitOfWork.Commit();
		}

		public void ReduceProductQuantityThroughSeller(int sellerId, int productTypeId, short reducingNumbers, out ICollection<string> errors)
		{
			errors = new List<string>();

			Product product = sellerRepository.GetProductBy(sellerId, productTypeId);

			if (product == null)
			{
				errors.Add("Could not found product");
				return;
			}

			OperatingModelService operatingModelService = OperatingModelServiceFactory.GetService(product.Model);

			if (operatingModelService.CanSellerReduceProductQuantity(product, out errors))
				unitOfWork.Commit();
		}

		public void ChangeProductModel(int sellerId, int productTypeId, OperatingModel model, out ICollection<string> errors)
		{
			if (registerProductService.TryChangeOperatingModel(sellerId, productTypeId, model, out errors))
				unitOfWork.Commit();
		}
		#endregion

		#region Seller
		private bool ValidateSeller(Seller seller, bool checkEmail, bool checkPassword, out ICollection<string> errors)
		{
			errors = new List<string>();

			if (seller == null)
			{
				errors.Add("Can not validate an empty instance");
				return false;
			}

			if (string.IsNullOrWhiteSpace(seller.Name))
			{
				errors.Add("Name can not be empty");
			}
			else seller.Name = seller.Name.Trim().Capitalize().RemoveMultipleSpaces();

			if (string.IsNullOrWhiteSpace(seller.PhoneNumber))
			{
				errors.Add("Phone number can not be empty");
			}
			else
			{
				seller.PhoneNumber = seller.PhoneNumber.Trim();
				if (!PhoneNumberValidationService.IsValidPhoneNumber(seller.PhoneNumber))
				{
					errors.Add("Invalid phone number");
				}
			}

			if (checkEmail)
			{
				if (string.IsNullOrWhiteSpace(seller.Email))
				{
					errors.Add("Email can not be empty");
				}
				else if (!EmailValidationService.IsValidEmail(seller.Email))
				{
					errors.Add("Invalid email address");
				}
				else if (adminRepository.GetBy(seller.Email) != null)
				{
					errors.Add("Email already in use");
				}
			}

			if (checkPassword)
			{
				if (string.IsNullOrEmpty(seller.Password))
				{
					errors.Add("Password can not be empty");
				}
				else seller.Password = EncryptionService.Encrypt(seller.Password);
			}

			return !errors.Any();
		}

		public void AddSeller(SellerAddModel addModel, out ICollection<string> errors)
		{
			Seller seller = addModel.ConvertToEntity();
			if (ValidateSeller(seller, true, true, out errors))
			{
				sellerRepository.Add(seller);
				unitOfWork.Commit();
			}
		}

		public SellerView GetSellerBy(int sellerId) => sellerRepository.GetBy(sellerId)?.ConvertToView() ?? null;

		public SellerView GetSellerBy(string sellerEmail) => sellerRepository.GetBy(sellerEmail)?.ConvertToView() ?? null;

		public SellerUpdateModel GetSellerUpdateModelBy(int sellerId) => sellerRepository.GetBy(sellerId)?.ConvertToUpdateModel() ?? null;

		public SellerUpdateModel GetSellerUpdateModelBy(string sellerEmail) => sellerRepository.GetBy(sellerEmail)?.ConvertToUpdateModel() ?? null;

		public IEnumerable<SellerView> GetSellersBy(SellerSearchModel searchModel, int? startIndex, short? length)
		{
			IEnumerable<Seller> sellers = sellerRepository.GetBy(searchModel.Email, searchModel.Name, searchModel.PhoneNumber, searchModel.Status);

			if (startIndex != null && startIndex > -1)
				sellers = sellers.Skip((int)startIndex);
			if (length != null && length > -1)
				sellers = sellers.Take((int)length);

			return sellers.ConvertToViews();
		}

		public int CountSellersBy(SellerSearchModel searchModel)
			=> sellerRepository
				.GetBy(searchModel.Email, searchModel.Name, searchModel.PhoneNumber, searchModel.Status)
				.Count();

		public void UpdateSeller(int sellerId, SellerUpdateModel updateModel, out ICollection<string> errors)
		{
			Seller seller = updateModel.ConvertToEntity();

			if (ValidateSeller(seller, false, false, out errors))
			{
				if (sellerRepository.GetBy(sellerId) != null)
				{
					sellerRepository.Update(sellerId, seller);
					unitOfWork.Commit();
				}
				else errors.Add("Could not found seller");
			}
		}

		public void UpdateSellerPassword(int sellerId, string password, out ICollection<string> errors)
		{
			errors = new List<string>();
			if (!string.IsNullOrEmpty(password))
			{
				Seller seller = sellerRepository.GetBy(sellerId);
				if (seller != null)
				{
					seller.Password = password;
					unitOfWork.Commit();
				}
				else errors.Add("Could not found seller");
			}
			else errors.Add("Password can not be empty");
		}

		public void UpdateSellerStatus(int sellerId, SellerStatus status, out ICollection<string> errors)
		{
			errors = new List<string>();
			Seller seller = sellerRepository.GetBy(sellerId);
			if (seller != null)
			{
				sellerRepository.GetBy(sellerId).Status = status;
				unitOfWork.Commit();
			}
			else errors.Add("Could not found seller");
		}

		public void UpdateSellerEmail(int sellerId, string email, out ICollection<string> errors)
		{
			errors = new List<string>();
			if (string.IsNullOrWhiteSpace(email))
			{
				errors.Add("Email can not be empty");
			}
			else if (!EmailValidationService.IsValidEmail(email))
			{
				errors.Add("Invalid email address");
			}
			else
			{
				Seller seller = sellerRepository.GetBy(sellerId);
				if (seller != null)
				{
					if (sellerRepository.GetBy(email) == null)
					{
						seller.Email = email;
						unitOfWork.Commit();
					}
					else errors.Add("Email already in use");
				}
				else errors.Add("Could not found seller");
			}
		}

		public void DeleteSeller(int sellerId)
		{
			if (sellerRepository.GetBy(sellerId) != null)
			{
				sellerRepository.Delete(sellerId);
				unitOfWork.Commit();
			}
		}

		public void DeleteSeller(Seller seller)
		{
			sellerRepository.Delete(seller);
			unitOfWork.Commit();
		}

		public string GetSellerEncryptedPassword(int id)
			=> sellerRepository.GetBy(id)?.Password ?? null;
		#endregion
	}
}
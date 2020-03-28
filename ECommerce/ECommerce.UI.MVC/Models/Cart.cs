using ECommerce.Application;
using ECommerce.Application.WorkingModels.AddModels;
using ECommerce.Application.WorkingModels.Views;
using ECommerce.Infrastructure.UnitOfWork;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.UI.MVC.Models
{
	public class Cart
	{
		[JsonIgnore]
		private ECommerceService eCommerce;
		[JsonIgnore]
		public ECommerceService ECommerce {
			get
			{
				return eCommerce;
			}
			set
			{
				eCommerce = value;
				foreach (CartLine line in Lines)
				{
					line.ECommerce = eCommerce;
				}
			}
		}

		public List<CartLine> Lines = new List<CartLine>();

		public Cart(IUnitOfWork unitOfWork)
		{
			ECommerce = new ECommerceService(unitOfWork);
		}

		public virtual async Task AddItemAsync(int sellerId, int productTypeId, short quantity, IDictionary<string, string> attributes)
		{
			CartLine newLine = new CartLine
			{
				SellerId = sellerId,
				ProductTypeId = productTypeId,
				Quantity = quantity,
				Attributes = attributes,
				ECommerce = ECommerce
			};

			CartLine line = Lines
				.FirstOrDefault(l => l.IsProductEquals(newLine));

			if (line == null)
			{
				newLine.Product = await ECommerce.GetProductByAsync(newLine.SellerId, newLine.ProductTypeId);
				Lines.Add(newLine);
			}
			else line.Quantity += quantity;
		}

		public virtual void RemoveItem(int index, short quantity)
		{
			if (-1 < index && index < Lines.Count())
			{
				CartLine line = Lines[index];
				line.Quantity -= quantity;
				if (line.Quantity < 1)
					Lines.Remove(line);
			}
		}

		public virtual void ChangeItemQuantity(int index, short quantity)
		{
			if (-1 < index && index < Lines.Count())
			{
				CartLine line = Lines[index];
				if (quantity < 1)
					Lines.Remove(line);
				else line.Quantity = quantity;
			}
		}

		public virtual void RemoveLine(int index)
		{
			if (-1 < index && index < Lines.Count())
			{
				Lines.RemoveAt(index);
			}
		}

		public virtual void RemoveLine(CartLine cartLine)
		{
			Lines.Remove(cartLine);
		}

		public virtual void Clear() => Lines.Clear();

		public decimal ComputeTotalValue()
			=> Lines.Sum(l => l.Product.Price * l.Quantity);

		public int TotalQuantity => Lines.Sum(l => l.Quantity);
	}

	public class CartLine
	{
		public int SellerId { get; set; }
		public int ProductTypeId { get; set; }
		public short Quantity { get; set; }
		public IDictionary<string, string> Attributes { get; set; } = new Dictionary<string, string>();

		[JsonIgnore]
		public ECommerceService ECommerce { get; set; }

		[JsonIgnore]
		private ProductView product;
		[JsonIgnore]
		public virtual ProductView Product
		{
			get
			{
				if (product == null)
				{
					if (ECommerce != null)
					{
						product = ECommerce.GetProductBy(SellerId, ProductTypeId);
					}
					else return default;
				}
				return product;
			}
			set
			{
				product = value;
			}
		}

		public bool IsProductEquals(CartLine line)
		{
			if (SellerId != line.SellerId)
				return false;

			if (ProductTypeId != line.ProductTypeId)
				return false;

			//check Attributes length
			if ((Attributes?.Count() ?? 0) != (Attributes?.Count() ?? 0))
				return false;

			//check Attribute key and value
			if (Attributes != null || Attributes.Count != 0)
			{
				foreach (var attribute in Attributes)
				{
					if (line.Attributes.TryGetValue(attribute.Key, out string value))
					{
						if (attribute.Value != value)
							return false;
					}
					else return false;
				}
			}

			return true;
		}
	}

	public static class CartLineExtensions
	{
		public static OrderAddModel ConvertToOrderAddModel(this CartLine cartLine)
		{
			return new OrderAddModel
			{
				Attributes = cartLine.Attributes,
				CurrentPrice = cartLine.Product.Price,
				Quantity = cartLine.Quantity,
				SellerId = cartLine.SellerId,
				ProductTypeId = cartLine.ProductTypeId
			};
		}
	}
}
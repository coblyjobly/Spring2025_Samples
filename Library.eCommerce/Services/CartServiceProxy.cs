
using Spring2025_Samples.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.eCommerce.Services
{
	public class CartServiceProxy
	{
		private CartServiceProxy()
		{
			CartItems = new List<CartItem?>();
		}
		private int LastKey
		{
			get
			{
				if (!CartItems.Any())
				{
					return 0;
				}
				return CartItems.Select(p => p?.Id ?? 0).Max();
			}
		}
		private static CartServiceProxy? instance;
		private static object instanceLock = new object();
		public static CartServiceProxy Current
		{
			get
			{
				lock (instanceLock)
				{
					if (instance == null)
					{
						instance = new CartServiceProxy();
					}
				}
				return instance;
			}
		}

		public CartItem? AddOrUpdate(CartItem cartItem)
		{
			var product = ProductServiceProxy.Current.Products.FirstOrDefault(p => p.Id == cartItem.ProductId);
			if (product == null)
			{
				Console.WriteLine("Product not found.");
				return null;
			}

			var existingCartItem = CartItems.FirstOrDefault(ci => ci.Id == cartItem.Id);
			if (existingCartItem != null)
			{
				product.Quantity += existingCartItem.Quantity;
			}

			if (cartItem.Quantity > product.Quantity)
			{
				Console.WriteLine($"Insufficient stock. Only {product.Quantity} available.");
				return null;
			}

			product.Quantity -= cartItem.Quantity;

			if (cartItem.Id == 0)
			{
				cartItem.Id = LastKey + 1;
				CartItems.Add(cartItem);
			}
			else
			{
				var existing = CartItems.FirstOrDefault(ci => ci.Id == cartItem.Id);
				if (existing != null)
				{
					existing.Quantity = cartItem.Quantity;
				}
			}

			return cartItem;
		}

		public CartItem? Delete(int id)
		{
			var item = CartItems.FirstOrDefault(ci => ci.Id == id);
			if (item != null)
			{
				var product = ProductServiceProxy.Current.Products
					.FirstOrDefault(p => p.Id == item.ProductId);
				if (product != null)
				{
					product.Quantity += item.Quantity;
				}
				CartItems.Remove(item);
			}
			return item;
		}
		public List<CartItem?> CartItems { get; private set; }
	}
}

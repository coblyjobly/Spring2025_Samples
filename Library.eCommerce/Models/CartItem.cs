using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring2025_Samples.Models
{
	public class CartItem
	{
		public int Id { get; set; }
		public int ProductId { get; set; }
		public string ProductName { get; set; } = string.Empty;
		public int Quantity { get; set; }
		public decimal Price { get; set; }
		public decimal Total => Quantity * Price;
		public string Display => $"{Id}. {ProductName} x{Quantity} @ ${Price} = ${Total}";
		public CartItem()
		{
			ProductId = 0;
			Quantity = 0;
			Price = 0;
		}
		public override string ToString()
		{
			return Display ?? string.Empty;
		}
	}
}

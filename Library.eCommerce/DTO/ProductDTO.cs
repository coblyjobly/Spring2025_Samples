using Spring2025_Samples.Models;
using System;

namespace Library.eCommerce.DTO
{
	public class ProductDTO
	{
		public int Id { get; set; }

		public string? Name { get; set; }

		public decimal Price { get; set; } // ✅ Add this

		public string? Display
		{
			get
			{
				return $"{Id}. {Name} - {Price:C}";
			}
		}

		public ProductDTO()
		{
			Name = string.Empty;
			Price = 0m;
		}

		public ProductDTO(Product p)
		{
			Name = p.Name;
			Id = p.Id;
			Price = 0m; // or pull from p if Product has Price later
		}

		public ProductDTO(ProductDTO p)
		{
			Name = p.Name;
			Id = p.Id;
			Price = p.Price;
		}

		public override string ToString()
		{
			return Display ?? string.Empty;
		}
	}
}

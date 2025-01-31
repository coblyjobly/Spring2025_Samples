using Library.eCommerce.Services;
using Spring2025_Samples.Models;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace MyApp
{
    internal class Program
    {

        static void InventoryManager()
        {
			Console.WriteLine("Welcome to Amazon Inventory Manager!");

			Console.WriteLine("C. Create new inventory item");
			Console.WriteLine("R. Read all inventory items");
			Console.WriteLine("U. Update an inventory item");
			Console.WriteLine("D. Delete an inventory item");
			Console.WriteLine("Q. Quit");

			List<Product?> list = ProductServiceProxy.Current.Products;

			char choice;
			do
			{
				string? input = Console.ReadLine();
				choice = input[0];
				switch (choice)
				{
					case 'C':
					case 'c':
						Console.WriteLine("Enter product name:");
						string name = Console.ReadLine() ?? string.Empty;
						Console.WriteLine("Enter product price:");
						decimal price = decimal.Parse(Console.ReadLine() ?? "0");
						Console.WriteLine("Enter product quantity:");
						int quantity = int.Parse(Console.ReadLine() ?? "0");

						ProductServiceProxy.Current.AddOrUpdate(new Product
						{
							Name = name,
							Price = price,
							Quantity = quantity
						});
						break;
					case 'R':
					case 'r':
						list.ForEach(Console.WriteLine);
						break;
					case 'U':
					case 'u':
						Console.WriteLine("Which product to update?");
						int selection = int.Parse(Console.ReadLine() ?? "-1");
						var selectedProd = list.FirstOrDefault(p => p.Id == selection);

						if (selectedProd != null)
						{
							Console.Write($"New name ({selectedProd.Name}): ");
							var newName = Console.ReadLine();
							if (!string.IsNullOrWhiteSpace(newName))
								selectedProd.Name = newName;

							Console.Write($"New price ({selectedProd.Price}): ");
							if (decimal.TryParse(Console.ReadLine(), out decimal newPrice))
								selectedProd.Price = newPrice;

							Console.Write($"New quantity ({selectedProd.Quantity}): ");
							if (int.TryParse(Console.ReadLine(), out int newQty))
								selectedProd.Quantity = newQty;

							ProductServiceProxy.Current.AddOrUpdate(selectedProd);
						}
						break;
					case 'D':
					case 'd':
						Console.WriteLine("Which product would you like to update?");
						selection = int.Parse(Console.ReadLine() ?? "-1");
						ProductServiceProxy.Current.Delete(selection);
						break;
					case 'Q':
					case 'q':
						break;
					default:
						Console.WriteLine("Error: Unknown Command");
						break;
				}
			} while (choice != 'Q' && choice != 'q');
		}

		static void Storefront()
		{
			Console.WriteLine("Welcome to Amazon Storefront!");

			var cartService = CartServiceProxy.Current;
			var productService = ProductServiceProxy.Current;

			char choice = '0';
			do
			{
				Console.WriteLine("\nOptions:");
				Console.WriteLine("A. Add to Cart");
				Console.WriteLine("V. View Cart");
				Console.WriteLine("U. Update Cart Item");
				Console.WriteLine("R. Remove Cart Item");
				Console.WriteLine("P. View Products");
				Console.WriteLine("Q. Quit");
				Console.Write("Choose an option: ");

				string? input = Console.ReadLine();
				if (string.IsNullOrEmpty(input)) continue;
				choice = input[0];

				switch (char.ToUpper(choice))
				{
					case 'A':
						Console.WriteLine("Enter Product ID:");
						int productId = int.Parse(Console.ReadLine() ?? "0");
						var product = productService.Products.FirstOrDefault(p => p.Id == productId);
						if (product == null)
						{
							Console.WriteLine("Product not found.");
							break;
						}

						Console.WriteLine("Enter Quantity:");
						int qty = int.Parse(Console.ReadLine() ?? "0");
						if (qty <= 0)
						{
							Console.WriteLine("Invalid quantity.");
							break;
						}

						cartService.AddOrUpdate(new CartItem
						{
							ProductId = productId,
							ProductName = product.Name,
							Quantity = qty,
							Price = product.Price
						});
						break;

					case 'V':
						Console.WriteLine("\nYour Cart:");
						cartService.CartItems.ForEach(item => Console.WriteLine(item));
						break;

					case 'U':
						Console.WriteLine("Enter Cart Item ID to update:");
						int cartId = int.Parse(Console.ReadLine() ?? "0");
						var cartItem = cartService.CartItems.FirstOrDefault(ci => ci.Id == cartId);
						if (cartItem == null)
						{
							Console.WriteLine("Item not found.");
							break;
						}

						Console.WriteLine("Enter New Quantity:");
						int newQty = int.Parse(Console.ReadLine() ?? "0");
						cartItem.Quantity = newQty;
						cartService.AddOrUpdate(cartItem);
						break;

					case 'R':
						Console.WriteLine("Enter Cart Item ID to remove:");
						int removeId = int.Parse(Console.ReadLine() ?? "0");
						cartService.Delete(removeId);
						break;

					case 'P':
						Console.WriteLine("\nAvailable Products:");
						productService.Products.ForEach(p => Console.WriteLine(p));
						break;

					case 'Q':
						return;

					default:
						Console.WriteLine("Invalid option.");
						break;
				}
			} while (char.ToUpper(choice) != 'Q');
		}


		static void Main(string[] args)
        {
			Console.WriteLine("Welcome to Amazon");
			Console.WriteLine("I. Manage Inventory");
			Console.WriteLine("S. Access Storefront");
			Console.WriteLine("Q. Quit");

			char choice;
			do
			{
				string? input = Console.ReadLine();
				choice = input[0];
				switch (choice)
				{
					case 'I':
					case 'i':
						InventoryManager();
						break;
					case 'S':
					case 's':
						Storefront();
						break;
					case 'Q':
					case 'q':
						var cartService = CartServiceProxy.Current;
						var cartItems = cartService.CartItems;

						if (!cartItems.Any())
						{
							Console.WriteLine("No items in cart. Exiting...");
							break;
						}

						Console.WriteLine("\n========== Receipt ==========");
						decimal subtotal = 0;
						foreach (var item in cartItems)
						{
							if (item == null) continue;
							Console.WriteLine($"{item.ProductName} x {item.Quantity} @ ${item.Price:F2} each: ${item.Total:F2}");
							subtotal += item.Total;
						}

						decimal taxRate = 0.07m;
						decimal tax = subtotal * taxRate;
						decimal total = subtotal + tax;

						Console.WriteLine("------------------------------");
						Console.WriteLine($"Subtotal: ${subtotal:F2}");
						Console.WriteLine($"Sales Tax (7%): ${tax:F2}");
						Console.WriteLine($"Total: ${total:F2}");
						Console.WriteLine("==============================");
						break;
					default:
						Console.WriteLine("Error: Unknown Command");
						break;
				}
			} while (choice != 'Q' && choice != 'q');

		}
    }


}

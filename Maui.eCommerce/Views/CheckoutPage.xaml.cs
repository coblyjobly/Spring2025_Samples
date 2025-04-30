using System;
using System.Collections.Generic;
using Library.eCommerce.Models;
using Library.eCommerce.Services;
using Microsoft.Maui.Controls;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Maui.eCommerce.Views
{
	public partial class CheckoutPage : ContentPage
	{
		public ObservableCollection<Item?> CartItems { get; set; }
		public decimal TotalPrice { get; set; }

		public CheckoutPage()
		{
			InitializeComponent();

			CartItems = new ObservableCollection<Item?>(
				ShoppingCartService.Current.CartItems.Where(i => i?.Quantity > 0));

			decimal subtotal = CartItems.Sum(item => (item?.Quantity ?? 0) * (item?.Product?.Price ?? 0m));
			decimal tax = subtotal * ShoppingCartService.Current.TaxRate;
			TotalPrice = subtotal + tax;


			BindingContext = this;
		}

		protected override bool OnBackButtonPressed() => true;

	}
}



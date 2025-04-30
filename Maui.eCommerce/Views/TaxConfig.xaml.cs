using Library.eCommerce.Services;
using System;
using Microsoft.Maui.Controls;

namespace Maui.eCommerce.Views
{
	public partial class TaxSettingsPage : ContentPage
	{
		public TaxSettingsPage()
		{
			InitializeComponent();
		}

		private void OnSaveTaxRateClicked(object sender, EventArgs e)
		{
			if (decimal.TryParse(TaxRateEntry.Text, out decimal newTaxRate))
			{
				ShoppingCartService.Current.TaxRate = newTaxRate;
				DisplayAlert("Saved", $"Tax rate updated to {newTaxRate:P}", "OK");
				Navigation.PopAsync();
			}
			else
			{
				DisplayAlert("Error", "Please enter a valid decimal value for tax rate.", "OK");
			}
		}
	}
}


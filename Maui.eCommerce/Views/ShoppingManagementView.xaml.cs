using Library.eCommerce.Services;
using Maui.eCommerce.ViewModels;
using Library.eCommerce.Models;

namespace Maui.eCommerce.Views;

public partial class ShoppingManagementView : ContentPage
{
	public ShoppingManagementView()
	{
		InitializeComponent();
		BindingContext = new ShoppingManagementViewModel();
	}

    private void RemoveFromCartClicked(object sender, EventArgs e)
    {
        (BindingContext as ShoppingManagementViewModel).ReturnItem();
    }
    private void AddToCartClicked(object sender, EventArgs e)
    {
		(BindingContext as ShoppingManagementViewModel).PurchaseItem();
    }

	private void InlineAddClicked(object sender, EventArgs e)
	{
		if (sender is Button btn && btn.BindingContext is Item item)
		{
			for (int i = 0; i < item.AddQuantity; i++)
			{
				ShoppingCartService.Current.AddOrUpdate(item);
			}

			(BindingContext as ShoppingManagementViewModel)?.RefreshUX();
		}
	}

	private async void OnCheckoutClicked(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync("CheckoutPage");
	}
	private async void OnConfigureTaxClicked(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync("TaxSettingsPage"); // make sure Shell route matches
	}


}
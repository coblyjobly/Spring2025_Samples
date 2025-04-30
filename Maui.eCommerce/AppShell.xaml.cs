using Maui.eCommerce.Views;

namespace Maui.eCommerce
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
			Routing.RegisterRoute("CheckoutPage", typeof(Maui.eCommerce.Views.CheckoutPage));
			Routing.RegisterRoute("TaxSettingsPage", typeof(Maui.eCommerce.Views.TaxSettingsPage));

		}
	}
}

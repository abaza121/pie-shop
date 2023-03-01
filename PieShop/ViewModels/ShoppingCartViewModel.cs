namespace PieShop.ViewModels;

using PieShop.Models;

public class ShoppingCartViewModel
{
    public IShoppingCart ShoppingCart { get; }
    public decimal ShoppingCartTotal { get; }

    public ShoppingCartViewModel(IShoppingCart shoppingCart, decimal shoppingCartTotal)
    {
        this.ShoppingCartTotal = shoppingCartTotal;
        this.ShoppingCart = shoppingCart;
    }
}
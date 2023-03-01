namespace PieShop.Models.ShoppingCart;

public class ShoppingCart : IShoppingCart
{
    private readonly PieShopDbContext _pieShopDbContext;
    public string? ShoppingCartId { get; set; }
    public List<ShoppingCartItem> ShoppingCartItems { get; set; } = default!;

    public ShoppingCart(PieShopDbContext pieShopDbContext) => this._pieShopDbContext = pieShopDbContext;

    public void AddToCart(Pie pie)
    {
        
    }

    public int RemoveFromCart(Pie pie) => throw new NotImplementedException();

    public List<ShoppingCartItem> GetShoppingCartItems() => throw new NotImplementedException();

    public void ClearCart() { throw new NotImplementedException(); }

    public decimal GetShoppingCartTotal() => throw new NotImplementedException();
}
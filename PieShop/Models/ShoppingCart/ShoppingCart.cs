namespace PieShop.Models;

using Microsoft.EntityFrameworkCore;

public class ShoppingCart : IShoppingCart
{
    private readonly PieShopDbContext _pieShopDbContext;
    public string? ShoppingCartId { get; set; }
    public List<ShoppingCartItem>? ShoppingCartItems { get; set; }
    
    public static ShoppingCart GetCart(IServiceProvider services)
    {
        var session = services.GetService<IHttpContextAccessor>()?.HttpContext?.Session;
        var dbContext = services.GetService<PieShopDbContext>();
        if(session == null || dbContext == null) throw new Exception("Failed to initialize Session");

        var cartId = session.GetString("CartId");
        if(cartId == null)
        {
            cartId = Guid.NewGuid().ToString();
            session.SetString("CartId", cartId);
        }
        
        var shoppingCart = new ShoppingCart(dbContext);
        shoppingCart.ShoppingCartId = cartId;
        return shoppingCart;
    }

    public ShoppingCart(PieShopDbContext pieShopDbContext) => this._pieShopDbContext = pieShopDbContext;

    public void AddToCart(Pie pie)
    {
        var targetItem = _pieShopDbContext.ShoppingCartItems.SingleOrDefault(item => item.Pie.PieId == pie.PieId && item.ShoppingCartId == this.ShoppingCartId);
        if(targetItem != null) targetItem.Amount++;
        else
        {
            var newItem = new ShoppingCartItem() {Pie = pie, ShoppingCartId = this.ShoppingCartId, Amount = 1};
            this._pieShopDbContext.ShoppingCartItems.Add(newItem);
        }

        this._pieShopDbContext.SaveChanges();
    }

    public int RemoveFromCart(Pie pie)
    {
        var localAmount = 0;
        var shoppingCartItem = _pieShopDbContext.ShoppingCartItems.SingleOrDefault(item => item.Pie.PieId == pie.PieId && item.ShoppingCartId == this.ShoppingCartId);
        if(shoppingCartItem == null) return localAmount;

        if(shoppingCartItem.Amount > 1)
        {
            shoppingCartItem.Amount--;
            localAmount = shoppingCartItem.Amount;
        }
        else _pieShopDbContext.ShoppingCartItems.Remove(shoppingCartItem);

        _pieShopDbContext.SaveChanges();

        return localAmount;
    }

    public List<ShoppingCartItem> GetShoppingCartItems() => ShoppingCartItems ??= _pieShopDbContext.ShoppingCartItems.Where(item => item.ShoppingCartId == this.ShoppingCartId).Include(item => item.Pie).ToList();


    public void ClearCart()
    {
        var cartItems = _pieShopDbContext.ShoppingCartItems.Where(cartItem => cartItem.ShoppingCartId == ShoppingCartId);
        _pieShopDbContext.RemoveRange(cartItems);
        _pieShopDbContext.SaveChanges();
    }

    public decimal GetShoppingCartTotal() => _pieShopDbContext.ShoppingCartItems.Where(cartItem => cartItem.ShoppingCartId == ShoppingCartId).Select(cartItem => cartItem.Pie.Price * cartItem.Amount).Sum();
}
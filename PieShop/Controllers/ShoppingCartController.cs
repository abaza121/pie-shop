using Microsoft.AspNetCore.Mvc;

namespace PieShop.Controllers;

using PieShop.Models;
using PieShop.ViewModels;

public class ShoppingCartController : Controller
{
    private readonly IShoppingCart shoppingCart;
    private readonly IPieRepository pieRepository;

    public ShoppingCartController(IPieRepository pieRepository, IShoppingCart shoppingCart)
    {
        this.shoppingCart = shoppingCart;
        this.pieRepository = pieRepository;
    }
    
    // GET
    public IActionResult Index()
    {
        var items = shoppingCart.GetShoppingCartItems();
        var shoppingCartViewModel = new ShoppingCartViewModel(shoppingCart, shoppingCart.GetShoppingCartTotal());
        return View(shoppingCartViewModel);
    }

    public RedirectToActionResult AddToShoppingCart(int pieId)
    {
        var pie = pieRepository.GetPieById(pieId);
        if(pie != null) shoppingCart.AddToCart(pie);
        return RedirectToAction("Index");
    }

    public RedirectToActionResult RemoveFromShoppingCart(int pieId)
    {
        var pie = pieRepository.GetPieById(pieId);
        if(pie != null) shoppingCart.RemoveFromCart(pie);
        return RedirectToAction("Index");
    }
}
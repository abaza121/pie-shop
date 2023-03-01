using Microsoft.AspNetCore.Mvc;

namespace PieShop.Controllers;

using PieShop.ViewModels;
using PieShop.Models;

public class HomeController : Controller
{
    private readonly IPieRepository _pieRepository;
    public HomeController(IPieRepository pieRepository)
    {
        _pieRepository = pieRepository;
    }
    
    // GET
    public IActionResult Index()
    {
        var viewModel=new HomeViewModel(_pieRepository.PiesOfTheWeek);
        return View(viewModel);
    }
}
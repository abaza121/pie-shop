using Microsoft.AspNetCore.Mvc;

namespace PieShop.Controllers;

using PieShop.Models;
using PieShop.ViewModels;

public class PieController : Controller
{
    private readonly IPieRepository _pieRepository;
    private readonly ICategoryRepository _categoryRepository;

    public PieController(IPieRepository pieRepository, ICategoryRepository categoryRepository)
    {
        _pieRepository = pieRepository;
        _categoryRepository = categoryRepository;
    }

    public IActionResult List()
    {
        return View(new PieListViewModel(_pieRepository.AllPies,_categoryRepository.AllCategories.First().CategoryName));
    }
}
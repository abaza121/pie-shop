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
        return View(new PieListViewModel(_pieRepository.AllPies,"All Pies"));
    }

    public IActionResult Details(int id)
    {
        var pie = _pieRepository.GetPieById(id);
        if(pie == null) return NotFound();
        return View(pie);
    }
}
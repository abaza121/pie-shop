namespace PieShop.ViewModels;

using PieShop.Models;

public class HomeViewModel
{
    public IEnumerable<Pie> PiesOfTheWeek { get; }
    
    public HomeViewModel(IEnumerable<Pie> piesOfTheWeek)
    {
        this.PiesOfTheWeek=piesOfTheWeek;
    }
}
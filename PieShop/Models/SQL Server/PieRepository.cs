namespace PieShop.Models;

using Microsoft.EntityFrameworkCore;

public class PieRepository : IPieRepository
{
    private readonly PieShopDbContext _pieShopDbContext;

    public PieRepository(PieShopDbContext dbContext)
    {
        this._pieShopDbContext=dbContext;
    }

    public IEnumerable<Pie> AllPies => _pieShopDbContext.Pies.Include(c => c.Category);
    public IEnumerable<Pie> PiesOfTheWeek => _pieShopDbContext.Pies.Include(c => c.Category).Where(pie => pie.IsPieOfTheWeek);
    public Pie? GetPieById(int pieId) => _pieShopDbContext.Pies.FirstOrDefault(x => x.PieId == pieId);
}
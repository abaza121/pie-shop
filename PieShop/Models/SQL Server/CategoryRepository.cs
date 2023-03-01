namespace PieShop.Models;

public class CategoryRepository : ICategoryRepository
{
    private readonly PieShopDbContext _pieShopDbContext;

    public CategoryRepository(PieShopDbContext dbContext)
    {
        this._pieShopDbContext=dbContext;
    }

    public IEnumerable<Category> AllCategories => _pieShopDbContext.Categories.OrderBy(p => p.CategoryName);
}
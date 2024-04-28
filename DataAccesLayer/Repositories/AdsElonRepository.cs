using DataAccesLayer.Datas;
using DataAccesLayer.Interfaces;
using DataAccesLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccesLayer.Repositories;

public class AdsElonRepository : Repository<AdsElon>, IAdsElonInterface
{
    public AdsElonRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IEnumerable<AdsElon>> GetAllWithDependenciesAsync()
        => await _dbContext.AdsElons
            .Include(t => t.SubCategory)
            .Include(t => t.SubRegionNavigation)
            .Include(t => t.Images)
            .Include(t => t.User)
            .ToListAsync();
}

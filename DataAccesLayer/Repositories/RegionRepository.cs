

using DataAccesLayer.Datas;
using DataAccesLayer.Interfaces;
using DataAccesLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccesLayer.Repositories;

public class RegionRepository : Repository<Region>, IRegionInterface
{
    public RegionRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IEnumerable<Region>> GetAllRegionsWithSubRegionsAsync()
        => await _dbContext.Regions.Include(x => x.SubRegions)  
                                .ToListAsync();
}

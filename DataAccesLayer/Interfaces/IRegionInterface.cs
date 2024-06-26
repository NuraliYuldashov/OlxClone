﻿

using DataAccesLayer.Models;

namespace DataAccesLayer.Interfaces;

public interface IRegionInterface:IRepository<Region>
{
    Task<IEnumerable<Region>> GetAllRegionsWithSubRegionsAsync();
}

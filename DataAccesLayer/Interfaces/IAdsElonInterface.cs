using DataAccesLayer.Models;

namespace DataAccesLayer.Interfaces;

public interface IAdsElonInterface:IRepository<AdsElon>
{
    Task<IEnumerable<AdsElon>> GetAllWithDependenciesAsync();
}
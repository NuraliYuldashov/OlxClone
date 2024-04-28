

using DataAccesLayer.Datas;
using DataAccesLayer.Interfaces;
using DataAccesLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccesLayer.Repositories;

public class Repository<TEntity>(AppDbContext dbContext) 
    : IRepository<TEntity> where TEntity : BaseEntity
{
    protected readonly AppDbContext _dbContext = dbContext;

    public async Task<TEntity?> AddAsync(TEntity entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        await _dbContext.Set<TEntity>().AddAsync(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task DeleteAsync(TEntity entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        _dbContext.Set<TEntity>().Remove(entity);
        await _dbContext.SaveChangesAsync();
    }

    public Task<IEnumerable<TEntity>> Filter(Func<TEntity, bool> predicate)
    {
        var entities = _dbContext.Set<TEntity>()
            .AsNoTracking()
            .Where(predicate);
        return Task.FromResult(entities);
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _dbContext.Set<TEntity>()
                               .AsNoTracking()
                               .ToListAsync();
    }

    public async Task<TEntity?> GetByIdAsync(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("Id must be greater than zero.", nameof(id));
        }

        return  await  _dbContext.Set<TEntity>()
                                 .AsNoTracking()
                                 .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task UpdateAsync(TEntity entity)
    {
        _dbContext.Set<TEntity>().Update(entity);
        await _dbContext.SaveChangesAsync();
    }
}

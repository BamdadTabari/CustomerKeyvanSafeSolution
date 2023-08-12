using KeyvanSafe.Shared.EntityFramework.Entities;
using System.Linq.Expressions;

namespace KeyvanSafe.Domain.EntityFramework.Interfaces;

public interface IRepository<BaseEntity> where BaseEntity : class, IBaseEntity
{
    Task<bool> ExistsAsync(int id);
    Task<bool> ExistsAsync(Expression<Func<BaseEntity, bool>> predicate);

    void Add(BaseEntity entity);
    void AddRange(IEnumerable<BaseEntity> entities);
    void Remove(BaseEntity entity);
    void RemoveRange(IEnumerable<BaseEntity> entities);
    void Update(BaseEntity entity);
    void UpdateRange(IEnumerable<BaseEntity> entities);

    Task AddAsync(BaseEntity entity);
    Task AddRangeAsync(IEnumerable<BaseEntity> entities);
}
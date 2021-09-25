using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PsaVideoGameCommon;

namespace PsaVideoGameDataProvider
{
  public interface IPsaRepository<T> where T : class, IEntity
  {
    DbSet<T> DbSet { get; }
    IEnumerable<T> GetAll(params Expression<Func<T, object>>[] includes);
    bool Any(Expression<Func<T, bool>> filter);
    Task<bool> AnyAsync(Expression<Func<T, bool>> filter);
    Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes);
    IEnumerable<T> GetAllWithFilter(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includes);
    Task<IEnumerable<T>> GetAllWithFilterAsync(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includes);
    T GetItemById(int id, params Expression<Func<T, object>>[] include);
    Task<T> GetItemByIdAsync(int id, params Expression<Func<T, object>>[] include);
    T FirstOrDefault(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] include);
    Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] include);
    T LastOrDefault(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] include);
    Task<T> LastOrDefaultAsync(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] include);
    void Insert(T entity);
    void Update(T entity);
    void Delete(int id);
    void CommitChange();
    void CommitChangeAsync();


  }
}

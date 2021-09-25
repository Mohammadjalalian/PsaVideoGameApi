using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PsaVideoGameCommon;

namespace PsaVideoGameDataProvider
{
  public class PsaRepository<T> : IDisposable, IPsaRepository<T> where T : class, IEntity
  {
    protected DbContext Context;
    protected OrderElement<T>[] Orders;

    public DbSet<T> DbSet { get; private set; }



    public PsaRepository(DbContext context, DbSet<T> dbSet)
    {
      Context = context;
      DbSet = dbSet;
    }

    private bool _disposed = false;


    protected virtual void Dispose(bool disposing)
    {
      if (!this._disposed)
      {
        if (disposing)
        {
          Context.Dispose();
        }
      }

      this._disposed = true;
    }

    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }


    public virtual IEnumerable<T> GetAll(params Expression<Func<T, object>>[] includes)
    {
      IQueryable<T> tcEntities = DbSet;
      if (Orders != null)
      {
        tcEntities = tcEntities.OrderByMultiple(Orders);
      }

      if (includes != null)
        return tcEntities.IncludeMultiple(includes).ToList();
      return tcEntities.ToList();
    }
    public virtual bool Any(Expression<Func<T, bool>> filter)
    {
      return DbSet.Any(filter);
    }

    public virtual async Task<bool> AnyAsync(Expression<Func<T, bool>> filter)
    {
      return await DbSet.AnyAsync(filter);
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
    {
      IQueryable<T> tcEntities = DbSet;
      if (Orders != null)
      {
        tcEntities = tcEntities.OrderByMultiple(Orders);
      }
      
      if (includes != null)
        return await tcEntities.IncludeMultiple(includes).ToListAsync();
      return await tcEntities.ToListAsync();
    }

    public IEnumerable<T> GetAllWithFilter(Expression<Func<T, bool>> filter,
      params Expression<Func<T, object>>[] includes)
    {
      IQueryable<T> tcEntities = DbSet;
      
      if (includes != null && Orders != null)
        return tcEntities.OrderByMultiple(Orders).IncludeMultiple(includes).Where(filter).ToList();
      if (includes != null)
        return tcEntities.IncludeMultiple(includes).Where(filter).ToList();
      if (Orders != null)

        return tcEntities.OrderByMultiple(Orders).Where(filter).ToList();

      return tcEntities.Where(filter).ToList();
    }

    public T FirstOrDefault(Expression<Func<T, bool>> filter,
      params Expression<Func<T, object>>[] includes)
    {
      IQueryable<T> tcEntities = DbSet;

      if (includes != null && Orders != null)
        return tcEntities.OrderByMultiple(Orders).IncludeMultiple(includes).FirstOrDefault(filter);
      if (includes != null)
        return tcEntities.IncludeMultiple(includes).FirstOrDefault(filter);
      if (Orders != null)

        return tcEntities.OrderByMultiple(Orders).FirstOrDefault(filter);

      return tcEntities.FirstOrDefault(filter);
    }

    public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> filter,
      params Expression<Func<T, object>>[] includes)
    {
      IQueryable<T> tcEntities = DbSet;
      if (Orders != null)
      {
        tcEntities = tcEntities.OrderByMultiple(Orders);
      }

      if (includes != null)
        return await tcEntities.IncludeMultiple(includes).FirstOrDefaultAsync(filter);
      return await tcEntities.FirstOrDefaultAsync(filter);
    }public T LastOrDefault(Expression<Func<T, bool>> filter,
      params Expression<Func<T, object>>[] includes)
    {
      IQueryable<T> tcEntities = DbSet;

      if (includes != null && Orders != null)
        return tcEntities.OrderByMultiple(Orders).IncludeMultiple(includes).LastOrDefault(filter);
      if (includes != null)
        return tcEntities.IncludeMultiple(includes).LastOrDefault(filter);
      if (Orders != null)

        return tcEntities.OrderByMultiple(Orders).LastOrDefault(filter);

      return tcEntities.LastOrDefault(filter);
    }

    public async Task<T> LastOrDefaultAsync(Expression<Func<T, bool>> filter,
      params Expression<Func<T, object>>[] includes)
    {
      IQueryable<T> tcEntities = DbSet;
      if (Orders != null)
      {
        tcEntities = tcEntities.OrderByMultiple(Orders);
      }

      if (includes != null)
        return await tcEntities.IncludeMultiple(includes).LastOrDefaultAsync(filter);
      return await tcEntities.LastOrDefaultAsync(filter);
    }
    public async Task<IEnumerable<T>> GetAllWithFilterAsync(Expression<Func<T, bool>> filter,
      params Expression<Func<T, object>>[] includes)
    {
      IQueryable<T> tcEntities = DbSet;
      if (Orders != null)
      {
        tcEntities = tcEntities.OrderByMultiple(Orders);
      }

      if (includes != null)
        return await tcEntities.IncludeMultiple(includes).Where(filter).ToListAsync();
      return await tcEntities.Where(filter).ToListAsync();
    }

    public virtual T GetItemById(int id, params Expression<Func<T, object>>[] includes)
    {
      //throw new NotImplementedException();
      IQueryable<T> tcEntities = DbSet;
   
      if (includes != null)
        return tcEntities.IncludeMultiple(includes).FirstOrDefault(a => a.Id == id);
      return tcEntities.FirstOrDefault(a => a.Id == id);
    }


    public virtual async Task<T> GetItemByIdAsync(int id, params Expression<Func<T, object>>[] includes)
    {
      IQueryable<T> tcEntities = DbSet;
  
      if (includes != null)
        return await tcEntities.IncludeMultiple(includes).FirstOrDefaultAsync(a => a.Id == id);
      //throw new NotImplementedException();
      return await DbSet.FirstOrDefaultAsync(a => a.Id == id);
    }
    public virtual void Insert(T entity)
    {
      entity.CreationTime = DateTime.Now;
      entity.ModificationTime = DateTime.Now;
      DbSet.Add(entity);
    }

    public virtual void Update(T entity)
    {
      entity.ModificationTime = DateTime.Now;
      Context.Entry(entity).State = EntityState.Modified;
    }

    public virtual void Delete(int id)
    {
      DbSet.Remove(GetItemById(id));
    }

    public virtual void CommitChange()
    {
      Context.SaveChanges();
    }
    public virtual async void CommitChangeAsync()
    {
      await Context.SaveChangesAsync();
    }
  }

  public static class QueryableExtension
  {
    public static IQueryable<T> IncludeMultiple<T>(this IQueryable<T> query, params Expression<Func<T, object>>[] includes)
      where T : class
    {
      if (includes != null)
      {
        query = includes.Aggregate(query,
          (current, include) => current.Include<T, object>(include));
      }

      return query;
    }
    public static IQueryable<T> OrderByMultiple<T>(this IQueryable<T> query, params OrderElement<T>[] orders)
      where T : class
    {
      if (orders != null)
      {
        query = orders.Aggregate(query,
          (current, order) => order.Ascending ?
                current.OrderBy<T, object>(order.Expression) :
                current.OrderByDescending<T, object>(order.Expression));
      }

      return query;
    }


  }

  public class OrderElement<T>
  {
    public Expression<Func<T, object>> Expression { get; set; }
    public bool Ascending { get; set; }
  }




}

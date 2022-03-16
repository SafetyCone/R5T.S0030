using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace R5T.S0030
{
    public abstract class FileSet
    {
        abstract public Task Save();
    }

    public abstract class FileSet<TEntity> : FileSet, IEnumerable<TEntity>
        // Use a class constraint to ensure modifying the returned object reference will modify the object in the file set.
        where TEntity : class
    {
        abstract public IEnumerator<TEntity> GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
        abstract public TEntity Add(TEntity entity);
        abstract public Task AddRange(IEnumerable<TEntity> entities);
        abstract public TEntity Remove(TEntity entity);
        abstract public void RemoveRange(IEnumerable<TEntity> entities);
        abstract public TEntity Update(TEntity entity);
        abstract public void UpdateRange(IEnumerable<TEntity> entities);
    }
}

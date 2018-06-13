using System.Collections.Generic;

namespace EngineerTest.Data.Models.Repository
{
    public interface IDataRepository<TEntity, U> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> GetAll(string filter);
        TEntity Get(U id);
        int Add(List<TEntity> b);
        int Update(U id, TEntity b);
        int Delete(U id);
    }
}

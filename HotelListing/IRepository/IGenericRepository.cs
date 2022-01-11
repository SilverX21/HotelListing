using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HotelListing.IRepository
{
    public interface IGenericRepository<T> where T : class  //T vai ser uma classe qualquer
    {
        /// <summary>
        /// Vou buscar uma lista de objetos
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="orderBy"></param>
        /// <param name="includes"></param>
        /// <returns>lista de objetos</returns>
        Task<IList<T>> GetAll(Expression<Func<T, bool>> expression = null, 
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, 
            List<string> includes = null
        );

        /// <summary>
        /// Vou buscar um objeto
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="includes"></param>
        /// <returns>um determinado objeto</returns>
        Task<T> Get(Expression<Func<T, bool>> expression = null, List<string> includes = null);

        /// <summary>
        /// Adiciono à BD um registo
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task Insert(T entity);

        /// <summary>
        /// Adiciono vários registos, à BD, de uma só vez
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task InsertRange(IEnumerable<T> entities);

        /// <summary>
        /// Elimino um determinado registo da BD
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task Delete(int id);

        /// <summary>
        /// Elimino vários registos da BD
        /// </summary>
        /// <param name="entities"></param>
        void DeleteRange(IEnumerable<T> entities);

        /// <summary>
        /// Atualiza um determinado registo na BD
        /// </summary>
        /// <param name="entity"></param>
        void Update(T entity);

    }
}

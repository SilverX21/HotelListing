using HotelListing.Data;
using HotelListing.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HotelListing.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        //aqui vamos tratar de utilizar Dependency Injection, para que possamos definir 
        //as classes a utilizar
        private readonly DatabaseContext _context;
        private readonly DbSet<T> _db;

        //aqui vamos utilizar DI, basta inicializar aqui no construtor e depois metemos as coisas no startup.cs
        public GenericRepository(DatabaseContext context)
        {
            _context = context; //o do lado direito deriva da DI, que está definida no startup.cs
            _db = _context.Set<T>(); //aqui o Set vai depender do que está a utilizar no momento, o DI vai tratar disso tbm
        }


        public async Task Delete(int id)
        {
            var entity = await _db.FindAsync(id);
            _db.Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            _db.RemoveRange(entities);
        }

        /// <summary>
        /// Retorna uma entidade
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="includes">se pretende que sejam incluídos detalhes adicionais, por ex: os detalhes do país do hotel</param>
        /// <returns></returns>
        public async Task<T> Get(Expression<Func<T, bool>> expression = null, List<string> includes = null)
        {
            IQueryable<T> query = _db;

            //aqui vamos ver se temos alguma coisa que queremos incluir na query
            //caso tenha ele vai incluir na query para puxar os dados da BD
            if (includes != null)
            {
                foreach (var includeProperty in includes)
                {
                    query = query.Include(includeProperty);
                }
            }

            //o AsNoTracking diz à base de dados para não seguir esta query, ou seja, não é criado nada na cache
            //o expression, que vem como parâmetro, vem com os detalhes a serem utilizados no linq! aquela expressão, por ex: x => x.Id
            return await query.AsNoTracking().FirstOrDefaultAsync(expression);
        }

        public async Task<IList<T>> GetAll(Expression<Func<T, bool>> expression = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, List<string> includes = null)
        {
            IQueryable<T> query = _db;

            //aqui vamos filtrar pela expression, ou seja, os detalhes que queremos ver
            //por exemplo: x => x.Id = 123 
            if (expression != null)
            {
                query = query.Where(expression);
            }

            if (includes != null)
            {
                foreach (var includeProperty in includes)
                {
                    query = query.Include(includeProperty);
                }
            }

            //aqui vamos ordenar pelo pretendido (asc, desc e por qual campo querem ordenar)
            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return await query.AsNoTracking().ToListAsync();
        }

        public async Task Insert(T entity)
        {
            await _db.AddAsync(entity);
        }

        public async Task InsertRange(IEnumerable<T> entities)
        {
            await _db.AddRangeAsync(entities);
        }

        public async void Update(T entity)
        {
            //o attach basicamente diz à base de dados para prestar atenção a este registo e alterar caso encontre algo diferente
            _db.Attach(entity);
            //depois aqui dizemos ao context (BD) que houve uma alteração no state da mesma (que houve um update)
            //depois disso ela irá tratar de correr um update
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}

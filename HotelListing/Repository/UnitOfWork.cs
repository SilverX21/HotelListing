using HotelListing.Data;
using HotelListing.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelListing.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _context;
        private IGenericRepository<Country> _countries;
        private IGenericRepository<Hotel> _hotels;

        public UnitOfWork(DatabaseContext context)
        {
            _context = context;
        }

        //aqui passamos as classes para o generic repository, para que este possa saber que tabelas utilizar e o contexto em que se encontra
        //??= -> basicamente é um if onde verifica se não é null o lado esquerdo, caso seja verdade aplica o que está à direita
        public IGenericRepository<Country> Countries => _countries ??= new GenericRepository<Country>(_context);

        public IGenericRepository<Hotel> Hotels => _hotels ??= new GenericRepository<Hotel>(_context);

        /// <summary>
        /// aqui faço o dispose da memória, para libertar memória e melhorar a performance
        /// </summary>
        public void Dispose()
        {
            //o dispose faz com que seja libertada a memória, é muito importante usar isto para não se ter problemas de performance
            _context.Dispose();
            //GC -> Garbage Collector
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Aqui guardamos as alterações feitas à BD
        /// </summary>
        /// <returns></returns>
        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}

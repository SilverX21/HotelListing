using HotelListing.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelListing.IRepository
{
    //aqui vamos basicamente registar as alterações feitas, referentes ao repositorio IGenericRepository
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Country> Countries { get; }
        IGenericRepository<Hotel> Hotels { get; }
        Task Save(); //aqui gravamos as alterações feitas à BD, só depois é que as alterações são feitas
    }
}

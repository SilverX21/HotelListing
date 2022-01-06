using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelListing.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        //coloca-se o nome das tabelas no plural
        public DbSet<Country> Countries { get; set; }
        public DbSet<Hotel> Hotels { get; set; }

        #region Seed da base de dados
        //aqui fazemos o seed da base de dados para ter uns dados iniciais para testar
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Country>().HasData(
                new Country
                {
                    Id = 1,
                    Name = "Portugal",
                    ShortName = "PT"
                },
                new Country
                {
                    Id = 2,
                    Name = "Espanha",
                    ShortName = "ES"
                },
                new Country
                {
                    Id = 3,
                    Name = "Suíça",
                    ShortName = "SUI"
                }
            );

            builder.Entity<Hotel>().HasData(
                new Hotel
                {
                    Id = 1,
                    Name = "Hotel Turismo Realense",
                    Address = "Rua do Aço",
                    CountryId = 1,
                    Rating = 4.9
                },
                new Hotel
                {
                    Id = 2,
                    Name = "Hotel Pirâmide",
                    Address = "Avenida de Sanxenxo",
                    CountryId = 2,
                    Rating = 4.4
                },
                new Hotel
                {
                    Id = 3,
                    Name = "Hotel Kandersteg",
                    Address = "Rua de Kandersteg Escutista",
                    CountryId = 3,
                    Rating = 4.8
                }
            );
        }
        #endregion
    }
}

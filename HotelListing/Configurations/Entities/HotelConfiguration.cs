using HotelListing.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelListing.Configurations.Entities
{
    public class HotelConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new Hotel { Id = 1, Name = "Hotel Turismo Realense", Address = "Rua do Aço", CountryId = 1, Rating = 4.9 },
                new Hotel { Id = 2, Name = "Hotel Pirâmide", Address = "Avenida de Sanxenxo", CountryId = 2, Rating = 4.4 },
                new Hotel { Id = 3, Name = "Hotel Kandersteg",Address = "Rua de Kandersteg Escutista",CountryId = 3,Rating = 4.8}
            );
        }
    }
}

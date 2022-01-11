using AutoMapper;
using HotelListing.Data;
using HotelListing.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelListing.Configurations
{
    //Esta é a classe que vai inicializar o automapper e que vai fazer o
    ////mapeamento das classes para as DTOS e vice versa
    public class MapperInitializer : Profile
    {
        //Aqui definimos quais classes é que vão ser mapeadas
        //depois temos de colocar no startup para que seja logo inicializado quando a API iniciar
        public MapperInitializer()
        {
            CreateMap<Country, CountryDTO>().ReverseMap();
            CreateMap<Country, CreateCountryDTO>().ReverseMap();
            CreateMap<Hotel, HotelDTO>().ReverseMap();
            CreateMap<Hotel, CreateHotelDTO>().ReverseMap();
        }
    }
}

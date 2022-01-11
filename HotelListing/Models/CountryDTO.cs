using HotelListing.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelListing.Models
{
    //Uma DTO é uma classe espelho, a qual vai ter as informações a serem passadas para o utilizador
    //estas informações podem ter ou não todos os campos e serve para dar uma maior camada de segurança
    //podemos meter aqui detalhes como tamanhos máximos, requireds, entre outras coisas
    //DTOs não falam com os Models normais, Como Country e Hotel, apenas comunicam entre si
    public class CountryDTO : CreateCountryDTO
    {
        public int Id { get; set; }
    }
    
    //aqui temos uma DTO para quando criamos um país, visto que não é preciso o Id na hora em que é criado, não o colocamos aqui
    public class CreateCountryDTO
    {
        [Required]
        [StringLength(maximumLength: 50, ErrorMessage = "Country name is too long!")]
        public string Name { get; set; }
        [Required]
        [StringLength(maximumLength: 3, ErrorMessage = "Short country name is too long!")]
        public string ShortName { get; set; }

        public IList<HotelDTO> Hotels { get; set; }
    }
}

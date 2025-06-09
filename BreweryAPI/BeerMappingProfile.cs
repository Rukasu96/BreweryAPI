using AutoMapper;
using BreweryAPI.Entities;
using BreweryAPI.Models.Beers;

namespace BreweryAPI
{
    public class BeerMappingProfile : Profile
    {
        public BeerMappingProfile()
        {
            CreateMap<Beer, BeerDto>();
            CreateMap<CreatedBeerDto, Beer>();
            CreateMap<BeerUpdateDto, Beer>();
        }
    }
}

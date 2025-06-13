using AutoMapper;
using BreweryAPI.Entities;
using BreweryAPI.Models.Beers;
using Microsoft.EntityFrameworkCore;

namespace BreweryAPI.Services
{
    public interface IClientService
    {
        List<BeerDto> GetAllWholesalersBeers(Guid wholesalerId);
        BeerDto GetWholesalerBeerById(Guid wholesalerId, int id);
    }

    public class ClientService : IClientService
    {
        private readonly BreweryContext context;
        private readonly IMapper mapper;
        public ClientService(BreweryContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public List<BeerDto> GetAllWholesalersBeers(Guid wholesalerId)
        {
            var wholesaler = context.Wholesalers.Include(x => x.Beers).FirstOrDefault(x => x.Id == wholesalerId);
            List<BeerDto> beers = new List<BeerDto>();

            foreach (var beer in wholesaler.Beers)
            {
                BeerDto beerDto = mapper.Map<BeerDto>(beer);

                if (beers.Any(x => x.Name == beerDto.Name))
                {
                    beers.First(x => x.Name == beerDto.Name).Quantity++;
                }
                else
                {
                    beers.Add(beerDto);
                }
            }

            return beers;
        }

        public BeerDto GetWholesalerBeerById(Guid wholesalerId, int id)
        {
            var wholesaler = context.Wholesalers.Include(x => x.Beers).FirstOrDefault(x => x.Id == wholesalerId);
            var beer = wholesaler.Beers.FirstOrDefault(x => x.Id == id);
            BeerDto beerDto = mapper.Map<BeerDto>(beer);

            foreach (var wholesalerBeer in wholesaler.Beers)
            {
                if(wholesalerBeer.Name == beerDto.Name)
                {
                    beerDto.Quantity++;
                }
            }

            return beerDto;
        }
    }
}

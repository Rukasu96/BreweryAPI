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
        void AddBeer(Guid wholesalerId, int beerId);
        List<Beer> GetClientsWholesalerBeers(Guid wholesalerId);
        public void DeleteAllBeers();
        public void DeleteBeerId(int beerId);
    }

    public class ClientService : IClientService
    {
        private readonly dbContext context;
        private readonly IMapper mapper;
        private readonly IUserContextService userContext;
        public ClientService(dbContext context, IMapper mapper, IUserContextService userContext)
        {
            this.context = context;
            this.mapper = mapper;
            this.userContext = userContext;
        }

        public List<BeerDto> GetAllWholesalersBeers(Guid wholesalerId)
        {
            //var wholesaler = context.Wholesalers.Include(x => x.Beers).FirstOrDefault(x => x.Id == wholesalerId);
            //List<BeerDto> beers = new List<BeerDto>();

            //foreach (var beer in wholesaler.Beers)
            //{
            //    BeerDto beerDto = mapper.Map<BeerDto>(beer);

            //    if (beers.Any(x => x.Name == beerDto.Name))
            //    {
            //        beers.First(x => x.Name == beerDto.Name).Quantity++;
            //    }
            //    else
            //    {
            //        beers.Add(beerDto);
            //    }
            //}

            //return beers;
            return null;
        }

        public BeerDto GetWholesalerBeerById(Guid wholesalerId, int beerId)
        {
            //var wholesaler = context.Wholesalers.Include(x => x.Beers).FirstOrDefault(x => x.Id == wholesalerId);
            //var beer = wholesaler.Beers.FirstOrDefault(x => x.Id == beerId);
            //BeerDto beerDto = mapper.Map<BeerDto>(beer);

            //foreach (var wholesalerBeer in wholesaler.Beers)
            //{
            //    if(wholesalerBeer.Name == beerDto.Name)
            //    {
            //        beerDto.Quantity++;
            //    }
            //}

            //return beerDto;
            return null;
        }

        public void AddBeer(Guid wholesalerId, int beerId)
        {
            //var wholesaler = context.Wholesalers.Include(x => x.Beers).FirstOrDefault(x => x.Id == wholesalerId);
            //var beer = wholesaler.Beers.FirstOrDefault(x => x.Id == beerId);

            //var client = context.Clients.First(x => x.Id == userContext.GetUserId);
            //client.Beers.Add(beer);
            ////minus beer from wholesaler
            //context.SaveChanges();
        }

        public List<Beer> GetClientsWholesalerBeers(Guid wholesalerId)
        {
            var client = context.Clients.First(x => x.Id == userContext.GetUserId);
            
            return client.Beers;
        }

        public void DeleteAllBeers()
        {
            var client = context.Clients.First(x => x.Id == userContext.GetUserId);
            client.Beers.Clear();
            context.SaveChanges();
        }

        public void DeleteBeerId(int beerId)
        {
            var client = context.Clients.First(x => x.Id == userContext.GetUserId);
            var beer = client.Beers.First(x => x.Id == beerId);
            client.Beers.Remove(beer);
            context.SaveChanges();
        }
    }
}

using AutoMapper;
using BreweryAPI.Entities;
using BreweryAPI.Models.Beers;

namespace BreweryAPI.Services
{
    public interface IBeerService
    {
        void CreateBeer(CreatedBeerDto dto);
        void UpdateBeer(BeerUpdateDto dto, int id);
        void DeleteBeer(int id);
    }

    public class BreweryBeerService : IBeerService
    {
        private readonly BreweryContext context;
        private readonly IMapper mapper;
        private readonly IUserContextService userContext;
        public BreweryBeerService(BreweryContext context, IMapper mapper, IUserContextService userContext)
        {
            this.context = context;
            this.mapper = mapper;
            this.userContext = userContext;
        }

        public void CreateBeer(CreatedBeerDto dto)
        {
            var beer = mapper.Map<Beer>(dto);
            var brewery = context.Breweries.First(x => x.Id == userContext.GetUserId);

            beer.BreweryId = brewery.Id;
            beer.Brewery = brewery;

            var type = context.BeerTypes.FirstOrDefault(x => x.Id == dto.BeerTypeId);
            beer.Type = type;

            if(beer.Id == 0)
            {
                beer.Id = 1;
            }

            context.Beers.Add(beer);
            context.SaveChanges();
        }

        public void DeleteBeer(int id)
        {
            var beer = context.Beers.FirstOrDefault(x => x.Id == id);
            
            context.Beers.Remove(beer);
            context.SaveChanges();
        }

        public void UpdateBeer(BeerUpdateDto dto, int id)
        {
            var beer = context.Beers.FirstOrDefault(x => x.Id == id);
            beer = mapper.Map<Beer>(dto);

            context.SaveChanges();
        }
    }
}

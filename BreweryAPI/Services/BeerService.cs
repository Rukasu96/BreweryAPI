using AutoMapper;
using BreweryAPI.Entities;
using BreweryAPI.Models;

namespace BreweryAPI.Services
{
    public interface IBeerService
    {
        void CreateBeer(CreatedBeerDto dto);
        void UpdateBeer();
        void DeleteBeer();
    }

    public class BeerService : IBeerService
    {
        private readonly BreweryContext context;
        private readonly IMapper mapper;
        private readonly IUserContextService userContext;
        public BeerService(BreweryContext context, IMapper mapper, IUserContextService userContext)
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

        public void DeleteBeer()
        {
            throw new NotImplementedException();
        }

        public void UpdateBeer()
        {
            throw new NotImplementedException();
        }
    }
}

using AutoMapper;
using BreweryAPI.Entities;
using BreweryAPI.Exceptions;
using BreweryAPI.Models.Beers;

namespace BreweryAPI.Services
{
    public interface IBeerService
    {
        void CreateBeer(CreatedBeerDto dto);
        void UpdateBeer(BeerUpdateDto dto, int id);
        void DeleteBeer(int id);
        Beer GetById(int id);
    }

    public class BeerService : IBeerService
    {
        private readonly dbContext context;
        private readonly IMapper mapper;
        private readonly IUserContextService userContext;

        public BeerService(dbContext context, IMapper mapper, IUserContextService userContext)
        {
            this.context = context;
            this.mapper = mapper;
            this.userContext = userContext;
        }

        public void CreateBeer(CreatedBeerDto dto)
        {
            var beer = mapper.Map<Beer>(dto);

            beer.BreweryId = userContext.GetUserId;

            if (context.Stocks.Any())
            {
                var beerInStock = context.Stocks.Where(x => x.CompanyAccountId == userContext.GetUserId).FirstOrDefault(x => x.BeerInStock.Name == beer.Name);

                if (beerInStock == null)
                {
                    var newStock = new Stock()
                    {
                        BeerInStock = beer,
                        Quantity = 1,
                        CompanyAccountId = userContext.GetUserId,
                        BeerId = beer.Id
                    };
                    context.Stocks.Add(newStock);
                    context.Beers.Add(beer);
                }
                else
                {
                    beerInStock.Quantity++;
                }
            }
            else
            {
                var newStock = new Stock()
                {
                    BeerInStock = beer,
                    Quantity = 1,
                    CompanyAccountId = userContext.GetUserId,
                    BeerId = beer.Id
                };
                context.Stocks.Add(newStock);
                context.Beers.Add(beer);
            }

            context.SaveChanges();
        }

        public void DeleteBeer(int id)
        {
            var beer = GetById(id);

            context.Beers.Remove(beer);
            context.SaveChanges();
        }

        public Beer GetById(int id)
        {
            var beer = context.Beers.FirstOrDefault(x => x.Id == id);

            if (beer == null)
            {
                throw new NotFoundException("Beer not found");
            }

            return beer;
        }

        public void UpdateBeer(BeerUpdateDto dto, int id)
        {
            var beer = GetById(id);
            beer = mapper.Map<Beer>(dto);

            context.SaveChanges();
        }
    }
}

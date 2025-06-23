using AutoMapper;
using BreweryAPI.Entities;

namespace BreweryAPI.Services
{
    public interface IWholesalerService
    {
        void AddBeer(Beer beer);
        void RemoveBeer(int beerId);
        string SendQuote(List<ShopBasket> clientShopBaskets);
    }

    public class WholesalerService : IWholesalerService
    {
        private readonly dbContext context;
        private readonly IUserContextService userContext;
        private readonly IMapper mapper;

        public WholesalerService(dbContext context, IUserContextService userContext, IMapper mapper)
        {
            this.context = context;
            this.userContext = userContext;
            this.mapper = mapper;
        }

        public void AddBeer(Beer beer) 
        {
            var wholesaler = context.Wholesalers.FirstOrDefault(x => x.Id == userContext.GetUserId);

            if (context.Stocks.Any()) 
            {
                var beerStock = context.Stocks.Where(x => x.CompanyAccountId == beer.BreweryId).FirstOrDefault(x => x.BeerInStock.Name == beer.Name);

                if (beerStock != null)
                {
                    var wholeSalerStock = context.Stocks.Where(x => x.CompanyAccountId == userContext.GetUserId).FirstOrDefault(x => x.BeerInStock.Name == beer.Name);
                    beerStock.Quantity--;

                    if (wholeSalerStock != null)
                    {
                        wholeSalerStock.Quantity++;
                    }
                    else
                    {
                        var newBeer = mapper.Map<Beer>(beer);

                        var newStock = new Stock()
                        {
                            BeerInStock = newBeer,
                            Quantity = 1,
                            CompanyAccountId = userContext.GetUserId,
                            BeerId = newBeer.Id
                        };
                        context.Stocks.Add(newStock);
                    }
                }
                else
                {
                    //Error
                }
            }

            context.SaveChanges();
        }

        public void RemoveBeer(int beerId)
        {
            var wholesaler = context.Wholesalers.FirstOrDefault(x => x.Id == userContext.GetUserId);
            var beer = context.Beers.FirstOrDefault(x => x.Id == beerId);

            if (wholesaler.Stocks.Any())
            {
                var beerStock = wholesaler.Stocks.First(x => x.BeerInStock.Name == beer.Name);

                if (beerStock.Quantity > 0)
                {
                    beerStock.Quantity--;
                }
                else
                {
                    context.Stocks.Remove(beerStock);
                }
            }

            context.SaveChanges();
        }

        public string SendQuote(List<ShopBasket> clientShopBaskets)
        {
            int finalQuantity = 0;
            decimal finalPrice = 0;
            decimal discount = 1;

            foreach (var basket in clientShopBaskets)
            {
                var beerPrice = context.Beers.First(x => x.Id == basket.BeerId).Price;
                finalQuantity += basket.Quantity;
                finalPrice += beerPrice * basket.Quantity;
            }

            if(finalQuantity > 10)
            {
                discount = 0.1m;
            }else if(finalQuantity > 20)
            {
                discount = 0.2m;
            }

            finalPrice *= discount;

            return $"Your quote: {finalPrice}$";
        }
    }
}

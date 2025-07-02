using AutoMapper;
using BreweryAPI.Entities;
using BreweryAPI.Exceptions;

namespace BreweryAPI.Services
{
    public interface IWholesalerService
    {
        void AddWholesalerBeer(Beer beer);
        void RemoveStock(int stockId);
        string SendQuote(List<ShopBasket> clientShopBaskets);

    }

    public class WholesalerService : IWholesalerService
    {
        private readonly DBaseContext context;
        private readonly IUserContextService userContext;
        private readonly IMapper mapper;

        public WholesalerService(DBaseContext context, IUserContextService userContext, IMapper mapper)
        {
            this.context = context;
            this.userContext = userContext;
            this.mapper = mapper;
        }
        public void AddWholesalerBeer(Beer beer)
        {
            var wholesaler = GetWholesaler;

            if (context.Stocks.Any())
            {
                var beerStock = context.Stocks.Where(x => x.CompanyAccountId == beer.BreweryId).FirstOrDefault(x => x.BeerInStock.Name == beer.Name);

                if(beerStock.Quantity == 0)
                {
                    throw new NotEnoughException("Not enough beer in stock");
                }

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
                    throw new NotFoundException("There's no beer in brewery stock");
                }
            }

            context.SaveChanges();
        }
        public void RemoveStock(int stockId)
        {
            var wholesaler = GetWholesaler();

            if (wholesaler.Stocks.Any())
            {
                var beerStock = wholesaler.Stocks.First(x => x.Id == stockId);

                context.Stocks.Remove(beerStock);
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
                var beerPrice = context.Beers.First(x => x.Id == basket.BeerInBasketId).Price;
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
        private Wholesaler GetWholesaler()
        {
            var wholesaler = context.UserAccounts.FirstOrDefault(x => x.Id == userContext.GetUserId) as Wholesaler;
            
            if(wholesaler == null)
            {
                throw new NotFoundException("Wholesaler not found");
            }

            return wholesaler;
        }
    }
}

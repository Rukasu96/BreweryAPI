using BreweryAPI.Entities;

namespace BreweryAPI.Services
{
    public interface IWholesalerService
    {
        void AddBeer(Beer beer);
        void RemoveBeer(Beer beer);
        string SendQuote(List<Beer> clientBeers);
    }

    public class WholesalerService : IWholesalerService
    {
        private readonly dbContext context;
        private readonly IUserContextService userContext;

        public WholesalerService(dbContext context, IUserContextService userContext)
        {
            this.context = context;
            this.userContext = userContext;
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
                    
                    if(wholeSalerStock != null)
                    {
                        wholeSalerStock.Quantity++;
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
                    }
                }
                else
                {
                    //Error
                }
            }

            context.SaveChanges();
        }

        public void RemoveBeer(Beer beer)
        {
            var wholesaler = context.Wholesalers.FirstOrDefault(x => x.Id == userContext.GetUserId);
            context.SaveChanges();
        }

        public string SendQuote(List<Beer> clientBeers)
        {
            decimal finalPrice = 0;
            decimal discount = 1;
            if(clientBeers.Count > 10)
            {
                discount = 0.1m;
            }else if(clientBeers.Count > 20)
            {
                discount = 0.2m;
            }

            foreach (var beer in clientBeers)
            {
                finalPrice += beer.Price;
            }

            finalPrice *= discount;

            return $"Your quote: {finalPrice}$";
        }
    }
}

using AutoMapper;
using BreweryAPI.Entities;

namespace BreweryAPI.Services
{
    public interface IClientService
    {
        void AddBeerToBasket(Beer beer);
        List<ShopBasket> GetBaskets(Guid wholesalerId);
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
        
        public void AddBeerToBasket(Beer beer)
        {
            var client = context.Clients.FirstOrDefault(x => x.Id == userContext.GetUserId);

            if (context.ShopBaskets.Any())
            {
                var shopBasketStock = context.ShopBaskets.Where(x => (x.ClientId == client.Id)).FirstOrDefault(x => x.BeerInBasket.StockId == beer.StockId);

                if (shopBasketStock != null)
                {
                    shopBasketStock.Quantity++;
                }
                else
                {
                    var newBeer = mapper.Map<Beer>(beer);

                    var newShopBasket = new ShopBasket()
                    {
                        BeerInBasket = newBeer,
                        Quantity = 1,
                        ClientId = client.Id,
                        BeerId = newBeer.Id
                    };
                    context.ShopBaskets.Add(newShopBasket);
                };
            }
            else
            {
                var newBeer = mapper.Map<Beer>(beer);

                var newShopBasket = new ShopBasket()
                {
                    BeerInBasket = newBeer,
                    Quantity = 1,
                    ClientId = client.Id,
                    BeerId = newBeer.Id
                };
                context.ShopBaskets.Add(newShopBasket);
            }

                context.SaveChanges();
        }
        
        public List<ShopBasket> GetBaskets(Guid wholesalerId)
        {
            var client = context.Clients.FirstOrDefault(x => x.Id == userContext.GetUserId);
            var wholesalerStocks = context.Stocks.Where(x => x.CompanyAccountId == wholesalerId).ToList();
            var baskets = context.ShopBaskets.Where(x => x.ClientId == client.Id).ToList();

            List<ShopBasket> wholesalerBaskets = new List<ShopBasket>();

            foreach (var basket in baskets)
            {
                foreach (var stock in wholesalerStocks)
                {
                    var beer = context.Beers.FirstOrDefault(x => basket.BeerId == x.Id);
                    var stockBeer = context.Beers.FirstOrDefault(x => x.Id == stock.BeerId);
                    if(stockBeer.Name == beer.Name)
                    {
                        wholesalerBaskets.Add(basket);
                        continue;
                    }
                }
            }

            return wholesalerBaskets;
        }
    }
}

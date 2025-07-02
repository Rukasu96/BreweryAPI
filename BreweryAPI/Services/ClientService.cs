using AutoMapper;
using BreweryAPI.Entities;
using BreweryAPI.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace BreweryAPI.Services
{
    public interface IClientService
    {
        void AddBeerToBasket(Beer beer, Guid wholesalerId);
        List<ShopBasket> GetBaskets(Guid wholesalerId);
    }

    public class ClientService : IClientService
    {
        private readonly DBaseContext context;
        private readonly IMapper mapper;
        private readonly IUserContextService userContext;
        public ClientService(DBaseContext context, IMapper mapper, IUserContextService userContext)
        {
            this.context = context;
            this.mapper = mapper;
            this.userContext = userContext;
        }
        
        public void AddBeerToBasket(Beer beer, Guid wholesalerId)
        {
            var client = GetClient();

            if (client == null) 
            {
                throw new NotFoundException("user not found");
            }

            if (context.ShopBaskets.Any())
            {
                var wholesalerstock = context.Stocks.Where(x => x.CompanyAccountId == wholesalerId).FirstOrDefault(x => x.BeerInStock.Id == beer.Id);
                var shopBasketStock = context.ShopBaskets.Where(x => (x.ClientId == client.Id)).FirstOrDefault(x => x.BeerInBasket.Id == wholesalerstock.BeerId);

                if(wholesalerstock.Quantity == 0)
                {
                    throw new NotEnoughException("Not enouth beer in stock");
                }

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
                        BeerInBasketId = newBeer.Id
                    };
                    context.ShopBaskets.Add(newShopBasket);
                };

                wholesalerstock.Quantity--;
            }
            else
            {
                var newBeer = mapper.Map<Beer>(beer);

                var newShopBasket = new ShopBasket()
                {
                    BeerInBasket = newBeer,
                    Quantity = 1,
                    ClientId = client.Id,
                    BeerInBasketId = newBeer.Id
                };
                context.ShopBaskets.Add(newShopBasket);
            }

            context.SaveChanges();
        }

        public List<ShopBasket> GetBaskets(Guid wholesalerId)
        {
            var client = GetClient();
            var wholesalerStocks = context.Stocks.Where(x => x.CompanyAccountId == wholesalerId).ToList();
            var baskets = context.ShopBaskets.Where(x => x.ClientId == client.Id).ToList();

            List<ShopBasket> wholesalerBaskets = new List<ShopBasket>();

            foreach (var basket in baskets)
            {
                foreach (var stock in wholesalerStocks)
                {
                    var beer = context.Beers.FirstOrDefault(x => basket.BeerInBasketId == x.Id);
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

        private Client GetClient()
        {
            var client = context.Clients.FirstOrDefault(x => x.Id == userContext.GetUserId);

            if(client == null)
            {
                throw new NotFoundException("User not found");
            }

            return client;
        }
    }
}

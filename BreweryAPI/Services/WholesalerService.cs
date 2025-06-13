using BreweryAPI.Entities;

namespace BreweryAPI.Services
{
    public interface IWholesalerService
    {
        void AddBeer(Beer beer);
        void RemoveBeer(Beer beer);
    }

    public class WholesalerService : IWholesalerService
    {
        private readonly BreweryContext context;
        private readonly IUserContextService userContext;

        public WholesalerService(BreweryContext context, IUserContextService userContext)
        {
            this.context = context;
            this.userContext = userContext;
        }

        public void AddBeer(Beer beer) 
        {
            var wholesaler = context.Wholesalers.FirstOrDefault(x => x.Id == userContext.GetUserId);
            wholesaler.Beers.Add(beer);
        }

        public void RemoveBeer(Beer beer)
        {
            var wholesaler = context.Wholesalers.FirstOrDefault(x => x.Id == userContext.GetUserId);
            wholesaler.Beers.Remove(beer);
        }
    }
}

using BreweryAPI.Entities;

namespace BreweryAPI.Services
{
    public class BeerServices
    {
        private readonly BreweryContext context;
        public BeerServices(BreweryContext context)
        {
            this.context = context;
        }

    }
}

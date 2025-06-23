using BreweryAPI.Entities.Configurations;

namespace BreweryAPI.Entities
{
    public class CompanyAccount : UserAccount
    {
        public List<Stock> Stocks { get; set; }
    }
}

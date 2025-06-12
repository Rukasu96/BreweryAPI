using BreweryAPI.Services;

namespace BreweryAPI.Requests
{
    public class WholesalerRequests
    {
        public static void RegisterEndpoints(WebApplication app)
        {
            app.MapPost("wholesaler/addBeer/{id}", WholesalerRequests.AddBeer);
        }

        public static IResult AddBeer(IBeerService beerService, IWholesalerService wholesalerService, int id)
        {
            var beer = beerService.GetById(id);
            wholesalerService.AddBeer(beer);

            return Results.Ok(beer);
        }
    }
}

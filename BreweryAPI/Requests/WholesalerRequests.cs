using BreweryAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BreweryAPI.Requests
{
    public class WholesalerRequests
    {
        public static void RegisterEndpoints(WebApplication app)
        {
            app.MapPost("wholesaler/addBeer/{beerId}", WholesalerRequests.AddBeer);
            app.MapDelete("wholesaler/removeBeer/{beerId}", WholesalerRequests.RemoveBeer);
        }

        public static IResult AddBeer(IBeerService beerService, IWholesalerService wholesalerService, int beerId)
        {
            var beer = beerService.GetById(beerId);
            wholesalerService.AddBeer(beer);

            return Results.Ok(beer);
        }

        public static IResult RemoveBeer(IWholesalerService beerService, [FromRoute]int beerId)
        {
            beerService.RemoveBeer(beerId);
            return Results.NoContent();
        }
    }
}

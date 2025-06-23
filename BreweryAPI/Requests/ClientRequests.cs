using BreweryAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BreweryAPI.Requests
{
    public class ClientRequests
    {
        public static void RegisterEndpoints(WebApplication app)
        {
            app.MapPost("/wholesalers/wholesaler/{beerId}/addBeer", ClientRequests.AddBeer);
            app.MapGet("/wholesalers/{wholesalerId}/quote", ClientRequests.GetQuote);
        }

        public static IResult AddBeer(IBeerService beerService, IClientService clientService, int beerId)
        {
            var beer = beerService.GetById(beerId);
            clientService.AddBeerToBasket(beer);
            return Results.Ok();
        }

        public static IResult GetQuote(IClientService clientService,IWholesalerService wholesalerService, [FromRoute] Guid wholesalerId)
        {
            var clientBaskets = clientService.GetBaskets(wholesalerId);
            var quote = wholesalerService.SendQuote(clientBaskets);
            return Results.Ok(quote);
        }
    }
}

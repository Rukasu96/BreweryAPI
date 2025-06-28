using BreweryAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BreweryAPI.Requests
{
    public class ClientRequests
    {
        public static void RegisterEndpoints(WebApplication app)
        {
            app.MapPost("/client/wholesaler/{beerId}/addBeer", ClientRequests.AddBeer);
            app.MapGet("/client/{wholesalerId}/quote", ClientRequests.GetQuote);
        }

        [Authorize(Roles = "Client")]
        public static IResult AddBeer(IBeerService beerService, IClientService clientService, [FromRoute] int beerId)
        {
            var beer = beerService.GetById(beerId);
            clientService.AddBeerToBasket(beer);
            return Results.Ok();
        }

        [Authorize(Roles = "Client")]
        public static IResult GetQuote(IClientService clientService,IWholesalerService wholesalerService, [FromRoute] Guid wholesalerId)
        {
            var clientBaskets = clientService.GetBaskets(wholesalerId);
            var quote = wholesalerService.SendQuote(clientBaskets);
            return Results.Ok(quote);
        }
    }
}

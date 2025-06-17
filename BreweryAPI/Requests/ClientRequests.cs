using BreweryAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BreweryAPI.Requests
{
    public class ClientRequests
    {
        public static void RegisterEndpoints(WebApplication app)
        {
            app.MapGet("/wholesalers/{wholesalerId}", ClientRequests.GetAllWholesalersBeers);
            app.MapGet("/wholesalers/{wholesalerId}/{beerId}", ClientRequests.GetWholesalerBeer);
            app.MapGet("/wholesalers/{wholesalerId}/{beerId}/addBeer", ClientRequests.AddBeer);
            app.MapGet("/wholesalers/{wholesalerId}/quote", ClientRequests.GetQuote);
        }

        public static IResult GetAllWholesalersBeers(IClientService clientService, [FromRoute]Guid wholesalerId)
        {
            var wholesalersBeers = clientService.GetAllWholesalersBeers(wholesalerId);
            return Results.Ok(wholesalersBeers);
        }

        public static IResult GetWholesalerBeer(IClientService clientService, [FromRoute] Guid wholesalerId, [FromRoute] int beerId)
        {
            var beer = clientService.GetWholesalerBeerById(wholesalerId, beerId);
            return Results.Ok(beer);
        }

        public static IResult AddBeer(IClientService clientService, [FromRoute] Guid wholesalerId, [FromRoute] int beerId)
        {
            clientService.AddBeer(wholesalerId, beerId);
            return Results.Ok();
        }

        public static IResult GetQuote(IClientService clientService,IWholesalerService wholesalerService, [FromRoute] Guid wholesalerId)
        {
            var clientBeers = clientService.GetClientsWholesalerBeers(wholesalerId);
            var quote = wholesalerService.SendQuote(clientBeers);
            clientService.DeleteAllBeers();
            return Results.Ok(quote);
        }
    }
}

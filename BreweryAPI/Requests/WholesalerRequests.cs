using AutoMapper;
using BreweryAPI.Entities;
using BreweryAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BreweryAPI.Requests
{
    public class WholesalerRequests
    {
        public static void RegisterEndpoints(WebApplication app)
        {
            app.MapPost("wholesaler/addBeer/{beerId}", WholesalerRequests.AddWholesalerBeer);
            app.MapDelete("wholesaler/removeStock/{stockId}", WholesalerRequests.RemoveBeerFromWholesalerStock);
        }

        [Authorize(Roles = "Wholesaler")]
        public static IResult AddWholesalerBeer(IWholesalerService wholesalerService, IBeerService beerService, [FromRoute]int beerId)
        {
            var beer = beerService.GetById(beerId);
            wholesalerService.AddWholesalerBeer(beer);
            return Results.Ok();
        }

        [Authorize(Roles = "Wholesaler")]
        public static IResult RemoveBeerFromWholesalerStock(IWholesalerService wholesalerService, int stockId)
        {
            wholesalerService.RemoveStock(stockId);
            return Results.NoContent();
        }

    }
}

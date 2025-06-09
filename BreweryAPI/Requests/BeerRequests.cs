using BreweryAPI.Models;
using BreweryAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BreweryAPI.Requests
{
    public class BeerRequests
    {
        public static void RegisterEndpoints(WebApplication app)
        {
            app.MapPost("/beers/create", BeerRequests.CreateBeer);
        }

        [Authorize(Roles = "Brewery")]
        public static IResult CreateBeer(IBeerService beerService, [FromBody]CreatedBeerDto dto)
        {
            beerService.CreateBeer(dto);
            return Results.Created();
        }
    }
}

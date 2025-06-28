using BreweryAPI.Models.Beers;
using BreweryAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BreweryAPI.Requests
{
    public class BeerRequests
    {
        public static void RegisterEndpoints(WebApplication app)
        {
            app.MapPost("/brewery/createBear", BeerRequests.CreateBeer);
            app.MapPut("/brewery/beers/{beerId}/update", BeerRequests.UpdateBeer);
            app.MapDelete("brewery/beers/{beerId}/delete", BeerRequests.DeleteBeer);
        }

        [Authorize(Roles = "Brewery")]
        public static IResult CreateBeer(IBeerService beerService, [FromBody] CreatedBeerDto dto)
        {
            beerService.CreateBeer(dto);
            return Results.Created();
        }

        [Authorize(Roles = "Brewery")]
        public static IResult DeleteBeer(IBeerService beerService, [FromRoute] int beerId)
        {
            beerService.DeleteBeer(beerId);
            return Results.NoContent();
        }

        [Authorize(Roles = "Brewery")]
        public static IResult UpdateBeer(IBeerService beerService, [FromBody] BeerUpdateDto dto, [FromRoute] int beerId)
        {
            beerService.UpdateBeer(dto, beerId);
            return Results.Ok(beerId);
        }
    }
}

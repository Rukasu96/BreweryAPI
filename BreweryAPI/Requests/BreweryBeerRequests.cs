using BreweryAPI.Models.Beers;
using BreweryAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BreweryAPI.Requests
{
    public class BreweryBeerRequests
    {
        public static void RegisterEndpoints(WebApplication app)
        {
            app.MapPost("/brewery/beers/create", BreweryBeerRequests.CreateBeer);
            app.MapPut("/brewery/beers/{id}/update", BreweryBeerRequests.UpdateBeer);
            app.MapDelete("brewery/beers/{id}/delete", BreweryBeerRequests.DeleteBeer);
        }

        [Authorize(Roles = "Brewery")]
        public static IResult CreateBeer(IBeerService beerService, [FromBody]CreatedBeerDto dto)
        {
            beerService.CreateBeer(dto);
            return Results.Created();
        }

        [Authorize(Roles = "Brewery")]
        public static IResult DeleteBeer(IBeerService beerService, [FromRoute] int id) 
        {
            beerService.DeleteBeer(id);
            return Results.NoContent();
        }

        [Authorize(Roles = "Brewery")]
        public static IResult UpdateBeer(IBeerService beerService, [FromBody] BeerUpdateDto dto, [FromRoute] int id)
        {
            beerService.UpdateBeer(dto, id);
            return Results.Ok(id);
        }
    }
}

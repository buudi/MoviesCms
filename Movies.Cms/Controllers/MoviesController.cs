
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Web.Common.Controllers;
using Movies.Cms.Services;
using Movies.Cms.Controllers.Models;
using Asp.Versioning;

namespace Movies.Cms.Controllers;

[ApiVersion(1)]
[ApiExplorerSettings(GroupName = "Movies V1")]
[Route("api/v1/movies")]
[ApiController]
public class MoviesController(MovieService movieService):UmbracoApiController
{
    [HttpGet]
    public IActionResult GetMovies([FromQuery]string? culture)
    {
        Console.WriteLine($"Culture {culture}");

        var movies = (culture != null) ? movieService.AllMovies(culture) : movieService.AllMovies(); 
        return Ok(movies);
    }

    [HttpGet("id")]
    public IActionResult GetId(Guid Id, [FromQuery]string? culture)
    {
        culture = culture ?? "en-US";
        var movie = movieService.getMovieById(Id, culture);


        return Ok(movie);
    }

    [HttpPost]
    public IActionResult CreateMovie(CreateMovieRequest requestModel, [FromQuery]string? culture)
    {
        culture = culture ?? "en-US";
	    movieService.CreateMovie(culture, requestModel.Name, requestModel.Synopsis, requestModel.ReleaseYear, requestModel.Director, requestModel.Poster);
        return Ok(movieService.AllMovies());
    }
        
    [HttpPut]
    public IActionResult UpdateMovie(UpdateMovieRequest requestModel)
    {
        requestModel.culture ??= "en-US";
        // can also do this: culture ??= "en-US";
        movieService.UpdateMovie(requestModel.culture, requestModel.Id, requestModel.Name, requestModel.Synopsis, requestModel.ReleaseYear, requestModel.Director, requestModel.Poster);
        return Ok(movieService.AllMovies());
    }

    [HttpDelete]
    public IActionResult DeleteMovie(Guid id)
    {
        movieService.DeleteMovie(id);
        return Ok("Deleted");
    }
}
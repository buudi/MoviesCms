
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Web.Common.Controllers;
using Movies.Cms.Services;
using Movies.Cms.Controllers.Models;
using Smidge.Models;
using System.Reflection;

namespace Movies.Cms.Controllers;

[Route("api/v1/en")]
[ApiController]
public class MyController(MovieService movieService):UmbracoApiController
{
    [HttpGet("movies")]
    public IActionResult GetMovies()
    {
        var movies = movieService.AllMovies;
        return Ok(movies);
    }

    [HttpGet("id")]
    public IActionResult GetId(Guid Id)
    {
        var movie = movieService.getMovieById(Id);

        return Ok(movie);
    }

    [HttpPost("movies")]
    public IActionResult CreateMovie(CreateMovieRequest requestModel)
    {
        Console.WriteLine("CreateMovie at controller runs okay");

	    movieService.CreateMovie("en-US", requestModel.Name, requestModel.Synopsis, requestModel.ReleaseYear, requestModel.Director, requestModel.Poster);
        return Ok(movieService.AllMovies);
    }
        
    [HttpPut("movies")]
    public IActionResult UpdateMovie(Guid id, UpdateMovieRequest requestModel)
    {
        movieService.UpdateMovie("en-US", id, requestModel.Name, requestModel.Synopsis, requestModel.ReleaseYear, requestModel.Director);
        return Ok(movieService.AllMovies);
    }

    [HttpDelete("movies")]
    public IActionResult DeleteMovie(Guid id)
    {
        movieService.DeleteMovie(id);
        return Ok("Deleted");
    }

    [HttpPost("movies/image")]
	public IActionResult UploadImage([FromForm]UploadPosterRequest file, Guid movieId)
	{
        IFormFile? posterFile = file.PosterFile;
        movieService.AddPosterToTestMovie(posterFile, movieId);
		return Ok();
	}


    [HttpPost("movies/anotherImage/{movieId}")]
    public IActionResult UploadAnotherImage([FromForm] UploadPosterRequest request, Guid movieId)
    {
        // this method is just testing the swagger/postman upload
        return Ok();
    }

    //[HttpPost("movies/anotherImage/{movieId}")]
    //public IActionResult UploadAnotherImage([FromForm] IFormFile file, Guid movieId)
    //{
    //    //movieService.AddPosterToTestMovie(files, movieId);
    //    return Ok();
    //}


    //[HttpPost("movies/image")]
    //public async Task<IActionResult> UploadImageAsync([FromForm] List<IFormFile> files, Guid movieId)
    //{
    //    long size = files.Sum(f => f.Length);

    //    // sample test movie:
    //    // 5f16354b-4f8c-45e9-8e20-d53b2c413c7d

    //    foreach (var formFile in files)
    //    {
    //        if (formFile.Length > 0)
    //        {
    //            var filePath = Path.GetTempFileName();

    //            Console.WriteLine($"to add as poster to movie with id: {movieId}");
    //            Console.WriteLine($"temp file path: {filePath}");


    //            //Console.WriteLine($"executing assemblies path: {currentPath}");

    //            using var stream = System.IO.File.Create(filePath);
    //            await formFile.CopyToAsync(stream);
    //        }
    //    }

    //    return Ok(new { count = files.Count, size });
    //}


}
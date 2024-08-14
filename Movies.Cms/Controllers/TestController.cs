using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Movies.Cms.Controllers.Models;
using Movies.Cms.Services;
using Umbraco.Cms.Web.Common.Controllers;

namespace Movies.Cms.Controllers;

[ApiExplorerSettings(GroupName = "Testing")]
[Route("test")]
[ApiController]
public class TestController(MovieService movieService) : ControllerBase
{
	[HttpPost("image")]
	public IActionResult UploadImage([FromForm] UploadPosterRequest file, Guid movieId)
	{
		IFormFile? posterFile = file.PosterFile;
		movieService.AddPosterToTestMovie(posterFile, movieId);
		return Ok();
	}

	[HttpPost("anotherImage/{movieId}")]
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

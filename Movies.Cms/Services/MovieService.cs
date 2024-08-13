using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Movies.Cms.Models;
using System;
using Umbraco;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.IO;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.PropertyEditors;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Strings;
using Umbraco.Cms.Web.Common;
using static Umbraco.Cms.Core.Constants.Conventions;

namespace Movies.Cms.Services;

public class MovieService(UmbracoHelper umbracoHelper, IContentService contentService, IMediaService mediaService, MediaFileManager _mediaFileManager, IShortStringHelper _shortStringHelper, IContentTypeBaseServiceProvider _contentTypeBaseServiceProvider, MediaUrlGeneratorCollection _mediaUrlGeneratorCollection)
{


    public static class ContentKeys
    {
        public const string MoviesListing = "16e1b89f-5a4e-4165-91bd-5689e387d787";
    }

	private void AddPosterToMovie([FromForm]IFormFile? file, Guid movieId)
	{
		// to get the media and add it to the media archive later on
		Guid mediaGuid;

		// Open a new stream to the file
		//using (Stream stream = System.IO.File.OpenRead(path))
		using (Stream stream = file!.OpenReadStream())
		{
			// Initialize a new image at the root of the media archive
			IMedia media = mediaService.CreateMedia("Unicorn", Constants.System.Root, Constants.Conventions.MediaTypes.Image);
			// Set the property value (Umbraco will handle the underlying magic)
			media.SetValue(_mediaFileManager, _mediaUrlGeneratorCollection, _shortStringHelper, _contentTypeBaseServiceProvider, Constants.Conventions.Media.File, "unicorn.jpg", stream);

			mediaGuid = media.Key;

			// Save to the media archive
			var result = mediaService.Save(media);
		}

		// get the page/content of movie item from movieId
		IContent? content = contentService.GetById(movieId);

		// now get media to assign to media picker by udi (MediaWithCrops) 
		var mediaToAssign = umbracoHelper.Media(mediaGuid);

		// create the udi
		var udi = Umbraco.Cms.Core.Udi.Create(Constants.UdiEntityType.Media, mediaToAssign!.Key);

		// set the value to the media picker
		content!.SetValue("poster", udi.ToString());

		// save and publish
		contentService.SaveAndPublish(content);
	}

	public List<Movie> AllMovies => umbracoHelper.Content(ContentKeys.MoviesListing)?
        .Children
        .Select(x => new Movie(x))
        .ToList() ?? [];

    public Movie getMovieById(Guid id)
    {
        IPublishedContent? movieContent = umbracoHelper.Content(id);
		Movie movie = new(movieContent!);
        return movie;
    }

    public void CreateMovie(string culture, string? name, string? synopsis, DateTime? releaseYear, Guid directorId, [FromForm]IFormFile? posterFile)
    {
        var content = contentService.Create(name!, Guid.Parse(ContentKeys.MoviesListing),"movie");
        
        // in a culture less setting:
        //content.Name = name;

        content.SetCultureName(name, culture);
        content.SetValue("synopsis", synopsis, culture);
        content.SetValue("releaseYear", releaseYear);

        var directorContent = umbracoHelper.Content(directorId);
        var directorUdi = Umbraco.Cms.Core.Udi.Create(Constants.UdiEntityType.Document, directorContent!.Key);

        content.SetValue("director", directorUdi.ToString());
        contentService.SaveAndPublish(content);

		Guid newMovieId = content.Key;
		AddPosterToMovie(posterFile, newMovieId);
    }

    public void UpdateMovie(string culture, Guid id, string? name, string? synopsis, DateTime? releaseYear, Guid directorId)
    {
        IContent? movieContent = contentService.GetById(id);
        
        movieContent!.SetCultureName(name, culture);

		if (!string.IsNullOrEmpty(synopsis))
			movieContent.SetValue("synopsis", synopsis, culture);

		if (!releaseYear.HasValue)
			movieContent.SetValue("releaseYear", releaseYear);

       
		var directorContent = umbracoHelper.Content(directorId);
		var directorUdi = Umbraco.Cms.Core.Udi.Create(Constants.UdiEntityType.Document, directorContent!.Key);

		movieContent.SetValue("director", directorUdi);
        
        contentService.SaveAndPublish(movieContent);
	}

    public void DeleteMovie(Guid Id)
    {
        IContent? movieContent = contentService.GetById(Id);
        contentService.Delete(movieContent!);
    }

	

	public void AddPosterToTestMovie(IFormFile? file, Guid movieId)
    {
        //string webRootPath = _webHostEnvironment.WebRootPath;
        //var path = Path.Combine(webRootPath, "images");
        //Console.WriteLine($"Web root Path: {webRootPath}");

        //foreach (var formFile in files)
        //{
        //    if (formFile.Length > 0)
        //    {
        //        Console.WriteLine($"to add as poster to movie with id: {movieId}");
        //        Console.WriteLine($"temp file path: {path}");

        //        using var stream = System.IO.File.Create(path);
        //        await formFile.CopyToAsync(stream);
        //    }
        //}

        //get the media and add it to the media archive

        Guid mediaGuid;

		// Open a new stream to the file
		//using (Stream stream = System.IO.File.OpenRead(path))
		using (Stream stream = file!.OpenReadStream())
		{
			// Initialize a new image at the root of the media archive
			IMedia media = mediaService.CreateMedia("Unicorn", Constants.System.Root, Constants.Conventions.MediaTypes.Image);
			// Set the property value (Umbraco will handle the underlying magic)
			media.SetValue(_mediaFileManager, _mediaUrlGeneratorCollection, _shortStringHelper, _contentTypeBaseServiceProvider, Constants.Conventions.Media.File, "unicorn.jpg", stream);

            mediaGuid = media.Key;

            // Save to the media archive
			var result = mediaService.Save(media);
		}
        
		// get the page/content of movie item from movieId
		IContent? content = contentService.GetById(movieId);

        // now get media to assign to media picker by udi (MediaWithCrops) 
        var mediaToAssign = umbracoHelper.Media(mediaGuid);

		// create the udi
		var udi = Umbraco.Cms.Core.Udi.Create(Constants.UdiEntityType.Media, mediaToAssign!.Key);

		// set the value to the media picker
		content!.SetValue("poster", udi.ToString());

		// save and publish
		contentService.SaveAndPublish(content);
	}

}
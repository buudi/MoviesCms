using Microsoft.IdentityModel.Tokens;
using Movies.Cms.Models;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Web.Common;
using static Umbraco.Cms.Core.Constants.Conventions;

namespace Movies.Cms.Services;

public class MovieService(UmbracoHelper umbracoHelper, IContentService contentService)
{
    public static class ContentKeys
    {
        public const string MoviesListing = "16e1b89f-5a4e-4165-91bd-5689e387d787";
    }
    public List<Movie> AllMovies => umbracoHelper.Content(ContentKeys.MoviesListing)?
        .Children
        .Select(x => new Movie(x))
        .ToList() ?? [];

    public void CreateMovie(string culture, string? name, string? synopsis, DateTime? releaseYear, Guid director)
    {
        var _director = new Director(director);
        var content = contentService.Create(name!, Guid.Parse(ContentKeys.MoviesListing),"movie");
        content.SetCultureName(name, culture);
        content.SetValue("synopsis", synopsis, culture);
        content.SetValue("releaseYear", releaseYear);
        content.SetValue("director", _director);
		
        contentService.SaveAndPublish(content);
    }


    public void UpdateMovie(string culture, Guid id, string? name, string? synopsis, DateTime? releaseYear, Director? director)
    {
        IContent? movieContent = contentService.GetById(id);

		movieContent!.SetCultureName(name, culture);

        if (!string.IsNullOrEmpty(name))
            movieContent.SetValue("name", name, culture);

		if (!string.IsNullOrEmpty(synopsis))
			movieContent.SetValue("synopsis", synopsis, culture);

		if (!releaseYear.HasValue)
			movieContent.SetValue("releaseYear", releaseYear, culture);

        //if (!string.IsNullOrEmpty(director!.Name))
        movieContent.SetValue("director", director, culture);

        contentService.SaveAndPublish(movieContent);
	}

}
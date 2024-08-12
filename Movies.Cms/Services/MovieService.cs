using Movies.Cms.Models;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Web.Common;

namespace Movies.Cms.Services;

public class MovieService(UmbracoHelper umbracoHelper, IContentService contentService)
{
    public static class ContentKeys
    {
        public const string MoviesListing = "16e1b89f-5a4e-4165-91bd-5689e387d787";
    }
    public List<Movie> AllMovies => umbracoHelper.Content(ContentKeys.MoviesListing)?.Children.Select(x => new Movie(x)).ToList() ?? [];

    public void CreateMovie(string culture, string? name, string? synopsis, DateTime? releaseYear, Director? director)
    {
        var content = contentService.Create(name!, Guid.Parse(ContentKeys.MoviesListing),"movie");
        content.SetCultureName(name, culture);
        content.SetValue("synopsis", synopsis, culture);
        content.SetValue("releaseYear", releaseYear, culture);
        content.SetValue("director", director, culture);

		contentService.SaveAndPublish(content);
    }

}
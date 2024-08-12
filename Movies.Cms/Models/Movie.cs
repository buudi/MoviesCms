using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace Movies.Cms.Models;

public class Movie
{
    public string Title { get; set; }
    public string? Synopsis { get; set; } 
    public DateTime ReleaseYear { get; set; } 

    public Director? Director { get; set; }

    public MediaWithCrops? Poster { get; set; }



    public Movie(IPublishedContent content)
    {
        Title = content.Name;
        Synopsis = content.Value<string>("synopsis"); 
        ReleaseYear = content.Value<DateTime>("releaseYear");
        Director = content.Value<Director>("director");
        Poster = content.Value<MediaWithCrops>("poster");

        var directorContent = content.Value<IPublishedContent>("director");
        if(directorContent != null)
        {
            Director = new Director(directorContent);
        }
    }
}

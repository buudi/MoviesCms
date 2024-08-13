using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace Movies.Cms.Models;

public class Movie
{
	public Guid Id { get; set; }
	public string Title { get; set; }
    public string? Synopsis { get; set; } 
    public DateTime ReleaseYear { get; set; } 

    public Director? Director { get; set; }

    //public MediaWithCrops? Poster {  get; set; }

    // here the poster just gets the path to folder in wwwroot/media
    //  sounds to me like the most right thing to do
	public string? Poster { get; set; }

	public Movie(IPublishedContent content)
    {
        Id = content.Key;
        Title = content.Name;
        Synopsis = content.Value<string>("synopsis"); 
        ReleaseYear = content.Value<DateTime>("releaseYear");
        //Director = content.Value<Director>("director");
        Poster = content.Value<MediaWithCrops>("poster")!.MediaUrl();
            
        IPublishedContent directorContent = content.Value<IPublishedContent>("director")!;

        if(directorContent != null)
        {
            Console.WriteLine("Director at Movie.cs is not null!!");

            Console.WriteLine(directorContent.Key);
            Director = new Director(directorContent);
        }
        else
        {
            Console.WriteLine("Director content is null");
            Director = null;
        }
    }
}

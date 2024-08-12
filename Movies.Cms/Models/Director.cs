using Umbraco.Cms.Core.Models.PublishedContent;

namespace Movies.Cms.Models;

public class Director(IPublishedContent content)
{
    public string Name { get; set; } = content.Name;
}

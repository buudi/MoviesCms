using Umbraco.Cms.Core.Models.PublishedContent;

namespace Movies.Cms.Models;

public class Director(IPublishedContent content)
{
	public Guid Id { get; set; } = content.Key;
	public string? Name { get; set; } = content.Name;

}
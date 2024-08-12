using Umbraco.Cms.Core.Models.PublishedContent;

namespace Movies.Cms.Models;

public class Director(Guid Id)
{
	//public string? Name { get; set; } = content.Name;
	public Guid Id { get; set; } = Id;

	//public Director(IPublishedContent content)
	//{
	//	if (content == null)
	//		throw new ArgumentNullException(nameof(content), "Content cannot be null");
		
	//	Id = content.Key;
	//}
}
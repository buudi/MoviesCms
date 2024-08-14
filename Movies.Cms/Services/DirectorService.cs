using Movies.Cms.Models;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Web.Common;

namespace Movies.Cms.Services;

public class DirectorService(UmbracoHelper umbracoHelper, IContentService contentService, IVariationContextAccessor variationContextAccessor)
{
	public static class ContentKeys
	{
		public const string DirectorsListing = "15052e65-f609-46a5-9627-ed6062ebecc2";
	}

	public List<Director> AllDirectors(string culture)
	{
		variationContextAccessor.VariationContext = new VariationContext(culture);
		var directorsContent = umbracoHelper.Content(ContentKeys.DirectorsListing)?
			.Children
			.Select(x => new Director(x))
			.ToList() ?? [];

		return directorsContent;
	}

}

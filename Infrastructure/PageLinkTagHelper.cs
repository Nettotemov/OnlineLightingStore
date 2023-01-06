using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using LampStore.Models.ViewModels;

namespace LampStore.Infrastructure
{
	[HtmlTargetElement("ul", Attributes = "page-model")]
	public class PageLinkTagHelper : TagHelper
	{
		private IUrlHelperFactory urlHelperFactory;
		public PageLinkTagHelper(IUrlHelperFactory helperFactory) { urlHelperFactory = helperFactory; }

		[ViewContext]
		[HtmlAttributeNotBound]
		public ViewContext? ViewContext { get; set; }
		public PagingInfo? PageModel { get; set; }
		public string? PageAction { get; set; }

		[HtmlAttributeName(DictionaryAttributePrefix = "page-url-")]
		public Dictionary<string, object> PageUrlValues { get; set; } = new Dictionary<string, object>();

		public bool PageClassesEnabled { get; set; } = false;
		public string PageClass { get; set; } = String.Empty;
		public string PageClassNormal { get; set; } = String.Empty;
		public string PageClassSelected { get; set; } = String.Empty;
		public string PageClassLi { get; set; } = String.Empty;
		public string PageDisabledClass { get; set; } = String.Empty;

		public override void Process(TagHelperContext context, TagHelperOutput output)
		{
			if (ViewContext != null && PageModel != null)
			{
				IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);
				TagBuilder result = new TagBuilder("ul");

				TagBuilder buttonNext = new TagBuilder("button");
				TagBuilder buttonBefore = new TagBuilder("button");

				TagBuilder tagWrapper = new TagBuilder("li");



				tagWrapper.InnerHtml.AppendHtml(buttonBefore);
				buttonBefore.InnerHtml.Append("Назад");
				buttonBefore.AddCssClass(PageClass);
				tagWrapper.AddCssClass(PageClassLi);
				if (PageModel.CurrentPage <= 1)
				{
					tagWrapper.AddCssClass(PageDisabledClass);
				}
				else
				{
					buttonBefore.Attributes["form"] = "catalogForm";
					buttonBefore.Attributes["type"] = "submit";
					buttonBefore.Attributes["value"] = (PageModel.CurrentPage - 1).ToString();
					buttonBefore.Attributes["formaction"] = "/Catalog/" + (PageModel.CurrentPage - 1);
				}
				result.InnerHtml.AppendHtml(tagWrapper);



				for (int i = 1; i <= PageModel.TotalPages; i++)
				{
					tagWrapper = new TagBuilder("li");
					tagWrapper.AddCssClass(PageClassLi);

					TagBuilder tag = new TagBuilder("button");
					PageUrlValues["productPage"] = i;
					tag.Attributes["form"] = "catalogForm";
					tag.Attributes["type"] = "submit";
					tag.Attributes["value"] = i.ToString();
					tag.Attributes["formaction"] = "/Catalog/" + i + "?sortOrder=0";
					if (PageClassesEnabled)
					{
						tag.AddCssClass(PageClass);
						tagWrapper.AddCssClass(i == PageModel.CurrentPage ? PageClassSelected : "");
					}
					tag.InnerHtml.Append(i.ToString());
					tagWrapper.InnerHtml.AppendHtml(tag);
					result.InnerHtml.AppendHtml(tagWrapper);
				}
				output.Content.AppendHtml(result.InnerHtml);



				var tagWrapperNext = new TagBuilder("li");
				tagWrapperNext.InnerHtml.AppendHtml(buttonNext);
				buttonNext.InnerHtml.Append("Вперёд");
				buttonNext.AddCssClass(PageClass);
				tagWrapperNext.AddCssClass(PageClassLi);
				if (PageModel.CurrentPage >= PageModel.TotalPages)
				{
					tagWrapperNext.AddCssClass(PageDisabledClass);
				}
				else
				{
					buttonNext.Attributes["form"] = "catalogForm";
					buttonNext.Attributes["type"] = "submit";
					buttonNext.Attributes["value"] = (PageModel.CurrentPage + 1).ToString();
					buttonNext.Attributes["formaction"] = "/Catalog/" + (PageModel.CurrentPage + 1);

				}
				result.InnerHtml.AppendHtml(tagWrapperNext);
			}
		}
	}
}
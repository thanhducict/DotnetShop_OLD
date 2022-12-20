using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DotnetShop.Admin.Warehouses
{
    public class BooksTagHelperComponent : TagHelperComponent
    {
        private readonly string _script =
           @"<script type=""module"" src=""/books-assets/books-initialization.js""></script>";

        public override int Order => 1;

        public override Task ProcessAsync(TagHelperContext context,
                                          TagHelperOutput output)
        {
            if (string.Equals(context.TagName, "body", StringComparison.OrdinalIgnoreCase))
            {
                output.PostContent.AppendHtml(_script);
            }

            return Task.CompletedTask;
        }
    }
}

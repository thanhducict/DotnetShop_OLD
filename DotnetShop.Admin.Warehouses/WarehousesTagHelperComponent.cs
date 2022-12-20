using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DotnetShop.Admin.Warehouses
{
    public class WarehousesTagHelperComponent : TagHelperComponent
    {
        private readonly string _style =
           @"<link rel=""stylesheet"" href=""/warehouses-assets/warehouse-map.css"" />";

        private readonly string _script =
           @"<script type=""module"" src=""/warehouses-assets/warehouses-initialization.js""></script>";

        public override int Order => 1;

        public override Task ProcessAsync(TagHelperContext context,
                                          TagHelperOutput output)
        {
            if (string.Equals(context.TagName, "head", StringComparison.OrdinalIgnoreCase))
            {
                output.PostContent.AppendHtml(_style);
            }

            if (string.Equals(context.TagName, "body", StringComparison.OrdinalIgnoreCase))
            {
                output.PostContent.AppendHtml(_script);
            }

            return Task.CompletedTask;
        }
    }
}

using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DotnetShop.Admin.OrderManagement
{
    public class OrderManagementTagHelperComponent : TagHelperComponent
    {
        private readonly string _style =
           @"<link rel=""stylesheet"" href=""/OrderManagementClient/dist/order-management.bundle.css"" />";

        private readonly string _script =
           @"<script src=""/OrderManagementClient/dist/order-management.bundle.js""></script>";

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

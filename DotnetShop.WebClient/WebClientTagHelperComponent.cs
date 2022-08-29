using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DotnetShop.WebClient
{
    public class WebClientTagHelperComponent : TagHelperComponent
    {
        private readonly string _style =
           @"<link rel=""stylesheet"" href=""/ClientResources/webclient.bundle.css"" />";

        private readonly string _script =
           @"<script src=""/ClientResources/webclient.bundle.js""></script>";

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

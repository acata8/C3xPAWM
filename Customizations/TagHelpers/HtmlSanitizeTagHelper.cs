using System.Threading.Tasks;
using Ganss.XSS;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace C3xPAWM.Customizations.TagHelpers
{
    [HtmlTargetElement(Attributes = "html-sanitize")]
    public class HtmlSanitizeTagHelper : TagHelper
    {
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            //Otteniamo il contenuto del tag
            TagHelperContent tagHelperContent = await output.GetChildContentAsync(NullHtmlEncoder.Default);
            string content = tagHelperContent.GetContent(NullHtmlEncoder.Default);

            var sanitizer = CreateSanitizer();
            content = sanitizer.Sanitize(content);

            //Reimpostiamo il contenuto del tag
            output.Content.SetHtmlContent(content);
        }

        private static HtmlSanitizer CreateSanitizer()
        {
            var sanitizer = new HtmlSanitizer();

            //Tag consentiti
            sanitizer.AllowedTags.Clear();
            
            //Attributi consentiti
            sanitizer.AllowedAttributes.Clear();
            
            //Stili consentiti
            sanitizer.AllowedCssProperties.Clear();

            return sanitizer;
        }
    }
}
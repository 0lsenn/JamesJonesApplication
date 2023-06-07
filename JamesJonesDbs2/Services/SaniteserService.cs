using Ganss.Xss;

namespace JamesJonesDbs2.Services
{
    /// <summary>
    /// Sanitiser Service configuration
    /// </summary>
    public class SaniteserService
    {
        public HtmlSanitizer Sanitiser { get; set; }

        public SaniteserService() 
        { 
            if (Sanitiser == null)
            {
                Sanitiser = new HtmlSanitizer();
                Sanitiser.AllowDataAttributes = true;
                Sanitiser.AllowedAttributes.Add("class");
            }
        }
    }
}

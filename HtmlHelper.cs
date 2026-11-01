using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace פקטיקוד_2
{
    public class HtmlHelper
    {
        private readonly static HtmlHelper _instance =new HtmlHelper();

        public static HtmlHelper Instance => _instance;
        public string[] HtmlTags { get; set; }
        public string[] HtmlVoidTags { get; set; }

        private HtmlHelper()
        {
            var textHtmlTags = File.ReadAllText("JSON Files/HtmlTags.json");
            HtmlTags = JsonSerializer.Deserialize<string[]>(textHtmlTags);
            var textHtmlVoidTags = File.ReadAllText("JSON Files/HtmlVoidTags.json");
            HtmlVoidTags = JsonSerializer.Deserialize<string[]>(textHtmlVoidTags);

        }
    }
}


using System.Text.RegularExpressions;
using פקטיקוד_2;

var html = await Load("https://www.w3schools.com/html/default.asp");

var cleanHtml = new Regex("\\s+").Replace(html, " ");
var htmlLines = new Regex("<(.*?)>").Split(cleanHtml).Where(s => !string.IsNullOrWhiteSpace(s));


var root = new HtmlElement();
var current = root;
foreach (var line in htmlLines)
{
    var firstWord = line.Split(' ')[0];
    if (firstWord == "/html")
        break;
    else if (firstWord.StartsWith("/") && current.Parent!=null)
        current = current.Parent;
    else if(HtmlHelper.Instance.HtmlTags.Contains(firstWord))
    {
        HtmlElement element = new HtmlElement();
        element.Name = firstWord;
        element.Parent = current;
        current.Children.Add(element);
        var restOfLine = line.Substring(firstWord.Length);
        var attributes=new Regex("([^\\s]*?)=\"(.*?)\"").Matches(restOfLine);
        foreach (Match attribute in attributes)
        {
            element.Attributes[attribute.Groups[1].Value] = attribute.Groups[2].Value;
        }
        if(element.Attributes.ContainsKey("class"))
        {
            string classValue = element.Attributes["class"];
            string[] s= classValue.Split(' ');
            foreach (var item in s)
            {
                element.Classes.Add(item);
            }
        }
        if(element.Attributes.ContainsKey("id"))
        {
            element.Id = element.Attributes["id"];
        }
        if(!(line.EndsWith("/") || HtmlHelper.Instance.HtmlVoidTags.Contains(firstWord)))
        {
            current = element;
        }

    }
    else
        current.InnerHtml+=line;
}
var selector = Selector.ParseQuery("div");
var result = root.FindElements(selector);
Console.WriteLine("Found: " + result.Count);

foreach (var item in result)
{
    Console.WriteLine(item.Name + " | id: " + item.Id + " | classes: " + string.Join(" ", item.Classes));
}

Console.ReadLine();
async Task<string> Load(string url)
{
    HttpClient client = new HttpClient();
    var response = await client.GetAsync(url);
    var html = await response.Content.ReadAsStringAsync();
    return html;
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace פקטיקוד_2
{
    public class HtmlElement
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Dictionary<string, string> Attributes { get; set; } = new Dictionary<string, string>();
        public List<string> Classes { get; set; } = new List<string>();
        public string InnerHtml { get; set; }
        public HtmlElement Parent { get; set; }
        public List<HtmlElement> Children { get; set; } = new List<HtmlElement>();

        public IEnumerable<HtmlElement> Descendants()
        {
            Queue<HtmlElement> q = new Queue<HtmlElement>();
            q.Enqueue(this);
            while (q.Count > 0)
            {
                var current = q.Dequeue();
                yield return current;
                foreach (var item in current.Children)
                {
                    q.Enqueue(item);
                }
            }
        }
        public IEnumerable<HtmlElement> Ancestors()
        {
            var element = this.Parent;
            while (element != null)
            {
                yield return element;
                element = element.Parent;
            }
        }
    }
}

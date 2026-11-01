using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace פקטיקוד_2
{
    public static class Extensions
    {
        public static HashSet<HtmlElement> FindElements(this HtmlElement element, Selector selector)
        {
            var results = new HashSet<HtmlElement>();
            FindElementsRecursive(element, selector, results);
            return results;
        }
        private static void FindElementsRecursive(HtmlElement element, Selector selector, HashSet<HtmlElement> results)
        {
            var res = element.Descendants();
            var filter = res.Where(r => IsMatch(r, selector));
            if (selector.Child == null)
            {
                foreach (var item in filter)
                {
                   results.Add(item);
                }
            }
            else
            {
                foreach (var item in filter)
                {
                    FindElementsRecursive(item, selector.Child, results);
                }
            }
        }
        private static bool IsMatch(HtmlElement element, Selector selector)
        {
            if (selector.TagName != null && selector.TagName != element.Name)
                return false;

            if (selector.Id != null && selector.Id != element.Id)
                return false;

            if (!selector.Classes.All(c => element.Classes.Contains(c)))
                return false;

            return true;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace פקטיקוד_2
{
    public class Selector
    {
        public string TagName { get; set; }
        public string Id { get; set; }
        public List<string> Classes { get; set; } = new List<string>();
        public Selector Parent { get; set; }
        public Selector Child { get; set; }

        public static Selector ParseQuery(string query)
        {
            string[] res= query.Split(' ');
            Selector root=new Selector();
            var current = root;
            foreach (var item in res)
            {
                var parts = Regex.Split(item, "(?=[#.])");
                foreach (var p in parts)
                {
                    if (string.IsNullOrEmpty(p))
                        continue;
                    else if(p.StartsWith("#"))
                    {
                        current.Id=p.Substring(1);
                    }
                    else if (p.StartsWith("."))
                    {
                        current.Classes.Add(p.Substring(1));
                    }
                    else if(HtmlHelper.Instance.HtmlTags.Contains(p))
                    {
                        current.TagName = p;
                    }
                }
                Selector newSelector=new Selector();
                newSelector.Parent=current;
                current.Child= newSelector;
                current=newSelector;
            }
            current.Parent.Child = null;
            return root;
        }

    }
}

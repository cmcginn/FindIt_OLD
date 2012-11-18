using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindIt.Core.Extensions
{
    public static class CommonExtensions
    {
        public static string UniqueName(this List<string> names, string prefix)
        {
            int counter = 1;
            string uniqueName = prefix;
            while (names.Where(x => x.ToLower() == uniqueName.ToLower()).Count() > 0)
            {
                uniqueName = String.Format("{0} {1}", prefix, counter);
                counter++;
            }
            return uniqueName;
        }
    }
}

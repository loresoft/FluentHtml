using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace FluentHtml.Extensions
{
    public static class EnumerableExtensions
    {
        public static void AddRange<TSource>(this ICollection<TSource> list, IEnumerable<TSource> range)
        {
            foreach (var r in range)
                list.Add(r);
        }

    }
}

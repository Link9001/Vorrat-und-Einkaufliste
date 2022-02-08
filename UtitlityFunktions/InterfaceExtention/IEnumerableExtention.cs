using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtitlityFunctions.InterfaceExtention
{
    public static class IEnumerableExtention
    {
        public static bool SequenceEqualsIgnoreOrder<T>(this IEnumerable<T> first, IEnumerable<T> second)
        {
            return first.All(second.Contains);
        }
    }
}

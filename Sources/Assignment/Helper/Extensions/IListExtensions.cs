using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Extensions
{
    public static class IListExtensions
    {
        public static IList<T> getRange<T>(this IList<T> list, int startIndex, int endIndex)
        {
            IList<T> resultList = new List<T>((endIndex + 1) - startIndex);
            int end = endIndex + 1;
            for(int index = startIndex; index < end; index++)
            {
                resultList.Add(list[index]);
            }

            return resultList;
        }
    }
}

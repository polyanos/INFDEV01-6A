using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentCode.Extensions
{
    public static class IListExtensions
    {
        public static IList<T> getRange<T>(this IList<T> list, int startIndex, int endIndex)
        {
            IList<T> resultList = new List<T>((endIndex + 1) - startIndex);
            int end = endIndex + 1;
            for(int index = startIndex; index < end; index++)
            {
                resultList[index] = list[index];
            }

            return resultList;
        }
    }
}

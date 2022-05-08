using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegatesAndEvents
{
    public static class MaxExt
    {
        public static T? GetMax<T>(this IEnumerable<T> e, Func<T, float> getParameter) where T : class
        {
            if (e is null || !e.Any())
            {
                return default;
            }

            T maxValue = e.First();

            foreach (var item in e)
            {
                if (getParameter(item) > getParameter(maxValue))
                {
                    maxValue = item;
                }
            }

            return maxValue;
        }

    }
}

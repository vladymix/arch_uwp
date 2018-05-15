
using System.Collections.Generic;

namespace UWPCore
{
    public class CollectionsTools
    {
        public static bool IsNullOrEmpty<T>(IEnumerable<T> source) {

            ICollection<T> col = (ICollection<T>)source;
            return source == null || col.Count == 0;

        }

    }
}

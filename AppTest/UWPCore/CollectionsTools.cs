
using System.Collections.Generic;

namespace UWPCore
{
    public class CollectionsTools
    {
        public static bool IsNullOrEmpty<T>(ICollection<T> source) {
            return source == null || source.Count == 0;
        }

    }
}

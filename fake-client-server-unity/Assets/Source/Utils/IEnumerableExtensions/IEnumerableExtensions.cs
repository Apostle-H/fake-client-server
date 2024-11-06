using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

namespace Assets.Source.Utils.IEnumerableExtensions
{
    internal static class IEnumerableExtensions
    {
        internal static T RandomElement<T>(this IEnumerable<T> enumerable)
        {
            var randomIndex = Random.Range(0, enumerable.Count());
            return enumerable.ElementAt(randomIndex);
        }
    }
}

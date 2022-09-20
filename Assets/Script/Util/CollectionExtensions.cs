using System;
using System.Collections.Generic;

namespace Util
{
    public static class CollectionExtensions
    {
        /// <summary>
        /// Returns a random item using Unity.Random
        /// </summary>
        /// <returns>Random item</returns>
        public static T Random<T>(this IList<T> list)
        {
            if(list.Count <= 0)
                throw new IndexOutOfRangeException("List is empty");
            return list[UnityEngine.Random.Range(0, list.Count)];
        }
        
        /// <summary>
        /// Returns a random item using Unity.Random
        /// </summary>
        /// <returns>Random item</returns>
        public static T Random<T>(this T[] arr)
        {
            if(arr.Length <= 0)
                throw new IndexOutOfRangeException("Array is empty");
            return arr[UnityEngine.Random.Range(0, arr.Length)];
        }
    }
}
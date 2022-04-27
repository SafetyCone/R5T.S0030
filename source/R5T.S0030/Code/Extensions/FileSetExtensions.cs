using System;
using System.Linq;

using R5T.T0128;


namespace System
{
    public static class FileSetExtensions
    {
        public static void AddRange<T>(this FileSet<T> fileSet,
            params T[] entities)
            where T : class
        {
            fileSet.AddRange(entities.AsEnumerable());
        }

        public static void RemoveRange<T>(this FileSet<T> fileSet,
            params T[] entities)
            where T : class
        {
            fileSet.RemoveRange(entities.AsEnumerable());
        }

        public static void UpdateRange<T>(this FileSet<T> fileSet,
            params T[] entities)
            where T : class
        {
            fileSet.UpdateRange(entities.AsEnumerable());
        }
    }
}

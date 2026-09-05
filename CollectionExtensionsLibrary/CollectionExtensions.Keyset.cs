#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;

namespace CollectionExtensionsLibrary
{
    public static partial class CollectionExtensions
    {
        /// <summary>
        /// Realiza a paginação inicial (primeira página) utilizando o método Keyset (Seek Method).
        /// </summary>
        public static KeysetPagedList<T, TKey> ToKeysetPagedList<T, TKey>(
            this IEnumerable<T> source,
            Func<T, TKey> keySelector,
            int pageSize) where TKey : IComparable<TKey>
        {
            return source.ToKeysetPagedListInternal(keySelector, default, pageSize, SeekDirection.Forward, isFirstPage: true);
        }

        /// <summary>
        /// Realiza a paginação subsequente ou reversa a partir de um cursor de referência.
        /// </summary>
        public static KeysetPagedList<T, TKey> ToKeysetPagedList<T, TKey>(
            this IEnumerable<T> source,
            Func<T, TKey> keySelector,
            TKey cursor,
            int pageSize,
            SeekDirection direction = SeekDirection.Forward) where TKey : IComparable<TKey>
        {
            return source.ToKeysetPagedListInternal(keySelector, cursor, pageSize, direction, isFirstPage: false);
        }

        private static KeysetPagedList<T, TKey> ToKeysetPagedListInternal<T, TKey>(
            this IEnumerable<T> source,
            Func<T, TKey> keySelector,
            TKey? cursor,
            int pageSize,
            SeekDirection direction,
            bool isFirstPage) where TKey : IComparable<TKey>
        {
            if (source is null) throw new ArgumentNullException(nameof(source));
            if (keySelector is null) throw new ArgumentNullException(nameof(keySelector));
            if (pageSize <= 0) throw new ArgumentOutOfRangeException(nameof(pageSize), "O tamanho da página deve ser maior que zero.");

            var comparer = Comparer<TKey>.Default;
            IEnumerable<T> filtered;

            if (isFirstPage || cursor is null)
            {
                filtered = source.OrderBy(keySelector);
            }
            else if (direction == SeekDirection.Forward)
            {
                filtered = source
                    .Where(item => comparer.Compare(keySelector(item), cursor) > 0)
                    .OrderBy(keySelector);
            }
            else
            {
                filtered = source
                    .Where(item => comparer.Compare(keySelector(item), cursor) < 0)
                    .OrderByDescending(keySelector);
            }

            var buffer = filtered.Take(pageSize + 1).ToList();
            bool hasMore = buffer.Count > pageSize;

            if (hasMore)
            {
                buffer.RemoveAt(buffer.Count - 1);
            }

            if (direction == SeekDirection.Backward)
            {
                buffer.Reverse();
            }

            bool hasNextPage = direction == SeekDirection.Forward ? hasMore : (!isFirstPage);
            bool hasPreviousPage = direction == SeekDirection.Forward ? (!isFirstPage) : hasMore;

            TKey? nextCursor = buffer.Count > 0 ? keySelector(buffer[buffer.Count - 1]) : default;
            TKey? previousCursor = buffer.Count > 0 ? keySelector(buffer[0]) : default;

            return new KeysetPagedList<T, TKey>(
                buffer,
                pageSize,
                hasNextPage,
                hasPreviousPage,
                nextCursor,
                previousCursor);
        }
    }
}


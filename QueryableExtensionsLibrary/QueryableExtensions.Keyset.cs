#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace QueryableExtensionsLibrary
{
    /// <summary>
    /// Especifica a direção de navegação para paginação baseada em keyset (seek method).
    /// </summary>
    public enum SeekDirection
    {
        Forward,
        Backward
    }

    /// <summary>
    /// Representa o resultado de uma consulta paginada utilizando o método Keyset (Seek Method) sobre IQueryable, garantindo navegação em tempo constante O(1).
    /// </summary>
    public class KeysetPagedList<T, TKey>
    {
        public IReadOnlyList<T> Items { get; }
        public int PageSize { get; }
        public bool HasNextPage { get; }
        public bool HasPreviousPage { get; }
        public TKey? NextCursor { get; }
        public TKey? PreviousCursor { get; }

        public KeysetPagedList(
            IReadOnlyList<T> items,
            int pageSize,
            bool hasNextPage,
            bool hasPreviousPage,
            TKey? nextCursor,
            TKey? previousCursor)
        {
            Items = items ?? Array.Empty<T>();
            PageSize = pageSize;
            HasNextPage = hasNextPage;
            HasPreviousPage = hasPreviousPage;
            NextCursor = nextCursor;
            PreviousCursor = previousCursor;
        }
    }

    public static partial class QueryableExtensions
    {
        /// <summary>
        /// Realiza a paginação inicial (primeira página) utilizando o método Keyset (Seek Method) sobre IQueryable.
        /// </summary>
        public static KeysetPagedList<T, TKey> ToKeysetPagedList<T, TKey>(
            this IQueryable<T> source,
            Expression<Func<T, TKey>> keySelector,
            int pageSize) where TKey : IComparable<TKey>
        {
            return source.ToKeysetPagedListInternal(keySelector, default, pageSize, SeekDirection.Forward, isFirstPage: true);
        }

        /// <summary>
        /// Realiza a paginação subsequente ou reversa a partir de um cursor de referência sobre IQueryable.
        /// </summary>
        public static KeysetPagedList<T, TKey> ToKeysetPagedList<T, TKey>(
            this IQueryable<T> source,
            Expression<Func<T, TKey>> keySelector,
            TKey cursor,
            int pageSize,
            SeekDirection direction = SeekDirection.Forward) where TKey : IComparable<TKey>
        {
            return source.ToKeysetPagedListInternal(keySelector, cursor, pageSize, direction, isFirstPage: false);
        }

        private static KeysetPagedList<T, TKey> ToKeysetPagedListInternal<T, TKey>(
            this IQueryable<T> source,
            Expression<Func<T, TKey>> keySelector,
            TKey? cursor,
            int pageSize,
            SeekDirection direction,
            bool isFirstPage) where TKey : IComparable<TKey>
        {
            if (source is null) throw new ArgumentNullException(nameof(source));
            if (keySelector is null) throw new ArgumentNullException(nameof(keySelector));
            if (pageSize <= 0) throw new ArgumentOutOfRangeException(nameof(pageSize), "O tamanho da página deve ser maior que zero.");

            IQueryable<T> query = source;

            if (!isFirstPage && cursor is not null)
            {
                var parameter = keySelector.Parameters[0];
                var keyAccess = keySelector.Body;
                var constant = Expression.Constant(cursor, typeof(TKey));

                BinaryExpression comparison = direction == SeekDirection.Forward
                    ? Expression.GreaterThan(keyAccess, constant)
                    : Expression.LessThan(keyAccess, constant);

                var lambda = Expression.Lambda<Func<T, bool>>(comparison, parameter);
                query = query.Where(lambda);
            }

            query = direction == SeekDirection.Forward
                ? query.OrderBy(keySelector)
                : query.OrderByDescending(keySelector);

            var buffer = query.Take(pageSize + 1).ToList();
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

            var compiledKey = keySelector.Compile();
            TKey? nextCursor = buffer.Count > 0 ? compiledKey(buffer[buffer.Count - 1]) : default;
            TKey? previousCursor = buffer.Count > 0 ? compiledKey(buffer[0]) : default;

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


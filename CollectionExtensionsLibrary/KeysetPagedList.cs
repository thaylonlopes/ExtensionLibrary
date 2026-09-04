#nullable enable
using System;
using System.Collections.Generic;

namespace CollectionExtensionsLibrary
{
    /// <summary>
    /// Representa o resultado de uma consulta paginada utilizando o método Keyset (Seek Method), garantindo navegação em tempo constante O(1).
    /// </summary>
    /// <typeparam name="T">O tipo dos elementos contidos na página.</typeparam>
    /// <typeparam name="TKey">O tipo da chave de ordenação utilizada como cursor.</typeparam>
    public class KeysetPagedList<T, TKey>
    {
        /// <summary>
        /// Itens retornados na página atual.
        /// </summary>
        public IReadOnlyList<T> Items { get; }

        /// <summary>
        /// Tamanho máximo da página solicitado.
        /// </summary>
        public int PageSize { get; }

        /// <summary>
        /// Indica se existem páginas subsequentes.
        /// </summary>
        public bool HasNextPage { get; }

        /// <summary>
        /// Indica se existem páginas anteriores.
        /// </summary>
        public bool HasPreviousPage { get; }

        /// <summary>
        /// O cursor apontando para a chave do último elemento da página atual, utilizado para solicitar a próxima página.
        /// </summary>
        public TKey? NextCursor { get; }

        /// <summary>
        /// O cursor apontando para a chave do primeiro elemento da página atual, utilizado para solicitar a página anterior.
        /// </summary>
        public TKey? PreviousCursor { get; }

        /// <summary>
        /// Inicializa uma nova instância de <see cref="KeysetPagedList{T, TKey}"/>.
        /// </summary>
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
}


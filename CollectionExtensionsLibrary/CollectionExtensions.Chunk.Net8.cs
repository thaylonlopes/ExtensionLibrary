using System;
using System.Collections.Generic;
using System.Linq;

namespace CollectionExtensionsLibrary
{
    public static partial class CollectionExtensions
    {
        /// <summary>
        /// Splits the IEnumerable into chunks of a specified size leveraging .NET 8 native Enumerable.Chunk.
        /// </summary>
        /// <typeparam name="TSource">The type of elements in the source IEnumerable.</typeparam>
        /// <param name="source">The source IEnumerable.</param>
        /// <param name="chunkSize">The size of each chunk.</param>
        /// <returns>An IEnumerable of IEnumerable chunks.</returns>
        public static IEnumerable<IEnumerable<TSource>> ChunkBy<TSource>(this IEnumerable<TSource> source, int chunkSize)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (chunkSize <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(chunkSize), "O tamanho do lote deve ser maior que zero.");
            }

            return source.Chunk(chunkSize);
        }
    }
}

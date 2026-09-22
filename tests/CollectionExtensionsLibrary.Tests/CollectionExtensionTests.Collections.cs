using System;
using System.Collections.Generic;
using System.Linq;
using CollectionExtensionsLibrary;
using Xunit;

namespace CollectionExtensionsLibrary.Tests
{
    public class CollectionExtensionTestsCollections
    {
        private sealed class TestItem
        {
            public int Id { get; set; }
            public string Category { get; set; } = string.Empty;
        }

        [Fact]
        public void IsNullOrEmpty_NullCollection_ShouldReturnTrue()
        {
            IEnumerable<int>? nullSequence = null;
            Assert.True(nullSequence.IsNullOrEmpty());
        }

        [Fact]
        public void IsNullOrEmpty_EmptyListAndEmptyHashSet_ShouldReturnTrue()
        {
            var emptyList = new List<string>();
            var emptySet = new HashSet<int>();
            IReadOnlyCollection<int> readOnlyCollection = new List<int>();

            Assert.True(emptyList.IsNullOrEmpty());
            Assert.True(emptySet.IsNullOrEmpty());
            Assert.True(readOnlyCollection.IsNullOrEmpty());
            Assert.True(Enumerable.Empty<double>().IsNullOrEmpty());
        }

        [Fact]
        public void IsNullOrEmpty_CollectionWithElements_ShouldReturnFalse()
        {
            var populatedList = new List<string> { "item1" };
            var populatedSet = new HashSet<int> { 42 };
            IEnumerable<int> stream = Enumerable.Range(1, 10);

            Assert.False(populatedList.IsNullOrEmpty());
            Assert.False(populatedSet.IsNullOrEmpty());
            Assert.False(stream.IsNullOrEmpty());
        }

        [Fact]
        public void AddRangeIfNotNull_NullDestination_ShouldThrowArgumentNullException()
        {
            ICollection<int>? destination = null;
            Assert.Throws<ArgumentNullException>(() => destination!.AddRangeIfNotNull(new[] { 1, 2 }));
        }

        [Fact]
        public void AddRangeIfNotNull_NullSource_ShouldKeepDestinationUnchanged()
        {
            var destination = new List<int> { 1, 2, 3 };
            destination.AddRangeIfNotNull(null!);

            Assert.Equal(3, destination.Count);
            Assert.Equal(new[] { 1, 2, 3 }, destination);
        }

        [Fact]
        public void AddRangeIfNotNull_ValidSource_ShouldAppendAllElements()
        {
            var destination = new List<int> { 1 };
            destination.AddRangeIfNotNull(new[] { 2, 3, 4 });

            Assert.Equal(4, destination.Count);
            Assert.Equal(new[] { 1, 2, 3, 4 }, destination);
        }

        [Fact]
        public void DistinctBy_ShouldDeduplicateElementsByKeySelector()
        {
            var items = new List<TestItem>
            {
                new TestItem { Id = 1, Category = "Eletronicos" },
                new TestItem { Id = 2, Category = "Livros" },
                new TestItem { Id = 3, Category = "Eletronicos" },
                new TestItem { Id = 4, Category = "Roupas" }
            };

            var distinct = items.DistinctBy(x => x.Category).ToList();

            Assert.Equal(3, distinct.Count);
            Assert.Equal(1, distinct[0].Id);
            Assert.Equal("Eletronicos", distinct[0].Category);
            Assert.Equal(2, distinct[1].Id);
            Assert.Equal("Livros", distinct[1].Category);
            Assert.Equal(4, distinct[2].Id);
            Assert.Equal("Roupas", distinct[2].Category);
        }

        [Fact]
        public void DistinctBy_NullSourceOrKeySelector_ShouldThrowArgumentNullException()
        {
            IEnumerable<TestItem>? nullItems = null;
            Assert.Throws<ArgumentNullException>(() => nullItems!.DistinctBy(x => x.Id));

            IEnumerable<TestItem> validItems = new List<TestItem> { new TestItem { Id = 1, Category = "A" } };
            Func<TestItem, int>? nullSelector = null;
            Assert.Throws<ArgumentNullException>(() => validItems.DistinctBy(nullSelector!));
        }

        [Fact]
        public void ChunkBy_ValidBatchSize_ShouldPartitionCorrectly()
        {
            var numbers = Enumerable.Range(1, 250).ToList();
            var chunks = numbers.ChunkBy(100);

            Assert.Equal(3, chunks.Count);
            Assert.Equal(100, chunks[0].Count);
            Assert.Equal(100, chunks[1].Count);
            Assert.Equal(50, chunks[2].Count);
        }

        [Fact]
        public void ChunkBy_EnumerableSequence_ShouldPartitionCorrectly()
        {
            IEnumerable<int> stream = Enumerable.Range(1, 250);
            var chunks = stream.ChunkBy(100).Select(c => c.ToList()).ToList();

            Assert.Equal(3, chunks.Count);
            Assert.Equal(100, chunks[0].Count);
            Assert.Equal(100, chunks[1].Count);
            Assert.Equal(50, chunks[2].Count);
        }

        [Fact]
        public void ChunkBy_InvalidChunkSize_ShouldThrowArgumentOutOfRangeException()
        {
            IEnumerable<int> sequence = new[] { 1, 2, 3 };
            IList<int> list = new List<int> { 1, 2, 3 };

            Assert.Throws<ArgumentOutOfRangeException>(() => sequence.ChunkBy(0).ToList());
            Assert.Throws<ArgumentOutOfRangeException>(() => sequence.ChunkBy(-5).ToList());
            Assert.Throws<ArgumentOutOfRangeException>(() => list.ChunkBy(0));
            Assert.Throws<ArgumentOutOfRangeException>(() => list.ChunkBy(-5));
        }

        [Fact]
        public void ChunkBy_NullSource_ShouldThrowArgumentNullException()
        {
            IEnumerable<int>? nullSequence = null;
            IList<int>? nullList = null;

            Assert.Throws<ArgumentNullException>(() => nullSequence!.ChunkBy(10).ToList());
            Assert.Throws<ArgumentNullException>(() => nullList!.ChunkBy(10));
        }
    }
}

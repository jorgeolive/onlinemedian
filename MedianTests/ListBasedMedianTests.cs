using MovingMedian.Logic;

namespace MedianTests
{
    public class ListBasedMedianTests
    {
        [Fact]
        public void EmptySequence_ReturnsNaN()
        {
            IMedian median = new ListBasedMedian();

            Assert.Equal(double.NaN, median.Value);
        }

        [Theory]
        [InlineData(new[] { 5 }, 5)]
        [InlineData(new[] { 2, 4, 6 }, 4)]
        [InlineData(new[] { 10, 20, 30, 40 }, 25)]
        [InlineData(new[] { 7, 3, 1, 5, 9 }, 5)]
        [InlineData(new[] { 7, 3, 1, 5, 9, 9, 9 }, 7)]
        [InlineData(new[] { 7, 7, 7, 7 }, 7)]  // All elements are the same
        [InlineData(new[] { 7, 7, 7, 7, 7 }, 7)]  // All elements are the same
        [InlineData(new[] { 1, 3, 5, 7, 9 }, 5)]  // Odd number of elements
        [InlineData(new[] { 1, 2, 3, 4, 5, 6 }, 3.5)]  // Even number of elements
        [InlineData(new[] { 1, 2, 3 }, 2)]  // Sorted in ascending order
        [InlineData(new[] { 3, 2, 1 }, 2)]  // Sorted in descending order
        [InlineData(new[] { 2, 1, 3 }, 2)]  // Unsorted with median in the middle
        [InlineData(new[] { 3, 2, 2, 1, 3 }, 2)]// Unsorted with multiple medians
        public void AddedNumbers_ReturnsCorrectValue(int[] numbers, double expectedMedian)
        {
            IMedian median = new ListBasedMedian();

            foreach (var number in numbers)
            {
                median.Add(number);
            }

            Assert.Equal(expectedMedian, median.Value);
        }
    }
}

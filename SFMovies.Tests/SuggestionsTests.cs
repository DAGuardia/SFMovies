using Xunit;

namespace SFMovies.Tests
{
    public class SuggestionsTests
    {
        [Fact]
        public void Titles_AreDistinct_IgnoringCase_AndTrimmed()
        {
            var titles = new[] { " Ant-Man ", "ant-man", "Ant-Man and the Wasp" };
            var distinct = titles
                .Where(t => !string.IsNullOrWhiteSpace(t))
                .Select(t => t.Trim())
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToArray();

            Assert.Equal(2, distinct.Length);
            Assert.Contains("Ant-Man", distinct);
            Assert.Contains("Ant-Man and the Wasp", distinct);
        }
    }
}
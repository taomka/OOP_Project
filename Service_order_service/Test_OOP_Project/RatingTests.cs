using Service_order_service;

namespace Test_OOP_Project
{
    [TestClass]
    public sealed class RatingTests
    {
        private Rating rating;

        [TestInitialize]
        public void Setup()
        {
            rating = new Rating(4, "Good service.");
        }

        [TestMethod]
        public void GetScore()
        {
            // Act & Assert
            Assert.AreEqual(4, rating.GetScore());
        }

        [TestMethod]
        public void GetComment()
        {
            // Act & Assert
            Assert.AreEqual("Good service.", rating.GetComment());
        }

        [TestMethod]
        public void CalculateAverage_CorrectAverageRating()
        {
            // Arrange
            double expectedAverage = (4 + 5) / 2.0;

            // Act
            double actualAverage = rating.CalculateAverage(1);

            // Assert
            Assert.AreEqual(expectedAverage, actualAverage, 0.01);
        }

        [TestMethod]
        public void CalculateAverage_EmptyComment()
        {
            // Arrange
            Rating ratingWithoutComment = new Rating(5);

            // Act
            double average = ratingWithoutComment.CalculateAverage(1);

            // Assert
            Assert.AreEqual(5.0, average, 0.01);
        }

        [TestMethod]
        public void GetScore_NegativeScore()
        {
            // Arrange
            Rating negativeRating = new Rating(-1);

            // Act & Assert
            Assert.AreEqual(-1, negativeRating.GetScore());
        }

        [TestMethod]
        public void GetComment_WhenNoCommentIsProvided()
        {
            // Arrange
            Rating noCommentRating = new Rating(5);

            // Act & Assert
            Assert.IsNull(noCommentRating.GetComment());
        }
    }
}

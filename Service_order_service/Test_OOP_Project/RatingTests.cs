using Service_order_service;

namespace Test_OOP_Project
{
    [TestClass]
    public sealed class RatingTests
    {
        private Rating rating = null!;

        [TestInitialize]
        public void Setup()
        {
            rating = new Rating(4, "Good service.");
        }

        [TestMethod]
        public void GetScore()
        {
            // Act & Assert
            Assert.AreEqual(4, rating.Score);
        }

        [TestMethod]
        public void GetComment()
        {
            // Act & Assert
            Assert.AreEqual("Good service.", rating.Comment);
        }

        [TestMethod]
        public void GetScore_NegativeScore()
        {
            // Act & Assert
            var exception = Assert.ThrowsException<ArgumentException>(() => new Rating(-1));
            Assert.AreEqual("Score must be between 1 and 5.", exception.Message);
        }

        [TestMethod]
        public void GetComment_WhenNoCommentIsProvided()
        {
            // Act & Assert
            var exception = Assert.ThrowsException<ArgumentException>(() => new Rating(5, ""));
            Assert.AreEqual("Comment cannot be empty.", exception.Message);
        }
    }
}

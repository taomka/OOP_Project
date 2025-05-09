namespace Service_order_service
{
    public class Rating
    {
        private int _score;
        private string? _comment;

        public int Score
        {
            get => _score;
            set => _score = value >= 1 && value <= 5
                ? value
                : throw new ArgumentException("Score must be between 1 and 5.");
        }

        public string? Comment
        {
            get => _comment;
            set => _comment = !string.IsNullOrWhiteSpace(value)
                ? value
                : throw new ArgumentException("Comment cannot be empty.");
        }
        
        public Rating(int score, string? comment = null)
        {
            Score = score;
            Comment = comment;
        }
    }
}

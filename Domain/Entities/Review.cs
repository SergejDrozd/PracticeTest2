namespace Domain.Entities
{
    public class Review
    {
        public int Id { get; }
        public int Rating { get; }
        public string Comment { get; }
        public int UserId { get; }
        public int ProductId { get; }
        public DateTime CreatedAt { get; }

        public Review(int id, int rating, string comment, int userId, int productId, DateTime createdAt)
        {
            if (rating < 1 || rating > 5)
                throw new ArgumentException("Оценка должна быть в пределах от 1 до 5");

            if (userId <= 0)
                throw new ArgumentException("Идентификатор пользователя должен быть больше 0");

            if (productId <= 0)
                throw new ArgumentException("Идентификатор товара должен быть больше 0");

            Id = id;
            Rating = rating;
            Comment = comment;
            UserId = userId;
            ProductId = productId;
            CreatedAt = createdAt;
        }
    }
}

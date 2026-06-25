namespace Domain.Entities
{
    public class Product
    {
        public int Id { get; }
        public string Name { get; }
        public string Description { get; }
        public decimal Price { get; }
        public double AverageRating { get; private set; }
        public int CategoryId { get; }
        public Category Category { get; }
        public DateTime CreatedAt { get; }

        public Product(int id, string name, string description, decimal price, int categoryId, Category category, DateTime createdAt)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Название товара не может быть пустым");

            if (price <= 0)
                throw new ArgumentException("Цена должна быть больше 0");

            Id = id;
            Name = name;
            Description = description;
            Price = price;
            CategoryId = categoryId;
            Category = category;
            CreatedAt = createdAt;
            AverageRating = 0;
        }

        public void UpdateAverageRating(IEnumerable<Review> reviews)
        {
            if (reviews.Any())
                AverageRating = reviews.Average(r => r.Rating);
        }
    }
}

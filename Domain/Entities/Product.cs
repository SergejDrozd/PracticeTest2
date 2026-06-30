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
                throw new ArgumentException("�������� ������ �� ����� ���� ������");

            if (price <= 0)
                throw new ArgumentException("���� ������ ���� ������ 0");

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
            double sum = 0;
            int count = 0;
            foreach (var review in reviews)
            {
                sum += review.Rating;
                count++;
            }
            AverageRating = count > 0 ? sum / count : 0;
        }
    }
}

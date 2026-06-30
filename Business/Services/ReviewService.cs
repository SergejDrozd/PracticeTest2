using Domain.Entities;
using Domain.Events;
using Domain.Interfaces;

namespace Business.Services
{
    public class ReviewService
    {
        private readonly IReviewRepository reviewRepository;
        private readonly IProductRepository productRepository;
        private readonly IEventPublisher eventPublisher;

        public ReviewService(
            IReviewRepository reviewRepository,
            IProductRepository productRepository,
            IEventPublisher eventPublisher)
        {
            this.reviewRepository = reviewRepository;
            this.productRepository = productRepository;
            this.eventPublisher = eventPublisher;
        }

        public async Task AddAsync(int userId, int productId, int rating, string comment, CancellationToken ct = default)
        {
            var existingReview = await this.reviewRepository.GetByUserAndProductAsync(userId, productId, ct);
            if (existingReview != null)
                throw new InvalidOperationException("Вы уже оставили отзыв на этот товар");

            var review = new Review(0, rating, comment, userId, productId, DateTime.UtcNow);
            await this.reviewRepository.AddAsync(review, ct);
            await RecalculateAverageRatingAsync(productId, ct);

            this.eventPublisher.Publish(new ReviewAddedEvent(productId, rating));
        }

        public async Task UpdateAsync(int reviewId, int rating, string comment, CancellationToken ct = default)
        {
            var review = await this.reviewRepository.GetByIdAsync(reviewId, ct);
            if (review == null)
                throw new InvalidOperationException("Отзыв не найден");

            var updatedReview = new Review(reviewId, rating, comment, review.UserId, review.ProductId, review.CreatedAt);
            await this.reviewRepository.UpdateAsync(updatedReview, ct);
            await RecalculateAverageRatingAsync(review.ProductId, ct);
        }

        public async Task DeleteAsync(int reviewId, CancellationToken ct = default)
        {
            var review = await this.reviewRepository.GetByIdAsync(reviewId, ct);
            if (review == null)
                throw new InvalidOperationException("Отзыв не найден");

            await this.reviewRepository.DeleteAsync(reviewId, ct);
            await RecalculateAverageRatingAsync(review.ProductId, ct);

            this.eventPublisher.Publish(new ReviewDeletedEvent(review.ProductId));
        }

        public async Task<IEnumerable<Review>> GetByProductIdAsync(int productId, int skip, int take, CancellationToken ct = default)
            => await this.reviewRepository.GetByProductIdAsync(productId, skip, take, ct);

        private async Task RecalculateAverageRatingAsync(int productId, CancellationToken ct = default)
        {
            var product = await this.productRepository.GetByIdAsync(productId, ct);
            if (product == null)
                return;

            var reviews = await this.reviewRepository.GetByProductIdAsync(productId, ct: ct);
            var updatedProduct = new Product(
                product.Id, product.Name, product.Description, product.Price,
                product.CategoryId, product.Category, product.CreatedAt);
            updatedProduct.UpdateAverageRating(reviews);

            await this.productRepository.UpdateAsync(updatedProduct, ct);
        }
    }
}

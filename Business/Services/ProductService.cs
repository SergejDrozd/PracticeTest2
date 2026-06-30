using Domain.Entities;
using Domain.Events;
using Domain.Interfaces;
using Domain.Strategies;

namespace Business.Services
{
    public class ProductService
    {
        private readonly IProductRepository productRepository;
        private readonly IEventPublisher eventPublisher;

        public ProductService(IProductRepository productRepository, IEventPublisher eventPublisher)
        {
            this.productRepository = productRepository;
            this.eventPublisher = eventPublisher;
        }

        public async Task<Product?> GetByIdAsync(int id, CancellationToken ct = default)
            => await this.productRepository.GetByIdAsync(id, ct);

        public async Task<IEnumerable<Product>> GetAllAsync(int skip, int take, IProductSortingStrategy? strategy = null, CancellationToken ct = default)
        {
            var products = await this.productRepository.GetAllAsync(skip, take, ct);
            return ApplySorting(products, strategy);
        }

        public async Task<IEnumerable<Product>> SearchByNameAsync(string name, IProductSortingStrategy? strategy = null, CancellationToken ct = default)
        {
            var products = await this.productRepository.SearchByNameAsync(name, ct);
            return ApplySorting(products, strategy);
        }

        public async Task<IEnumerable<Product>> GetByCategoryAsync(int categoryId, int skip, int take, IProductSortingStrategy? strategy = null, CancellationToken ct = default)
        {
            var products = await this.productRepository.GetByCategoryAsync(categoryId, skip, take, ct);
            return ApplySorting(products, strategy);
        }

        public async Task<IEnumerable<Product>> GetByPriceRangeAsync(decimal min, decimal max, int skip, int take, IProductSortingStrategy? strategy = null, CancellationToken ct = default)
        {
            var products = await this.productRepository.GetByPriceRangeAsync(min, max, skip, take, ct);
            return ApplySorting(products, strategy);
        }

        public async Task<IEnumerable<Product>> GetByMinRatingAsync(double minRating, int skip, int take, IProductSortingStrategy? strategy = null, CancellationToken ct = default)
        {
            var products = await this.productRepository.GetByMinRatingAsync(minRating, skip, take, ct);
            return ApplySorting(products, strategy);
        }

        public async Task AddAsync(Product product, CancellationToken ct = default)
            => await this.productRepository.AddAsync(product, ct);

        public async Task UpdatePriceAsync(int productId, decimal newPrice, CancellationToken ct = default)
        {
            var product = await this.productRepository.GetByIdAsync(productId, ct);
            if (product == null)
                throw new InvalidOperationException("Товар не найден");

            var oldPrice = product.Price;
            var updatedProduct = new Product(
                product.Id, product.Name, product.Description, newPrice,
                product.CategoryId, product.Category, product.CreatedAt);

            await this.productRepository.UpdateAsync(updatedProduct, ct);
            this.eventPublisher.Publish(new PriceChangedEvent(product.Name, oldPrice, newPrice));
        }

        public async Task DeleteAsync(int id, CancellationToken ct = default)
        {
            var product = await this.productRepository.GetByIdAsync(id, ct);
            if (product == null)
                throw new InvalidOperationException("Товар не найден");

            await this.productRepository.DeleteAsync(id, ct);
            this.eventPublisher.Publish(new ProductDeletedEvent(product.Name));
        }

        public async Task<int> CountAsync(CancellationToken ct = default)
            => await this.productRepository.CountAsync(ct);

        private static IEnumerable<Product> ApplySorting(IEnumerable<Product> products, IProductSortingStrategy? strategy)
        {
            return strategy == null ? products : new ProductSortingContext(strategy).Sort(products);
        }
    }
}

using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IProductRepository
    {
        Task<Product?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<IEnumerable<Product>> GetAllAsync(int skip = 0, int take = 10, CancellationToken ct = default);
        Task<IEnumerable<Product>> SearchByNameAsync(string name, CancellationToken ct = default);
        Task<IEnumerable<Product>> GetByCategoryAsync(int categoryId, int skip = 0, int take = 10, CancellationToken ct = default);
        Task<IEnumerable<Product>> GetByPriceRangeAsync(decimal min, decimal max, int skip = 0, int take = 10, CancellationToken ct = default);
        Task<IEnumerable<Product>> GetByMinRatingAsync(double minRating, int skip = 0, int take = 10, CancellationToken ct = default);
        Task AddAsync(Product product, CancellationToken ct = default);
        Task UpdateAsync(Product product, CancellationToken ct = default);
        Task DeleteAsync(int id, CancellationToken ct = default);
        Task<int> CountAsync(CancellationToken ct = default);
    }
}

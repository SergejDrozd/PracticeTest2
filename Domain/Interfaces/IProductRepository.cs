using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IProductRepository
    {
        Task<Product?> GetByIdAsync(int id);
        Task<IEnumerable<Product>> GetAllAsync(int skip = 0, int take = 10);
        Task<IEnumerable<Product>> SearchByNameAsync(string name);
        Task<IEnumerable<Product>> GetByCategoryAsync(int categoryId, int skip = 0, int take = 10);
        Task<IEnumerable<Product>> GetByPriceRangeAsync(decimal min, decimal max, int skip = 0, int take = 10);
        Task<IEnumerable<Product>> GetByMinRatingAsync(double minRating, int skip = 0, int take = 10);
        Task AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(int id);
        Task<int> CountAsync();
    }
}

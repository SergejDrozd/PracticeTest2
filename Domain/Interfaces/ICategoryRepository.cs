using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ICategoryRepository
    {
        Task<Category?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<Category?> GetByNameAsync(string name, CancellationToken ct = default);
        Task<IEnumerable<Category>> GetAllAsync(CancellationToken ct = default);
        Task AddAsync(Category category, CancellationToken ct = default);
        Task UpdateAsync(Category category, CancellationToken ct = default);
        Task DeleteAsync(int id, CancellationToken ct = default);
        Task<bool> HasProductsAsync(int categoryId, CancellationToken ct = default);
    }
}

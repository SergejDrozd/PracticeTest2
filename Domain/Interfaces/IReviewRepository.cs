using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IReviewRepository
    {
        Task<Review?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<IEnumerable<Review>> GetByProductIdAsync(int productId, int skip = 0, int take = 10, CancellationToken ct = default);
        Task<Review?> GetByUserAndProductAsync(int userId, int productId, CancellationToken ct = default);
        Task AddAsync(Review review, CancellationToken ct = default);
        Task UpdateAsync(Review review, CancellationToken ct = default);
        Task DeleteAsync(int id, CancellationToken ct = default);
        Task<int> CountByProductIdAsync(int productId, CancellationToken ct = default);
    }
}

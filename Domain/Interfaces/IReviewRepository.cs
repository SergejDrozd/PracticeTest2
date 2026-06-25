using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IReviewRepository
    {
        Task<Review?> GetByIdAsync(int id);
        Task<IEnumerable<Review>> GetByProductIdAsync(int productId, int skip = 0, int take = 10);
        Task<Review?> GetByUserAndProductAsync(int userId, int productId);
        Task AddAsync(Review review);
        Task UpdateAsync(Review review);
        Task DeleteAsync(int id);
        Task<int> CountByProductIdAsync(int productId);
    }
}

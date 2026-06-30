using Domain.Entities;

namespace Domain.Strategies
{
    public class SortByRatingStrategy : IProductSortingStrategy
    {
        public IEnumerable<Product> Sort(IEnumerable<Product> products)
        {
            return products.OrderByDescending(p => p.AverageRating);
        }
    }
}

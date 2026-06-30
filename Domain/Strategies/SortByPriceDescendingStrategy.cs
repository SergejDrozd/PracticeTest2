using Domain.Entities;

namespace Domain.Strategies
{
    public class SortByPriceDescendingStrategy : IProductSortingStrategy
    {
        public IEnumerable<Product> Sort(IEnumerable<Product> products)
        {
            return products.OrderByDescending(p => p.Price);
        }
    }
}

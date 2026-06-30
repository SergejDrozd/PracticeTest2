using Domain.Entities;

namespace Domain.Strategies
{
    public class SortByPriceAscendingStrategy : IProductSortingStrategy
    {
        public IEnumerable<Product> Sort(IEnumerable<Product> products)
        {
            return products.OrderBy(p => p.Price);
        }
    }
}

using Domain.Entities;

namespace Domain.Strategies
{
    public interface IProductSortingStrategy
    {
        IEnumerable<Product> Sort(IEnumerable<Product> products);
    }
}

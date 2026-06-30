using Domain.Entities;

namespace Domain.Strategies
{
    public class ProductSortingContext
    {
        private IProductSortingStrategy strategy;

        public ProductSortingContext(IProductSortingStrategy strategy)
        {
            this.strategy = strategy;
        }

        public void SetStrategy(IProductSortingStrategy strategy)
        {
            this.strategy = strategy;
        }

        public IEnumerable<Product> Sort(IEnumerable<Product> products)
        {
            return this.strategy.Sort(products);
        }
    }
}

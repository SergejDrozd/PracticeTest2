using Domain.Entities;
using Domain.Events;
using Domain.Interfaces;

namespace Business.Services
{
    public class CategoryService
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IEventPublisher eventPublisher;

        public CategoryService(ICategoryRepository categoryRepository, IEventPublisher eventPublisher)
        {
            this.categoryRepository = categoryRepository;
            this.eventPublisher = eventPublisher;
        }

        public async Task<Category?> GetByIdAsync(int id, CancellationToken ct = default)
            => await this.categoryRepository.GetByIdAsync(id, ct);

        public async Task<IEnumerable<Category>> GetAllAsync(CancellationToken ct = default)
            => await this.categoryRepository.GetAllAsync(ct);

        public async Task AddAsync(string name, string description, CancellationToken ct = default)
        {
            var existing = await this.categoryRepository.GetByNameAsync(name, ct);
            if (existing != null)
                throw new InvalidOperationException("Категория с таким названием уже существует");

            var category = new Category(0, name, description);
            await this.categoryRepository.AddAsync(category, ct);
        }

        public async Task UpdateAsync(int id, string name, string description, CancellationToken ct = default)
        {
            var category = await this.categoryRepository.GetByIdAsync(id, ct);
            if (category == null)
                throw new InvalidOperationException("Категория не найдена");

            var updatedCategory = new Category(id, name, description);
            await this.categoryRepository.UpdateAsync(updatedCategory, ct);
        }

        public async Task DeleteAsync(int id, CancellationToken ct = default)
        {
            var category = await this.categoryRepository.GetByIdAsync(id, ct);
            if (category == null)
                throw new InvalidOperationException("Категория не найдена");

            var hasProducts = await this.categoryRepository.HasProductsAsync(id, ct);
            if (hasProducts)
                throw new InvalidOperationException("Нельзя удалить категорию, в которой есть товары");

            await this.categoryRepository.DeleteAsync(id, ct);
            this.eventPublisher.Publish(new CategoryDeletedEvent(category.Name));
        }
    }
}

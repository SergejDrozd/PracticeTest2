using Domain.Entities;
using Domain.Enums;
using Domain.Events;
using Domain.Interfaces;

namespace Business.Services
{
    public class UserService
    {
        private readonly IUserRepository userRepository;
        private readonly IEventPublisher eventPublisher;

        public UserService(IUserRepository userRepository, IEventPublisher eventPublisher)
        {
            this.userRepository = userRepository;
            this.eventPublisher = eventPublisher;
        }

        public async Task RegisterAsync(string username, string email, string password, Role role, CancellationToken ct = default)
        {
            var existingUser = await this.userRepository.GetByUsernameAsync(username, ct);
            if (existingUser != null)
                throw new InvalidOperationException("Пользователь с таким именем уже существует");

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
            var user = new User(0, username, email, passwordHash, role, DateTime.UtcNow);
            await this.userRepository.AddAsync(user, ct);
        }

        public async Task<User?> LoginAsync(string username, string password, CancellationToken ct = default)
        {
            var user = await this.userRepository.GetByUsernameAsync(username, ct);
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                return null;

            this.eventPublisher.Publish(new UserLoggedInEvent(user.Username));
            return user;
        }

        public async Task ChangeRoleAsync(int userId, Role newRole, CancellationToken ct = default)
        {
            var user = await this.userRepository.GetByIdAsync(userId, ct);
            if (user == null)
                throw new InvalidOperationException("Пользователь не найден");

            var oldRole = user.Role;
            var updatedUser = new User(user.Id, user.Username, user.Email, user.PasswordHash, newRole, user.CreatedAt);
            await this.userRepository.UpdateAsync(updatedUser, ct);

            this.eventPublisher.Publish(new UserRoleChangedEvent(user.Username, oldRole, newRole));
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync(CancellationToken ct = default)
            => await this.userRepository.GetAllAsync(ct);

        public async Task DeleteUserAsync(int id, CancellationToken ct = default)
        {
            var user = await this.userRepository.GetByIdAsync(id, ct);
            if (user == null)
                throw new InvalidOperationException("Пользователь не найден");

            await this.userRepository.DeleteAsync(id, ct);
        }
    }
}

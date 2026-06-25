namespace Domain.Entities
{
    public class User
    {
        public int Id { get; }
        public string Username { get; }
        public string Email { get; }
        public string PasswordHash { get; }
        public Enums.Role Role { get; }
        public DateTime CreatedAt { get; }

        public User(int id, string username, string email, string passwordHash, Enums.Role role, DateTime createdAt)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("Имя пользователя не может быть пустым");

            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
                throw new ArgumentException("Некорректный Email");

            if (string.IsNullOrWhiteSpace(passwordHash))
                throw new ArgumentException("Пароль обязателен");

            Id = id;
            Username = username;
            Email = email;
            PasswordHash = passwordHash;
            Role = role;
            CreatedAt = createdAt;
        }
    }
}

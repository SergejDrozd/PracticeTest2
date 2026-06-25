namespace Domain.Entities
{
    public class Category
    {
        public int Id { get; }
        public string Name { get; }
        public string Description { get; }

        public Category(int id, string name, string description)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Ќазвание категории не может быть пустым");

            Id = id;
            Name = name;
            Description = description;
        }
    }
}

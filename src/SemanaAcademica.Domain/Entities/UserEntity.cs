namespace SemanaAcademica.Domain.Entities
{
    public class UserEntity
    {
        public Guid Id { get; private set; }
        public string FullName { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string Password { get; private set; } = string.Empty;
        public DateTime CreatedAt { get; private set; }
        public bool Active { get; private set; }

        protected UserEntity() { }

        public UserEntity(string fullName, string email, string password)
        {
            Id = Guid.NewGuid();
            FullName = fullName;
            Email = email;
            Password = password;
            CreatedAt = DateTime.UtcNow;
            Active = true;
        }

        public void Deactivate() => Active = false;

        public void Activate() => Active = true;

        public void UpdatePassword(string newPassword) => Password = newPassword;

        public void Update(string fullName, string email)
        {
            FullName = fullName;
            Email = email;
        }
    }
}